using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Exceptions;
using PlanningPoker.Domain.Exceptions.ExceptionReasons;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Domain.Services;
using PlanningPoker.UnitOfWork.Abstractions;
using System;

namespace PlanningPoker.Services
{
    public class GamesService : IGamesService
    {
        private readonly IGamesRepository _gamesRepository;
        private readonly IUnitOfWork _uow;

        public GamesService(IGamesRepository gamesRepository,
                            IUnitOfWork uow)
        {
            _gamesRepository = gamesRepository;
            _uow = uow;
        }

        public int BeginGame(int sessionId)
        {
            if (_gamesRepository.HasUnfinishedGameWithSessionId(sessionId))
                throw new ApplicationException(BeginGameExceptionReason.UnfinishedGameExists);

            Game game = new Game()
            {
                DateCreated = DateTime.UtcNow,
                SessionId = sessionId
            };

            _gamesRepository.Create(game);
            _uow.SaveChanges();

            return game.Id;
        }
    }
}
