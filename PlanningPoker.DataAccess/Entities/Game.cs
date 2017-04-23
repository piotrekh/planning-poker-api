using System;
using System.Collections.Generic;

namespace PlanningPoker.DataAccess.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public string TaskName { get; set; }

        public string ExternalTaskUrl { get; set; }

        public int? FinalEstimate { get; set; }

        public DateTime DateCreated { get; set; }


        #region Navigation properties

        public Session Session { get; set; }

        public ICollection<GameEstimate> Estimates { get; set; }

        #endregion
    }
}
