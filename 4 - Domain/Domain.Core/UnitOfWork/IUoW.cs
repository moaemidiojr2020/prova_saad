using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Entities;

namespace Domain.Core.UnitOfWork
{
    public interface IUoW : IDisposable
    {
        Task<bool> SaveChangesAsync();
        Task CommitAsync();
        Task RollBackAsync();
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task<T> DbSetGetByIdAsync<T>(Guid id) where T : Entity<T>;
        Task<ICollection<T>> DbSetQueryAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity<T>;

        void DbSetAdd<T>(T obj) where T : Entity<T>;
        void DbSetUpdate<T>(T obj) where T : Entity<T>;
        void DbSetRemove<T>(T obj) where T : Entity<T>;

        Task<T> DbSetGetByIdIncludesAsync<T>(Guid id,
         params Expression<Func<T, object>>[] includes) where T : Entity<T>;
        Task<T> DbSetGetWithIncludeAsync<T>(Guid id, string[] includes) where T : Entity<T>;

    }
}