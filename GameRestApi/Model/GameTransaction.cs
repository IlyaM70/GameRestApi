using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameRestApi.Model
{
    /// <summary>
    /// Сущность транзакции
    /// </summary>
    public class GameTransaction
    {
        /// <summary>
        /// Id транзакции в базе данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public int Summ {  get; set; }

        /// <summary>
        /// Id отправителя в базе данных
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Id получателя в базе данных
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Пользователь отправитель, навигационное свойство
        /// </summary>
        public User? Sender { get; set; }

        /// <summary>
        /// Пользователь получатель, навигационное свойство
        /// </summary>
        public User? Receiver { get; set; }
    }
}
