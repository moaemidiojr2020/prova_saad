using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Infra.Data.Core.ConnectionStrings;
using Infra.Data.Persistent.Mappings.Prova;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistent.Contexts
{
    public class SaadDbContext : DbContext, IDisposable
    {

        public SaadDbContext() { }

        protected SaadDbContext(string connString)
        {
            this.connString = connString;
        }
        protected readonly string connString;
        public SaadDbContext(ConnectionStringHandler connectionStringHandler
        , DbContextOptions options) : base(options)
        {
            connString = connectionStringHandler.GetConnectionString();
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfiguration(new PessoaRootMap());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            const string chaveID = "Id";

            AddedEntities.ForEach(E =>
            {
                foreach (var item in E.Properties)
                {
                    if (item.Metadata.Name == chaveID && item.CurrentValue == null)
                    {
                        item.CurrentValue = Guid.NewGuid();
                        continue;
                    }
                }
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
        }


    }
}