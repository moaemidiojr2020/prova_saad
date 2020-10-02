using System;
using System.Threading.Tasks;
using Domain.Core.UnitOfWork;
using FluentValidation.Results;
using Infra.Data.Persistent.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infra.Data.Persistent.Contexts
{
    public class EasyDbContext : DbContext, IUoW, IDisposable
    {

        private IDbContextTransaction _transaction;

        public EasyDbContext(DbContextOptions options) : base(options)
        {
        }

        public EasyDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string _connString = "Server=tcp:easy-dev-db-server.database.windows.net,1433;Initial Catalog=easy-dev-db;Persist Security Info=False;User ID=easydelivery;Password=jVwv8dBhNf3W;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfiguration(new ExemploRootMap());

            base.OnModelCreating(modelBuilder);
        }

        protected async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null) return;

            _transaction = await this.Database.BeginTransactionAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await base.SaveChangesAsync();

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

        public override void Dispose()
        {
            _transaction?.Dispose();
            base.Dispose();
        }

    }
}