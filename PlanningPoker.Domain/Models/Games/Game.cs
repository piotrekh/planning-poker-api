namespace PlanningPoker.Domain.Models.Games
{
    public class Game
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public string TaskName { get; set; }

        public string ExternalTaskUrl { get; set; }

        public int? FinalEstimate { get; set; }

        public UtcDateTime DateCreated { get; set; }
    }
}
