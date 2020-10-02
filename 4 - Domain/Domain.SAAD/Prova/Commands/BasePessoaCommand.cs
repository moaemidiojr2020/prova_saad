using System;
using Domain.Core.Commands;
using Domain.SAAD.Prova.Enums;

namespace Domain.SAAD.Prova.Commands
{
	public class BasePessoaCommand : Command
	{
		public string nome { get; set; }
		public EstadoCivilEnum EstadoCivil { get; set; }
		public DateTime dataNasimento { get; set; }
		public string nomeParceiro { get; set; }
		public DateTime? dataNasimentoParceiro { get; set; }
	}
}
