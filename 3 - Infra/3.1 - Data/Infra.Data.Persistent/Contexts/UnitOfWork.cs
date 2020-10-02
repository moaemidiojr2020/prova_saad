using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Infra.Data.Persistent.Contexts
{
    public class UnitOfWork : IUoW
    {
        private IDbContextTransaction _transaction;
        private CancellationToken _transactionCancellationToken;

        private readonly SaadDbContext _easyDbContext;

        public UnitOfWork(SaadDbContext easyDbContext)
        {
            _easyDbContext = easyDbContext;
        }

        public async Task RollBackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transactionCancellationToken = cancellationToken;

            _transaction = await _easyDbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await _easyDbContext.SaveChangesAsync(true, _transactionCancellationToken);
                return result > 0;
            }
            catch (System.Exception e)
            {
                //TODO logar erros
                throw;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (System.Exception e)
            {
                //TODO logar erros
                await RollBackAsync();
                throw e;
            }
        }

        public async Task<T> DbSetGetByIdAsync<T>(Guid id) where T : Entity<T>
        {
            var obj = await _easyDbContext.Set<T>().FindAsync(id);

            return obj;
        }

        public async Task<T> DbSetGetByIdIncludesAsync<T>(Guid id,
        params Expression<Func<T, object>>[] includes)
        where T : Entity<T>
        {
            var entity = _easyDbContext.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                entity = entity.Include(include);
            }

            var obj = await entity.Where(x => x.Id == id).FirstOrDefaultAsync();

            return obj;
        }

        public async Task<T> DbSetGetWithIncludeAsync<T>(Guid id,
                string[] includes)
                where T : Entity<T>
        {
            var entity = _easyDbContext.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                entity = entity.Include(include);
            }

            var obj = await entity.Where(x => x.Id == id).FirstOrDefaultAsync();

            return obj;
        }

        public void DbSetAdd<T>(T obj) where T : Entity<T>
        {
            _easyDbContext.Set<T>().Add(obj);
        }

        public void DbSetUpdate<T>(T obj) where T : Entity<T>
        {
            _easyDbContext.Set<T>().Update(obj);
        }

        public void DbSetRemove<T>(T obj) where T : Entity<T>
        {
            _easyDbContext.Set<T>().Remove(obj);
        }

        public async Task<ICollection<T>> DbSetQueryAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity<T>
        {
            var query = _easyDbContext.Set<T>().Where(predicate).ToArrayAsync();

            return await query;
        }

        public virtual void Dispose()
        {
            if (this._transaction != null)
                _transaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}