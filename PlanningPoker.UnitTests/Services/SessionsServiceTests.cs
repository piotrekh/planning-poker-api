using Moq;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Enums;
using PlanningPoker.Domain.Models.Sessions;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Services;
using PlanningPoker.Tests.Common.Stubs.UnitOfWork;
using PlanningPoker.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PlanningPoker.UnitTests.Services
{
    public class SessionsServiceTests
    {
        private IUnitOfWork _uow;

        public SessionsServiceTests()
        {
            _uow = new IUnitOfWorkStub();
        }

        [Fact]
        public void CreateSession_Should_Succeed_When_AddingNewSession()
        {
            #region Arrange

            int userId = 1;
            CreateSession sessionToAdd = new CreateSession()
            {
                Title = "Session title",
                EstimationUnit = Domain.Enums.EstimationUnit.ManHour,
                PlayersIds = new List<int>() { 1, 2, 3 }
            };

            Session addedSessionEntity = null;

            Mock<ISessionsRepository> sessionsRepositoryMock = new Mock<ISessionsRepository>();
            sessionsRepositoryMock.Setup(x => x.Create(It.IsAny<Session>()))
                                  .Callback<Session>(session => addedSessionEntity = session);

            SessionsService sessionsService = new SessionsService(sessionsRepositoryMock.Object, null, _uow);

            #endregion

            #region Act

            sessionsService.CreateSession(userId, sessionToAdd);

            #endregion

            #region Assert

            Assert.Equal(userId, addedSessionEntity.ModeratorId);
            Assert.Equal(sessionToAdd.Title, addedSessionEntity.Title);
            Assert.Equal(sessionToAdd.EstimationUnit.ToString(), addedSessionEntity.EstimationUnit);
            //make sure the DateCreated was set
            Assert.NotEqual(default(DateTime), addedSessionEntity.DateCreated);
            //check if players were added
            Assert.NotNull(addedSessionEntity.Players);
            Assert.Equal(sessionToAdd.PlayersIds.Count, addedSessionEntity.Players.Count);
            foreach(var addedPlayer in addedSessionEntity.Players)            
                Assert.Contains(addedPlayer.UserId, sessionToAdd.PlayersIds);            

            #endregion
        }
        
        [Fact]
        public void GetUserSessions_Should_ReturnUserFinishedSessions_When_QueryingFinishedSessions()
        {
            #region Arrange

            //arrange a session in which user is participating
            int userId = 1;
            Session sessionEntity = new Session()
            {
                Games = new List<Game>(),
                DateCreated = DateTime.UtcNow,
                EstimationUnit = EstimationUnit.ManHour.ToString(),
                Id = 1,
                Moderator = new DataAccess.Entities.User()
                {
                    Id = userId,
                    Email = "test@test.com",
                    FirstName = "John",
                    LastName = "Doe"
                },
                ModeratorId = userId,
                Title = "Test session 1",
                Players = new List<SessionPlayer>()
                {
                    new SessionPlayer()
                    {
                        SessionId = 1,
                        UserId = userId
                    }
                },
                IsFinished = false,
                LiveSessionId = Guid.NewGuid()
            };

            Mock<ISessionsRepository> sessionsRepositoryMock = new Mock<ISessionsRepository>();
            sessionsRepositoryMock.Setup(x => x.GetUserSessions(userId, true))
                                  .Returns(new List<Session>() { sessionEntity });

            SessionsService sessionsService = new SessionsService(sessionsRepositoryMock.Object, null, _uow);

            #endregion

            #region Act

            List<SessionWithGames> sessions = sessionsService.GetUserSessions(1, true);

            #endregion

            #region Assert

            sessionsRepositoryMock.Verify(x => x.GetUserSessions(userId, true), Times.Once);

            Assert.Equal(1, sessions.Count);
            Assert.NotNull(sessions.First().Games);
            Assert.Equal(0, sessions.First().Games.Count);
            Assert.False(sessions.First().IsFinished);

            #endregion
        }
    }
}
