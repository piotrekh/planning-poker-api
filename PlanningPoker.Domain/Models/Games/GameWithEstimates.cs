using System.Collections.Generic;

namespace PlanningPoker.Domain.Models.Games
{
    public class GameWithEstimates
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string ExternalTaskUrl { get; set; }

        public int? FinalEstimate { get; set; }

        /// <summary>
        /// A list of all players' estimates - if a player
        /// has not estimated the task yet, he will still
        /// be present on this list, but without the estimate
        /// (its value will be null)
        /// </summary>
        public List<GameEstimate> PlayerEstimates { get; set; }
    }
}
