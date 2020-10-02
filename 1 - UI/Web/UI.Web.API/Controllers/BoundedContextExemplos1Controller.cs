
using System;
using System.Threading.Tasks;
using Application.EasyDelivery.BoundedContextExemplos1.Services;
using Application.EasyDelivery.BoundedContextExemplos1.ViewModels;
using Domain.EasyDelivery.BoundedContextExemplos1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UI.Web.API.Controllers
{

    [ApiController]
    public class BoundedContextExemplos1Controller : EasyApiController
    {
        private readonly IExemploAppService _exemploAppService;
        private readonly IExemploReadOnlyRepository _exemploReadOnlyRepository;

        public BoundedContextExemplos1Controller(IExemploReadOnlyRepository exemploReadOnlyRepository,
        IExemploAppService exemploAppService,
        ILogger<EasyApiController> logger) : base(logger)
        {
            _exemploAppService = exemploAppService;
            _exemploReadOnlyRepository = exemploReadOnlyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = await _exemploReadOnlyRepository.ObterExemplosAsync();

                return ApiResponse(null, result: query);
            }
            catch (System.Exception e)
            {
                //TODO tratar exceções
                throw e;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddExemploVM vm)
        {
            try
            {
                var result = await _exemploAppService.AddAsync(vm);

                return ApiResponse(result);
            }
            catch (System.Exception e)
            {
                //TODO tratar exceções
                throw e;
            }
        }

        //    [HttpPut]
        // public async Task<IActionResult> Put([FromBody] UpdateExemploVM vm)

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Put(Guid id)

        //    [HttpGet("exemplos/{id}")]
        // public async Task<IActionResult> Get(Guid id)

        //    [HttpGet("exemplos/filhoDeUmExemploNoContexto/{id}")]
        // public async Task<IActionResult> Get(Guid id)
    }
}