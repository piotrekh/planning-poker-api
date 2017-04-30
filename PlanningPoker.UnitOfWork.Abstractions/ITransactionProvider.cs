using System.Data;

namespace PlanningPoker.UnitOfWork.Abstractions
{
    public interface ITransactionProvider
    {
        ITransaction BeginTransaction();

        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        int SaveChanges();
    }
}
