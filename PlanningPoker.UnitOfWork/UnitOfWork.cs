using PlanningPoker.UnitOfWork.Abstractions;
using System;
using System.Data;

namespace PlanningPoker.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransactionProvider _transactionProvider;
        private ITransaction _transaction;
        private IsolationLevel? _isolationLevel;
        
        public bool HasOpenTransaction { get { return _transaction != null; } }
        
        public UnitOfWork(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider ?? throw new ArgumentNullException(nameof(transactionProvider));
        }
        
        private void StartNewTransactionIfNeeded()
        {
            if (_transaction == null)
            {
                if (_isolationLevel.HasValue)
                    _transaction = _transactionProvider.BeginTransaction(_isolationLevel.GetValueOrDefault());
                else
                    _transaction = _transactionProvider.BeginTransaction();
            }
        }

        public void BeginTransactionInstantly()
        {
            StartNewTransactionIfNeeded();
        }

        public void CommitTransaction()
        {
            //do not open transaction here, because if during the request
            //nothing was changed (only select queries were run), we don't
            //want to open and commit an empty transaction - calling SaveChanges()
            //on _transactionProvider will not send any sql to database in such case
            _transactionProvider.SaveChanges();

            if (_transaction != null)
            {
                _transaction.Commit();

                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();

            _transaction.Dispose();
            _transaction = null;
        }
        
        public int SaveChanges()
        {
            StartNewTransactionIfNeeded();

            return _transactionProvider.SaveChanges();
        }        

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
        }
    }
}
