using Microsoft.EntityFrameworkCore;

namespace GameRestApi.Model
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    [Index(nameof(Name))]
    public class User
    {
        /// <summary>
        /// Id пользователя в базе данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Баланс
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Отправленные транзакции пользователя
        /// </summary>
        public ICollection<GameTransaction>? TransactionsSended { get; set; }

        /// <summary>
        /// Полученные транзакции пользователя
        /// </summary>
        public ICollection<GameTransaction>? TransactionsRecieved { get; set; }

        /// <summary>
        /// Истории матчей пользователя выигранные, навигационное свойство
        /// </summary>
        public ICollection<MatchHistory>? MatchesWinned { get; set; }

        /// <summary>
        /// Истории матчей пользователя проигранные, навигационное свойство
        /// </summary>
        public ICollection<MatchHistory>? MatchesLosed { get; set; }
    }
}
