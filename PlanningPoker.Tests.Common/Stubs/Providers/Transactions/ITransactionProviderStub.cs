using PlanningPoker.Domain.Providers.Transactions;

namespace PlanningPoker.Tests.Common.Stubs.Providers.Transactions
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
