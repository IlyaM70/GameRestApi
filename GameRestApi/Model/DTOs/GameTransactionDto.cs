using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameRestApi.Model.DTOs
{
    /// <summary>
    /// DTO для GameTransaction
    /// </summary>
    public class GameTransactionDto
    {
        /// <summary>
        /// Сумма транзакции
        /// </summary>
        [Required(ErrorMessage = "Введите сумму транзакции")]
        [Range(1, int.MaxValue, ErrorMessage = "Сумма транзакции должна быть больше 0")]
        [NotNull()]
        [DefaultValue(1)]
        public int Summ { get; set; } = 1;

        /// <summary>
        /// Id отправителя в базе данных
        /// </summary>
        [Required(ErrorMessage = "Введите Id отправителя")]
        [Range(1, int.MaxValue, ErrorMessage = "Id отправителя должен быть больше 0")]
        [NotNull()]
        [DefaultValue(1)]
        public int SenderId { get; set; } = 1;

        /// <summary>
        /// Id получателя в базе данных
        /// </summary>
        [Required(ErrorMessage = "Введите Id получателя")]
        [Range(1, int.MaxValue, ErrorMessage = "Id получателя должен быть больше 0")]
        [NotNull()]
        [DefaultValue(2)]
        public int ReceiverId { get; set; } = 1;
    }
}
