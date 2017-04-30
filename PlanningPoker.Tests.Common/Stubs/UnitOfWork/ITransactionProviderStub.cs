using PlanningPoker.UnitOfWork.Abstractions;

namespace PlanningPoker.Tests.Common.Stubs.UnitOfWork
{
    public class ITransactionProviderStub : ITransactionProvider
    {
        public ITransaction BeginTransaction()
        {
            return new ITransactionStub();
        }

        public ITransaction BeginTransaction(System.Data.IsolationLevel level)
        {
            return new ITransactionStub();
        }

        public int SaveChanges()
        {
            return 1;
        }
    }
}
