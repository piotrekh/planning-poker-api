using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Enums;
using PlanningPoker.Domain.Exceptions;
using PlanningPoker.Domain.Providers.Transactions;
using PlanningPoker.Domain.Repositories;
using System;

namespace PlanningPoker.Domain.Services
{
    public class GamesService : IGamesService
    {
        private readonly IGamesRepository _gamesRepository;
        private readonly ITransactionProvider _transactionProvider;

        public GamesService(IGamesRepository gamesRepository, ITransactionProvider transactionProvider)
        {
            _gamesRepository = gamesRepository;
            _transactionProvider = transactionProvider;
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
            _transactionProvider.SaveChanges();

            return game.Id;
        }
    }
}
