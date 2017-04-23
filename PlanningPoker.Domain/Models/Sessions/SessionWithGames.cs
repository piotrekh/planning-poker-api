using PlanningPoker.Domain.Models.Games;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Models.Sessions
{
    public class SessionWithGames
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Moderator Moderator { get; set; }

        public string EstimationUnit { get; set; }

        public UtcDateTime DateCreated { get; set; }

        /// <summary>
        /// An unfinished session is a session that has a pending game or no games at all
        /// </summary>
        public bool IsFinished { get; set; }

        public List<Game> Games { get; set; }
    }
}
