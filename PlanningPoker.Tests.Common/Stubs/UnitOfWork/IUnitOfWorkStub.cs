using PlanningPoker.UnitOfWork.Abstractions;
using System.Data;

namespace PlanningPoker.Tests.Common.Stubs.UnitOfWork
{
    public class IUnitOfWorkStub : IUnitOfWork
    {
        public bool HasOpenTransaction => true;

        public void BeginTransactionInstantly()
        {
        }

        public void CommitTransaction()
        {
        }

        public void RollbackTransaction()
        {
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
        }
    }
}
