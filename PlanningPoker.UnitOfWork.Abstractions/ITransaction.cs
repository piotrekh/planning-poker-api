using System;

namespace PlanningPoker.UnitOfWork.Abstractions
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
