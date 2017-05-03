using Moq;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Exceptions;
using PlanningPoker.Domain.Exceptions.ExceptionReasons;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Services;
using PlanningPoker.Tests.Common.Stubs.UnitOfWork;
using PlanningPoker.UnitOfWork.Abstractions;
using Xunit;

namespace PlanningPoker.UnitTests.Services
{
    public class GamesServiceTests
    {
        private IUnitOfWork _uow;

        public GamesServiceTests()
        {
            _uow = new IUnitOfWorkStub();
        }

        [Fact]
        public void BeginGame_Should_CreateNewGame_When_SessionHasNoUnfinishedGames()
        {
            #region Arrange

            int sessionId = 1;
            int expectedGameId = 1;            

            Mock<IGamesRepository> gamesRepositoryMock = new Mock<IGamesRepository>();
            gamesRepositoryMock.Setup(x => x.HasUnfinishedGameWithSessionId(sessionId))
                               .Returns(false);
            gamesRepositoryMock.Setup(x => x.Create(It.IsAny<Game>()))
                .Callback<Game>(x => x.Id = expectedGameId);

            GamesService gamesService = new GamesService(gamesRepositoryMock.Object, _uow);

            #endregion

            #region Act

            int actualGameId = gamesService.BeginGame(sessionId);

            #endregion

            #region Assert

            Assert.Equal(expectedGameId, actualGameId);
            gamesRepositoryMock.Verify(x => x.HasUnfinishedGameWithSessionId(sessionId), Times.Once);
            gamesRepositoryMock.Verify(x => x.Create(It.IsAny<Game>()), Times.Once);

            #endregion
        }

        [Fact]
        public void BeginGame_Should_ThrowException_When_SessionHasUnfinishedGame()
        {
            #region Arrange

            int sessionId = 1;

            Mock<IGamesRepository> gamesRepositoryMock = new Mock<IGamesRepository>();
            gamesRepositoryMock.Setup(x => x.HasUnfinishedGameWithSessionId(sessionId))
                               .Returns(true);

            GamesService gamesService = new GamesService(gamesRepositoryMock.Object, _uow);

            #endregion

            #region Act
            
            var exception = Record.Exception(() => gamesService.BeginGame(sessionId));
            var applicationException = exception as ApplicationException;

            #endregion

            #region Assert

            //check if an exception has been thrown 
            Assert.NotNull(exception);
            //check if the exception thrown is of appropriate type
            Assert.NotNull(applicationException);
            //check the reason of the exception            
            Assert.Equal(BeginGameExceptionReason.UnfinishedGameExists, (BeginGameExceptionReason)applicationException.Reason);
            
            gamesRepositoryMock.Verify(x => x.HasUnfinishedGameWithSessionId(sessionId), Times.Once);
            gamesRepositoryMock.Verify(x => x.Create(It.IsAny<Game>()), Times.Never);

            #endregion
        }
    }
}
