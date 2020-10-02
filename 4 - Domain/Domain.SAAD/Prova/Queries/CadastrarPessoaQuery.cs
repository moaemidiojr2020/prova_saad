using System;
using Domain.SAAD.Prova.Enums;

namespace Domain.SAAD.Prova.Queries
{
	public class CadastrarPessoaQuery
	{
		public CadastrarPessoaQuery(string nome, EstadoCivilEnum estadoCivil, DateTime dataNasimento, string nomeParceiro, DateTime? dataNasimentoParceiro)
		{
			this.nome = nome;
			EstadoCivil = estadoCivil;
			this.dataNasimento = dataNasimento;
			this.nomeParceiro = nomeParceiro;
			this.dataNasimentoParceiro = dataNasimentoParceiro;
		}

		public string nome;
		public EstadoCivilEnum EstadoCivil;
		public DateTime dataNasimento;
		public string nomeParceiro;
		public DateTime? dataNasimentoParceiro;
	}
}
