using Domain.Core.Repositories;
using Domain.EasyDelivery.BoundedContextExemplos1.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistent.Mappings
{
    public class ExemploRootMap : IEntityTypeConfiguration<ExemploRoot>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExemploRoot> builder)
        {
            builder.ToTable("Exemplos", SchemasConstants.EASY_SCHEMA);
            
             builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();  

            
            builder.Property(c => c.DataNascimento)
                .HasColumnType("datetime")
                .IsRequired();   
        }
    }
}