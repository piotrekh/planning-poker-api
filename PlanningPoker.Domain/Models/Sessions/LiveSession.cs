using PlanningPoker.Domain.Models.Games;
using PlanningPoker.Domain.Models.Users;
using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Models.Sessions
{
    public class LiveSession
    {
        public int SessionId { get; set; }

        public Guid LiveSessionId { get; set; }

        public int ModeratorId { get; set; }

        public List<User> Players { get; set; }

        public GameWithEstimates CurrentGame { get; set; }
    }
}
