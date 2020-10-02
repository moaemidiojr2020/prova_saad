using System;
using Domain.Core.Commands;

namespace Domain.SAAD.Prova.Commands
{
	public class PessoaCommand : BasePessoaCommand
	{

	}

	public class EditarPessoaCommand : BasePessoaCommand
	{
		public Guid Id { get; set; }
	}

	public class ExcluirPessoaCommand : Command
	{
		public ExcluirPessoaCommand(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
