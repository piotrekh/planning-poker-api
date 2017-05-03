using Microsoft.EntityFrameworkCore;
using PlanningPoker.DataAccess;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Repositories;
using System.Linq;

namespace PlanningPoker.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly PlanningPokerDbContext _dbContext;

        public GamesRepository(PlanningPokerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Game game)
        {
            _dbContext.Games.Add(game);
        }

        public bool HasUnfinishedGameWithSessionId(int sessionId)
        {
            return _dbContext.Games.Any(x => x.SessionId == sessionId && x.FinalEstimate == null);                
        }

        public Game GetCurrentGameWithEstimatesForSession(int sessionId)
        {
            return _dbContext.Games
                .Include(x => x.Estimates).ThenInclude(x => x.User)
                .Where(x => x.SessionId == sessionId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
        }
    }
}
