using System.Collections.Generic;

namespace PlanningPoker.Domain.Models.Sessions
{
    public class CreateSession
    {
        public string Title { get; set; }

        public string EstimationUnit { get; set; }

        public List<int> PlayersIds { get; set; }
    }
}
