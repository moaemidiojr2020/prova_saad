using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Commands;
using Domain.Core.Operacoes;
using Domain.SAAD.Prova.Models;
using Domain.SAAD.Prova.Queries;
using Domain.SAAD.Prova.Repositories;
using MediatR;

namespace Domain.SAAD.Prova.Commands
{
	public class PessoaCommandHandler : CommandHandler,
		IRequestHandler<PessoaCommand, RespostaCommand>,
		IRequestHandler<EditarPessoaCommand, RespostaCommand>,
		IRequestHandler<ExcluirPessoaCommand, RespostaCommand>
	{
		public PessoaCommandHandler(IPessoaPersistentRepository pessoaPersistentRepository, IPessoaReadOnlyRepository pessoaReadOnlyRepository)
		{
			_pessoaPersistentRepository = pessoaPersistentRepository;
			_pessoaReadOnlyRepository = pessoaReadOnlyRepository;
		}

		private readonly IPessoaPersistentRepository _pessoaPersistentRepository;
		private readonly IPessoaReadOnlyRepository _pessoaReadOnlyRepository;

		public async Task<RespostaCommand> Handle(PessoaCommand request, CancellationToken cancellationToken)
		{
			var pessoa = new PessoaRoot(request);

			base.RespostaCommand.ValidationResult = pessoa.ValidarNovaPessoa();

			if (!base.IsValid) return base.RespostaCommand;

			_pessoaPersistentRepository.Add(pessoa);

			await _pessoaPersistentRepository.SaveChangesAsync();

			base.RespostaCommand.Dados = new CadastrarPessoaQuery(
				pessoa.Nome,
				pessoa.EstadoCivil,
				pessoa.DataNascimento,
				pessoa.NomeParceiro,
				pessoa.DataNascimentoParceiro);

			return base.RespostaCommand;
		}

		public async Task<RespostaCommand> Handle(ExcluirPessoaCommand request, CancellationToken cancellationToken)
		{
			_pessoaPersistentRepository.RemoveByIdAsync(request.Id);

			await _pessoaPersistentRepository.SaveChangesAsync();

			base.RespostaCommand.Dados = new ExcluirPessoaCommand(
				request.Id);

			return base.RespostaCommand;
		}

		public async Task<RespostaCommand> Handle(EditarPessoaCommand request, CancellationToken cancellationToken)
		{
			var model = new PessoaRoot(request.nome,
				request.EstadoCivil,
				request.dataNasimento,
				request.nomeParceiro,
				request.dataNasimentoParceiro);

			base.RespostaCommand.ValidationResult = model.ValidarNovaPessoa();

			if (!base.IsValid) return base.RespostaCommand;

			var pessoa = await _pessoaPersistentRepository.GetByIdAsync(request.Id);
			pessoa.AlterarPessoa(request.nome,
				request.EstadoCivil,
				request.dataNasimento,
				request.nomeParceiro,
				request.dataNasimentoParceiro);

			_pessoaPersistentRepository.Add(pessoa);
			_pessoaPersistentRepository.UpdateDbSet(pessoa);

			await _pessoaPersistentRepository.SaveChangesAsync();

			base.RespostaCommand.Dados = new BuscaPessoaQuery(
				request.Id,
				request.nome,
				request.EstadoCivil,
				request.dataNasimento,
				request.nomeParceiro,
				request.dataNasimentoParceiro);

			return base.RespostaCommand;
		}
	}
}

