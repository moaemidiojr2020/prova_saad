using Domain.Core.Repositories;
using Domain.SAAD.Prova.Models;
using Infra.Data.Persistent.Mappings.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Persistent.Mappings.Prova
{
	public class PessoaRootMap : BaseEntityMap<PessoaRoot>
	{
		public override void Configure(EntityTypeBuilder<PessoaRoot> builder)
		{
			base.Configure(builder);

			builder.ToTable("Pessoa",
				SchemasConstants.ProvaContext);

			builder.HasKey(c => c.Id);

			builder.Property(c => c.Nome)
				.HasColumnType("nvarchar(100)")
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(c => c.DataNascimento)
				.HasColumnType("datetime2")
				.IsRequired();

			builder.Property(c => c.EstadoCivil)
				.HasConversion<string>().HasMaxLength(50);

			builder.Property(c => c.NomeParceiro)
				.HasColumnType("nvarchar(100)")
				.HasMaxLength(100);

			builder.Property(c => c.DataNascimentoParceiro)
				.HasColumnType("datetime2");
		}
	}
}
