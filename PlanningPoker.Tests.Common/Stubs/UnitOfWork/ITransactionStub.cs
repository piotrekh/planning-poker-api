using PlanningPoker.UnitOfWork.Abstractions;

namespace PlanningPoker.Tests.Common.Stubs.UnitOfWork
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
