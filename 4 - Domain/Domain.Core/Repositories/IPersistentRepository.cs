using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.UnitOfWork;

namespace Domain.Core.Repositories
{
    public interface IPersistentRepository<T> : IRepository
    where T : Entity<T>
    {
        // IUoW UnitOfWork { get; }

        Task<T> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
        T Add(T obj);
        T Update(T obj);
        T Remove(T obj);
        Task<T> RemoveByIdAsync(Guid id);
        Task<ICollection<T>> DbSetQueryAsync(Expression<Func<T, bool>> predicate);

        Task<U> GetDbSetByIdAsync<U>(Guid id) where U : Entity<U>;
        U AddDbSet<U>(U obj) where U : Entity<U>;

        U UpdateDbSet<U>(U obj) where U : Entity<U>;
        U RemoveDbSet<U>(U obj) where U : Entity<U>;
        Task<U> RemoveDbSetByIdAsync<U>(Guid id) where U : Entity<U>;

        Task<T> GetByIdIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdIncludesAsync(Guid id, string[] includes);
    }
}