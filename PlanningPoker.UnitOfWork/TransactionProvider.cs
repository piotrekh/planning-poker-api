using Microsoft.EntityFrameworkCore;
using PlanningPoker.UnitOfWork.Abstractions;
using System.Data;

namespace PlanningPoker.UnitOfWork
{
    public class TransactionProvider : ITransactionProvider
    {
        private DbContext _context;

        public TransactionProvider(DbContext context)
        {
            _context = context;
        }

        public ITransaction BeginTransaction()
        {
            var dbTransaction = _context.Database.BeginTransaction();
            return new Transaction(dbTransaction);
        }

        public ITransaction BeginTransaction(IsolationLevel level)
        {
            var dbTransaction = _context.Database.BeginTransaction(level);
            return new Transaction(dbTransaction);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
