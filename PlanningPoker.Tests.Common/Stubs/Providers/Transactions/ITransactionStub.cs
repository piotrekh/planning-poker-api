using PlanningPoker.Domain.Providers.Transactions;

namespace PlanningPoker.Tests.Common.Stubs.Providers.Transactions
{
    public class ITransactionStub : ITransaction
    {
        public void Commit()
        {            
        }

        public void Dispose()
        {            
        }

        public void Rollback()
        {            
        }
    }
}
