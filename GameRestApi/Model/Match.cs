using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace GameRestApi.Model
{
    /// <summary>
    /// Класс матча
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Id игрока 1
        /// </summary>
        [Required(ErrorMessage = "Введите Id игрока 1")]
        [Range(1, int.MaxValue, ErrorMessage = "Id игрока 1 должен быть больше 0")]
        [NotNull()]
        [DefaultValue(1)]
        public int PlayerOneId { get; set; }

        /// <summary>
        /// Id игрока 2
        /// </summary>
        [Required(ErrorMessage = "Введите Id игрока 2")]
        [Range(1, int.MaxValue, ErrorMessage = "Id игрока 2 должен быть больше 0")]
        [NotNull()]
        [DefaultValue(2)]
        public int PlayerTwoId { get; set; }

        /// <summary>
        /// Ставка, которую проигравший платит победителю
        /// </summary>
        [Required(ErrorMessage = "Введите cтавку,  которую проигравший платит победителю")]
        [Range(1, int.MaxValue, ErrorMessage = "Ставка должна быть больше 0")]
        [NotNull()]
        [DefaultValue(1)]
        public int Rate { get; set; }
    }
}
