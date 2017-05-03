using PlanningPoker.Domain.Models.Sessions;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Services
{
    public interface ISessionsService
    {
        void CreateSession(int userId, CreateSession session);

        List<SessionWithGames> GetUserSessions(int userId, bool finished);


    }
}
