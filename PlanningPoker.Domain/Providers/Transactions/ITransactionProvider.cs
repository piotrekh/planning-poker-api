using System.Data;

namespace PlanningPoker.Domain.Providers.Transactions
{
    public interface ITransactionProvider
    {
        ITransaction BeginTransaction();

        ITransaction BeginTransaction(IsolationLevel level);

        int SaveChanges();
    }
}
