using PlanningPoker.Domain.Enums;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Models.Sessions
{
    public class CreateSession
    {
        public string Title { get; set; }

        public EstimationUnit EstimationUnit { get; set; }

        public List<int> PlayersIds { get; set; }
    }
}
