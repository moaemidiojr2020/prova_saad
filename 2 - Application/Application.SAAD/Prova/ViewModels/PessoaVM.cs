using Domain.SAAD.Prova.Enums;
using System;
using Domain.SAAD.Prova.Commands;

namespace Application.SAAD.Prova.ViewModels
{
	public class PessoaVM
	{
		public string nome;
		public EstadoCivilEnum EstadoCivil;
		public DateTime dataNasimento;
		public string nomeParceiro;
		public DateTime? dataNasimentoParceiro;

		public PessoaCommand MapperToPessoaCommand()
		{
			var command = new PessoaCommand
			{
				nome = nome,
				EstadoCivil = EstadoCivil,
				dataNasimento = dataNasimento,
				nomeParceiro = nomeParceiro,
				dataNasimentoParceiro = dataNasimentoParceiro
			};

			return command;
		}
	}
}
