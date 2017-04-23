﻿using PlanningPoker.Domain.Models.Sessions;
using PlanningPoker.Domain.Providers.Transactions;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Models;

namespace PlanningPoker.Services
{
    public class SessionsService : ISessionsService
    {
        private readonly ISessionsRepository _sessionsRepository;
        private readonly ITransactionProvider _transactionProvider;

        public SessionsService(ISessionsRepository sessionsRepository, ITransactionProvider transactionProvider)
        {
            _sessionsRepository = sessionsRepository;
            _transactionProvider = transactionProvider;
        }

        public void CreateSession(int userId, CreateSession session)
        {
            List<SessionPlayer> players = session.PlayersIds.Select(x => new SessionPlayer()
            {
                UserId = x
            }).ToList();

            //make sure the moderator is also on the players list
            if (players.FirstOrDefault(x => x.UserId == userId) == null)
                players.Add(new SessionPlayer() { UserId = userId });

            Session sessionEntity = new Session()
            {
                DateCreated = DateTime.UtcNow,
                EstimationUnit = session.EstimationUnit.ToString(),
                ModeratorId = userId,
                Title = session.Title,
                Players = players
            };

            _sessionsRepository.Create(sessionEntity);

            _transactionProvider.SaveChanges();
        }

        public List<SessionWithGames> GetUserSessions(int userId)
        {
            List<Session> sessionEntities = _sessionsRepository.GetUserSessions(userId);

            List<SessionWithGames> sessions = new List<SessionWithGames>();
            foreach (Session sessionEntity in sessionEntities)
            {
                List<Domain.Models.Games.Game> games = sessionEntity.Games.Select(x => new Domain.Models.Games.Game()
                {
                    DateCreated = new UtcDateTime(x.DateCreated),
                    ExternalTaskUrl = x.ExternalTaskUrl,
                    FinalEstimate = x.FinalEstimate,
                    Id = x.Id,
                    SessionId = x.SessionId,
                    TaskName = x.TaskName
                }).ToList();

                SessionWithGames session = new SessionWithGames()
                {
                    DateCreated = new UtcDateTime(sessionEntity.DateCreated),
                    EstimationUnit = sessionEntity.EstimationUnit,
                    Games = games,
                    Id = sessionEntity.Id,
                    IsFinished = games.Count > 0 && games.All(x => x.FinalEstimate.HasValue),
                    Moderator = new Moderator()
                    {
                        Email = sessionEntity.Moderator.Email,
                        FirstName = sessionEntity.Moderator.FirstName,
                        Id = sessionEntity.ModeratorId,
                        LastName = sessionEntity.Moderator.LastName
                    },
                    Title = sessionEntity.Title
                };

                sessions.Add(session);
            }

            return sessions;
        }
    }
}