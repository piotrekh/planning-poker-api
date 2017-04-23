using PlanningPoker.DataAccess.Entities;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Repositories
{
    public interface ISessionsRepository
    {
        void Create(Session session);        

        List<Session> GetUserSessions(int userId);
    }
}
