using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Enums;
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
        
        public async Task<CreateUserResult> CreateUser(CreateUser data)
        {
            //check if a user with specified email already exists
            User existingUser = await _usersRepository.FindByEmail(data.Email);
            if (existingUser != null)
                return CreateUserResult.UserAlreadyExists;

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
                    var result = await _usersRepository.Create(user, data.Password);

                    if (result)
                    {
                        await _usersRepository.AddToRole(user, data.Role);
                        transaction.Commit();
                        return CreateUserResult.Success;
                    }
                    else
                    {
                        transaction.Rollback();
                        return CreateUserResult.UnspecifiedError;
                    }
                }
                catch
                {
                    transaction.Rollback();
                    return CreateUserResult.UnspecifiedError;
                }
            }
        }
    }
}
