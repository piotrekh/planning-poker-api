﻿using Microsoft.EntityFrameworkCore;
using PlanningPoker.DataAccess;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        public List<Session> GetUserSessions(int userId, bool finished)
        {
            return _dbContext.Sessions
                .Include(x => x.Games)
                .Include(x => x.Moderator)
                .Include(x => x.Players)
                .Where(x => x.IsFinished
                        && x.Players.Any(player => player.UserId == userId))
                .ToList();
        }

        public Session GetSessionWithPlayers(int sessionId)
        {
            return _dbContext.Sessions                
                .Include(x => x.Players).ThenInclude(x => x.User)                
                .Single(x => x.Id == sessionId);
        }
    }
}
