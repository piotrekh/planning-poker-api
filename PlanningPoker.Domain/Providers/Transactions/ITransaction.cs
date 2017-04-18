using System;

namespace PlanningPoker.Domain.Providers.Transactions
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
