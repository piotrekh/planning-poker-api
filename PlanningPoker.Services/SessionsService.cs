using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Exceptions;
using PlanningPoker.Domain.Exceptions.ExceptionReasons;
using PlanningPoker.Domain.Models;
using PlanningPoker.Domain.Models.Sessions;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Domain.Services;
using PlanningPoker.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanningPoker.Services
{
    public class SessionsService : ISessionsService
    {
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IGamesRepository _gamesRepository;
        private readonly IUnitOfWork _uow;

        public SessionsService(ISessionsRepository sessionsRepository,
                               IGamesRepository gamesRepository,
                               IUnitOfWork uow)
        {
            _sessionsRepository = sessionsRepository;
            _gamesRepository = gamesRepository;
            _uow = uow;
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
                Players = players,
                IsFinished = false,
                LiveSessionId = Guid.NewGuid()
            };

            _sessionsRepository.Create(sessionEntity);

            _uow.SaveChanges();
        }

        public List<SessionWithGames> GetUserSessions(int userId, bool finished)
        {
            List<Session> sessionEntities = _sessionsRepository.GetUserSessions(userId, finished);

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
                    IsFinished = sessionEntity.IsFinished,
                    Moderator = new Domain.Models.Users.User()
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

        public LiveSession JoinLiveSession(int sessionId)
        {
            Session session = _sessionsRepository.GetSessionWithPlayers(sessionId);

            if (session.IsFinished)
                throw new ApplicationException(JoinLiveSessionExceptionReason.SessionIsFinished);
            
            //get the currently played game
            Game game = _gamesRepository.GetCurrentGameWithEstimatesForSession(sessionId);

            LiveSession liveSession = new LiveSession()
            {
                SessionId = session.Id,
                LiveSessionId = session.LiveSessionId,
                ModeratorId = session.ModeratorId,
                Players = session.Players.Select(x => new Domain.Models.Users.User()
                {
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    Id = x.UserId,
                    LastName = x.User.LastName
                }).ToList()
            };

            if(game != null)
            {
                liveSession.CurrentGame = new Domain.Models.Games.GameWithEstimates()
                {
                    ExternalTaskUrl = game.ExternalTaskUrl,
                    FinalEstimate = game.FinalEstimate,
                    Id = game.Id,
                    TaskName = game.TaskName
                };

                //make sure to return estimates (or their lack) for all players
                liveSession.CurrentGame.PlayerEstimates = liveSession.Players.Select(player => new Domain.Models.Games.GameEstimate()
                {
                    UserId = player.Id,
                    Estimate = game.Estimates.FirstOrDefault(estimate => estimate.UserId == player.Id)?.Estimate
                }).ToList();                
            }

            return liveSession;
        }
    }
}
