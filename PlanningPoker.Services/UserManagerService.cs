using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Enums;
using PlanningPoker.Domain.Exceptions;
using PlanningPoker.Domain.Models.Users;
using PlanningPoker.Domain.Providers.Transactions;
using PlanningPoker.Domain.Repositories;
using PlanningPoker.Domain.Services;
using System.Threading.Tasks;

namespace PlanningPoker.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITransactionProvider _transactionProvider;

        public UserManagerService(IUsersRepository usersRepository, ITransactionProvider transactionProvider)
        {
            _usersRepository = usersRepository;
            _transactionProvider = transactionProvider;
        }
        
        public async Task CreateUser(CreateUser data)
        {
            //check if a user with specified email already exists
            User existingUser = await _usersRepository.FindByEmail(data.Email);
            if (existingUser != null)
                throw new ApplicationException(CreateUserExceptionReason.UserAlreadyExists);                

            User user = new User()
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                UserName = data.Email
            };

            using (var transaction = _transactionProvider.BeginTransaction())
            {
                try
                {
                    //create user and add them to the specified role
                    bool createResult = await _usersRepository.Create(user, data.Password);

                    if (createResult)
                    {
                        bool addToRoleResult = await _usersRepository.AddToRole(user, data.Role);

                        if (addToRoleResult)
                            transaction.Commit();
                        else
                            throw new ApplicationException(CreateUserExceptionReason.UnspecifiedError);
                    }
                    else
                    {
                        throw new ApplicationException(CreateUserExceptionReason.UnspecifiedError);
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
