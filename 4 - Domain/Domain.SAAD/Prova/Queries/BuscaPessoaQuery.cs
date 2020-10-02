using System;
using Domain.SAAD.Prova.Enums;

namespace Domain.SAAD.Prova.Queries
{
	public struct BuscaPessoaQuery
	{
		public Guid Id;
		public string nome;
		public EstadoCivilEnum EstadoCivil;
		public DateTime dataNascimento;
		public string nomeParceiro;
		public DateTime? dataNascimentoParceiro;
		
	

		public BuscaPessoaQuery(Guid id, string nome, EstadoCivilEnum estadoCivil, DateTime dataNascimento, string nomeParceiro, DateTime? dataNascimentoParceiro) : this()
		{
			Id = id;
			this.nome = nome;
			EstadoCivil = estadoCivil;
			this.dataNascimento = dataNascimento;
			this.nomeParceiro = nomeParceiro;
			this.dataNascimentoParceiro = dataNascimentoParceiro;
		}
	}
}
