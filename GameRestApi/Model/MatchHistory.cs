namespace GameRestApi.Model
{
    /// <summary>
    /// Сущность истории матча
    /// </summary>
    public class MatchHistory
    {
        /// <summary>
        /// Id истории матча в базе данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id победителя
        /// </summary>
        public int WinnerId { get; set; }

        /// <summary>
        /// Id проигравшего
        /// </summary>
        public int LoserId { get; set; }

        /// <summary>
        /// Победитель, навигационное свойство
        /// </summary>
        public User? Winner{ get; set; }

        /// <summary>
        /// Проигравший, навигационное свойство
        /// </summary>
        public User? Loser { get; set; }

        /// <summary>
        /// Ставка, которую проигравший платит подедителю
        /// </summary>
        public int Rate { get; set; }
    }
}
