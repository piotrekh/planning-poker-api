using PlanningPoker.DataAccess.Entities;

namespace PlanningPoker.Domain.Repositories
{
    public interface IGamesRepository
    {
        void Create(Game game);

        bool HasUnfinishedGameWithSessionId(int sessionId);
    }
}
