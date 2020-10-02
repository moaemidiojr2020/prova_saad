using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Core.UnitOfWork;
using Infra.Data.Persistent.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistent.Repositories
{
    public abstract class PersistentRepository<T> : IPersistentRepository<T>
    where T : Entity<T>
    {
        public readonly IUoW _uow;

        protected PersistentRepository(IUoW uow)
        {
            this._uow = uow;
        }

        public virtual T Add(T obj)
        {
            this._uow.DbSetAdd(obj);

            return obj;
        }

        public virtual T Update(T obj)
        {
            this._uow.DbSetUpdate(obj);


            return obj;
        }

        public virtual T Remove(T obj)
        {
            this._uow.DbSetRemove(obj);

            return obj;
        }

        public virtual async Task<T> RemoveByIdAsync(Guid id)
        {
            var obj = await GetByIdAsync(id);
            this._uow.DbSetRemove(obj);

            return obj;
        }


        public async Task<T> GetByIdAsync(Guid id)
        {
            var obj = await this._uow.DbSetGetByIdAsync<T>(id);

            return obj;
        }

        public async Task<ICollection<T>> DbSetQueryAsync(Expression<Func<T, bool>> predicate)
        {
            var list = await this._uow.DbSetQueryAsync(predicate);

            return list;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _uow.SaveChangesAsync();
        }

        public async Task<U> GetDbSetByIdAsync<U>(Guid id) where U : Entity<U>
        {
            return await this._uow.DbSetGetByIdAsync<U>(id);
        }
        public U AddDbSet<U>(U obj) where U : Entity<U>
        {
            this._uow.DbSetAdd(obj);

            return obj;
        }

        public U UpdateDbSet<U>(U obj) where U : Entity<U>
        {
            this._uow.DbSetUpdate(obj);

            return obj;
        }

        public U RemoveDbSet<U>(U obj) where U : Entity<U>
        {
            this._uow.DbSetRemove(obj);

            return obj;
        }

        public async Task<U> RemoveDbSetByIdAsync<U>(Guid id) where U : Entity<U>
        {
            var obj = await this._uow.DbSetGetByIdAsync<U>(id);

            this._uow.DbSetRemove(obj);

            return obj;
        }

        public async Task<T> GetByIdIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            var obj = await this._uow.DbSetGetByIdIncludesAsync(id, includes);
           
            return obj;
        }

         public async Task<T> GetByIdIncludesAsync(Guid id, string[] includes)
        {
            var obj = await this._uow.DbSetGetWithIncludeAsync<T>(id, includes);
           
            return obj;
        }
    }
}