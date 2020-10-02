using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Web.API.Models;

namespace UI.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class EasyApiController : ControllerBase
    {
        protected readonly ILogger<EasyApiController> _logger;

        protected EasyApiController(ILogger<EasyApiController> logger)
        {
            _logger = logger;
        }

        protected IActionResult ApiResponse(ValidationResult validation, object result = null)
        {
            if (validation != null && validation.IsValid)
            {
                var validacoes = new List<ValidacaoChaveValor>();

                foreach (var item in validation.Errors)
                {
                    validacoes.Add(new ValidacaoChaveValor(item.PropertyName, item.ErrorMessage));
                }


                return BadRequest(new EnvelopeRespostaApi()
                {
                    Validacoes = validacoes,
                    Dados = result
                });
            }

            var envelope = new EnvelopeRespostaApi()
            {
                Dados = result,
            };

            return Ok(envelope);
        }
    }
}