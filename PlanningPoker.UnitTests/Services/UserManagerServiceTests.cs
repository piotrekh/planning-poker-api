﻿using Moq;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Constants;
using PlanningPoker.Domain.Models.Users;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Services;
using PlanningPoker.Tests.Common.Stubs.UnitOfWork;
using PlanningPoker.UnitOfWork.Abstractions;
using System.Threading.Tasks;
using Xunit;

namespace PlanningPoker.UnitTests.Services
{
    public class UserManagerServiceTests
    {
        private IUnitOfWork _uow;

        public UserManagerServiceTests()
        {
            _uow = new IUnitOfWorkStub();
        }

        [Fact]
        public async Task CreateUser_Should_Succeed_When_AddingNewUser()
        {
            #region Arrange

            CreateUser userToAdd = new CreateUser()
            {
                Email = "test@test.com",
                FirstName = "Test_FirstName",
                LastName = "Test_LastName",
                Password = "!Qaz123",
                Role = Roles.Player
            };

            //setup IUsersRepository to find no existing user and to succeed when creating
            //a new one and adding them to a new role
            Mock<IUsersRepository> usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock.Setup(x => x.FindByEmail(It.IsAny<string>()))
                               .ReturnsAsync((DataAccess.Entities.User)null);
            usersRepositoryMock.Setup(x => x.Create(It.IsAny<DataAccess.Entities.User>(), It.IsAny<string>()))
                               .ReturnsAsync(true);
            usersRepositoryMock.Setup(x => x.AddToRole(It.IsAny<DataAccess.Entities.User>(), It.IsAny<string>()))
                               .ReturnsAsync(true);

            UserManagerService userManagerService = new UserManagerService(usersRepositoryMock.Object, _uow);                      

            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(() => userManagerService.CreateUser(userToAdd));

            #endregion

            #region Assert

            //verify that no exception was thrown
            Assert.Null(exception);

            //verify that methods have been called with correct parameters and correct number of times
            usersRepositoryMock.Verify(x => x.FindByEmail(userToAdd.Email), Times.Once);
            usersRepositoryMock.Verify(x => x.Create(It.IsAny<DataAccess.Entities.User>(), It.IsAny<string>()), Times.Once);
            usersRepositoryMock.Verify(x => x.AddToRole(It.IsAny<DataAccess.Entities.User>(), userToAdd.Role), Times.Once);

            #endregion
        }
    }
}
