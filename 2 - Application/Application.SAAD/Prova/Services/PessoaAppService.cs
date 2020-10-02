using System;
using System.Threading.Tasks;
using Application.SAAD.Core;
using Application.SAAD.Prova.Interfaces;
using Application.SAAD.Prova.ViewModels;
using AutoMapper;
using Domain.Core.Operacoes;
using Domain.Core.UnitOfWork;
using Domain.SAAD.Prova.Commands;
using MediatR;

namespace Application.SAAD.Prova.Services
{
	public class PessoaAppService : BaseAppService, IPessoaAppService
	{
		public PessoaAppService(IMediator mediator, IMapper mapper, IUoW uow) : base(mediator, mapper, uow)
		{
		}

		public async Task<RespostaCommand> AddAsync(PessoaVM viewModel)
		{
			try
			{
				await UnitOfWork.BeginTransactionAsync();

				var command = viewModel.MapperToPessoaCommand();
				var resultado = await base.mediator.Send(command);

				if (!resultado.IsValid) return resultado;

				await UnitOfWork.CommitAsync();

				return resultado;
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}
		public async Task<RespostaCommand> AlterarPessoaAsync(AlterarPessoaVM viewModel)
		{
			try
			{
				await UnitOfWork.BeginTransactionAsync();

				var command = viewModel.MapperToEditarPessoaCommand();
				var resultado = await base.mediator.Send(command);

				if (!resultado.IsValid) return resultado;

				await UnitOfWork.CommitAsync();

				return resultado;
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}
		public async Task<RespostaCommand> ExcluirPessoaAsync(Guid id)
		{
			try
			{
				await UnitOfWork.BeginTransactionAsync();
				var cmd = new ExcluirPessoaCommand(id);
				var resultado = await mediator.Send(cmd);

				if (!resultado.IsValid)
				{
					await UnitOfWork.RollBackAsync();
					return resultado;
				}

				await UnitOfWork.CommitAsync();

				return resultado;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
