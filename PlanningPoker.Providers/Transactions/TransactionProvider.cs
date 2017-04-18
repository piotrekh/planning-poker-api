using Microsoft.EntityFrameworkCore;
using PlanningPoker.DataAccess;
using PlanningPoker.Domain.Providers.Transactions;
using System.Data;

namespace PlanningPoker.Providers.Transactions
{
    public class TransactionProvider : ITransactionProvider
    {
        private PlanningPokerDbContext _context;

        public TransactionProvider(PlanningPokerDbContext context)
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
