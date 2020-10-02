using System;
using Domain.SAAD.Prova.Commands;
using Domain.SAAD.Prova.Enums;


namespace Application.SAAD.Prova.ViewModels
{
	public class AlterarPessoaVM
	{
		public Guid id;
		public string nome;
		public EstadoCivilEnum EstadoCivil;
		public DateTime dataNasimento;
		public string nomeParceiro;
		public DateTime? dataNasimentoParceiro;

		public EditarPessoaCommand MapperToEditarPessoaCommand()
		{
			var command = new EditarPessoaCommand
			{
				Id = id,
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
