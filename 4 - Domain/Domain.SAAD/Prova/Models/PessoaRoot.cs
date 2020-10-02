using System;
using Domain.Core.Entities;
using Domain.SAAD.Prova.Commands;
using Domain.SAAD.Prova.Enums;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.SAAD.Prova.Models
{
	public class PessoaRoot : Entity<PessoaRoot>
	{

		public PessoaRoot(string nome, EstadoCivilEnum estadoCivil, DateTime dataNascimento, string nomeParceiro, DateTime? dataNascimentoParceiro)
		{
			Nome = nome;
			EstadoCivil = estadoCivil;
			DataNascimento = dataNascimento;
			NomeParceiro = nomeParceiro;
			DataNascimentoParceiro = dataNascimentoParceiro;
		}

		public PessoaRoot(PessoaCommand request)
		{
			Nome = request.nome;
			EstadoCivil = request.EstadoCivil;
			DataNascimento = request.dataNasimento;
			NomeParceiro = request.nomeParceiro;
			DataNascimentoParceiro = request.dataNasimentoParceiro;
		}

		public string Nome { get; protected set; }
		public EstadoCivilEnum EstadoCivil { get; protected set; }
		public DateTime DataNascimento { get; protected set; }
		public string NomeParceiro { get; protected set; }
		public DateTime? DataNascimentoParceiro { get; protected set; }

		protected void ValidarNome()
		{
			this.RuleFor(p => p.Nome)
				.NotEmpty().WithMessage("O Campo Nome não pode ser vazio");
		}

		protected void ValidarDataNascimento()
		{
			this.RuleFor(p => p.DataNascimento)
				.NotEmpty().WithMessage("O Campo Data de Nascimento não pode ser vazio");
		}

		public ValidationResult ValidarNovaPessoa()
		{
			this.ValidarNome();
			this.ValidarDataNascimento();
			this.ValidationResult = base.Validate(this);
			return this.ValidationResult;
		}

		public void AlterarPessoa(string nome, EstadoCivilEnum estadoCivil, DateTime dataNascimento, string nomeParceiro, DateTime? dataNascimentoParceiro)
		{
			Nome = nome;
			EstadoCivil = EstadoCivil;
			DataNascimento = dataNascimento;
			NomeParceiro = nomeParceiro;
			DataNascimentoParceiro = dataNascimentoParceiro;
		}
	}
}
