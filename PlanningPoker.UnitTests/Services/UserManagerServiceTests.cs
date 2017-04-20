using Moq;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Constants;
using PlanningPoker.Domain.Enums;
using PlanningPoker.Domain.Models.Users;
using PlanningPoker.Domain.Providers.Transactions;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Services;
using PlanningPoker.Tests.Common.Stubs.Providers.Transactions;
using System.Threading.Tasks;
using Xunit;

namespace PlanningPoker.UnitTests.Services
{
    public class UserManagerServiceTests
    {
        private ITransactionProvider _transactionProvider;

        public UserManagerServiceTests()
        {
            _transactionProvider = new ITransactionProviderStub();
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
                               .ReturnsAsync((User)null);
            usersRepositoryMock.Setup(x => x.Create(It.IsAny<User>(), It.IsAny<string>()))
                               .ReturnsAsync(true);
            usersRepositoryMock.Setup(x => x.AddToRole(It.IsAny<User>(), It.IsAny<string>()))
                               .ReturnsAsync(true);

            UserManagerService userManagerService = new UserManagerService(usersRepositoryMock.Object, _transactionProvider);                      

            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(() => userManagerService.CreateUser(userToAdd));

            #endregion

            #region Assert

            //verify that no exception was thrown
            Assert.Null(exception);

            //verify that methods have been called with correct parameters and correct number of times
            usersRepositoryMock.Verify(x => x.FindByEmail(userToAdd.Email), Times.Once);
            usersRepositoryMock.Verify(x => x.Create(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            usersRepositoryMock.Verify(x => x.AddToRole(It.IsAny<User>(), userToAdd.Role), Times.Once);

            #endregion
        }
    }
}
