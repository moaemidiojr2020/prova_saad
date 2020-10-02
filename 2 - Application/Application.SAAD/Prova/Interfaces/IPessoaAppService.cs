using System;
using System.Threading.Tasks;
using Application.SAAD.Core;
using Application.SAAD.Prova.ViewModels;
using Domain.Core.Operacoes;

namespace Application.SAAD.Prova.Interfaces
{
	public interface IPessoaAppService : IBaseAppService
	{
		Task<RespostaCommand> AddAsync(PessoaVM viewModel);
		Task<RespostaCommand> AlterarPessoaAsync(AlterarPessoaVM viewModel);
		Task<RespostaCommand> ExcluirPessoaAsync(Guid id);
	}
}
