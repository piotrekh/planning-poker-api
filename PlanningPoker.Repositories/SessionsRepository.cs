using PlanningPoker.DataAccess;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace PlanningPoker.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        private readonly PlanningPokerDbContext _dbContext;

        public SessionsRepository(PlanningPokerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Session session)
        {
            _dbContext.Sessions.Add(session);
        }

        public List<Session> GetUserSessions(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
