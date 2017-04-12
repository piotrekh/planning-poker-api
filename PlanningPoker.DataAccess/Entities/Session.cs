using System;
using System.Collections.Generic;

namespace PlanningPoker.DataAccess.Entities
{
    public class Session
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ModeratorId { get; set; }

        public string EstimationUnit { get; set; }

        public DateTime DateCreated { get; set; }


        #region Navigation properties

        public User Moderator { get; set; }

        public ICollection<SessionPlayer> Players { get; set; }

        public ICollection<Game> Games { get; set; }

        #endregion
    }
}
