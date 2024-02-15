using GameRestApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Reflection.Emit;

namespace GameRestApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        public DbSet<User> Users{ get; set; }
        public DbSet<GameTransaction> GameTransactions { get; set; }
        public DbSet<MatchHistory> MatchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Отношения

            // Один ко многим User и MatchHistory выигранные
            builder.Entity<User>()
                .HasMany(u => u.MatchesWinned)
                .WithOne(m => m.Winner)
                .HasForeignKey(m => m.WinnerId);

            // Один ко многим User и MatchHistory проигранные
            builder.Entity<User>()
                .HasMany(u => u.MatchesLosed)
                .WithOne(m => m.Loser)
                .HasForeignKey(m => m.LoserId);

            // Один ко многим User и Transactions отправленные
            builder.Entity<User>()
                .HasMany(u => u.TransactionsSended)
                .WithOne(t => t.Sender)
                .HasForeignKey(t => t.SenderId);

            // Один ко многим User и Transactions полученные
            builder.Entity<User>()
                .HasMany(u => u.TransactionsRecieved)
                .WithOne(t => t.Receiver)
                .HasForeignKey(t => t.ReceiverId);

            #endregion

            #region Заполнить данные
            builder.Entity<User>().HasData(
                new User { Id = 1,Name = "Red",Balance=1000000},
                new User { Id = 2, Name = "Blue",Balance = 1000000 }
            );
            #endregion

        }
    }
}
