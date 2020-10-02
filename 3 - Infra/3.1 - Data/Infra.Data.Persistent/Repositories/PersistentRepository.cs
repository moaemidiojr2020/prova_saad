using System;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Core.UnitOfWork;
using Infra.Data.Persistent.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistent.Repositories
{
    public abstract class PersistentRepository<T> : IPersistentRepository<T>
    where T : Entity
    {
        private readonly EasyDbContext context;
        private readonly DbSet<T> DbSet;

        protected PersistentRepository(EasyDbContext context)
        {
            this.context = context;
            DbSet = this.context.Set<T>();
        }
        public IUoW UnitOfWork => context;

        public virtual  T Add(T obj)
        {
            DbSet.Add(obj);

            return obj;
        }

        public virtual T Update(T obj)
        {
            DbSet.Update(obj);

            return obj;
        }

        public virtual T Remove(T obj)
        {
             DbSet.Remove(obj);

            return obj;
        }


        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}