using System;
using System.Threading.Tasks;
using Application.SAAD.Prova.Interfaces;
using Application.SAAD.Prova.ViewModels;
using Domain.SAAD.Prova.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Web.API.Controllers;

namespace UI.API.Web.Controllers.Prova
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProvaController : SaadApiController
	{
		public ProvaController(ILogger<SaadApiController> logger, IPessoaAppService pessoaAppService, IPessoaReadOnlyRepository pessoaReadOnlyRepository) : base(logger)
		{
			_pessoaAppService = pessoaAppService;
			_pessoaReadOnlyRepository = pessoaReadOnlyRepository;
		}

		private readonly IPessoaAppService _pessoaAppService;
		private readonly IPessoaReadOnlyRepository _pessoaReadOnlyRepository;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] PessoaVM vm)
		{
			try
			{
				var result = await _pessoaAppService.AddAsync(vm);
				return ApiResponse(result.ValidationResult, result.Dados);
			}
			catch (System.Exception e)
			{
				var msg = new
				{
					errorMsg = e.Message,
					internalMsg = e.InnerException?.Message
				};
				return BadRequest(msg);
				//TODO tratar exceções
				//throw e;
			}
		}

		[HttpPut("AlterarPessoa")]
		public async Task<IActionResult> AlterarPessoa([FromBody] AlterarPessoaVM model)
		{
			try
			{
				var resultado = await _pessoaAppService.AlterarPessoaAsync(model);
				return ApiResponse(resultado.ValidationResult, resultado.Dados);
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}

		[HttpDelete("ExcluirPessoa/{id}")]
		public async Task<IActionResult> ExcluirPessoa(Guid id)
		{
			try
			{
				var resultado = await _pessoaAppService.ExcluirPessoaAsync(id);
				return ApiResponse(resultado.ValidationResult, resultado.Dados);
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}


		[HttpGet("ObterEstadoCivilPessoa")]
		public async Task<IActionResult> ObterEstadoCivilPessoa()
		{
			try
			{
				var query = await _pessoaReadOnlyRepository.ObterEstadoCivilAsync();
				return ApiResponse(null, result: query);
			}
			catch (System.Exception e)
			{
				var msg = new
				{
					errorMsg = e.Message,
					internalMsg = e.InnerException?.Message
				};
				return BadRequest(msg);
				//TODO tratar exceções
				// throw e;
			}
		}

		[HttpGet("ObterPessoaPorId")]
		public async Task<IActionResult> ObterPessoaPorId(Guid id)
		{
			try
			{
				var query = await _pessoaReadOnlyRepository.ObterPessoaPorIdAsync(id);
				return ApiResponse(null, result: query);
			}
			catch (System.Exception e)
			{
				var msg = new
				{
					errorMsg = e.Message,
					internalMsg = e.InnerException?.Message
				};
				return BadRequest(msg);
				//TODO tratar exceções
				// throw e;
			}
		}

		[HttpGet("BuscarPessoas")]
		public async Task<IActionResult> BuscarPessoas(string nome)
		{
			try
			{
				var query = await _pessoaReadOnlyRepository.BuscarPessoasAsync(nome);
				return ApiResponse(null, result: query);
			}
			catch (System.Exception e)
			{
				var msg = new
				{
					errorMsg = e.Message,
					internalMsg = e.InnerException?.Message
				};
				return BadRequest(msg);
				//TODO tratar exceções
				// throw e;
			}
		}

	}
}
