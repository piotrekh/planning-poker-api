using Moq;
using PlanningPoker.DataAccess.Entities;
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
    }
}
