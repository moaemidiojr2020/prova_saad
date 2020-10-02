using System;
using System.Threading.Tasks;
using Domain.EasyDelivery.Autenticacao.Models;
using Domain.EasyDelivery.Autenticacao.Services;

namespace Infra.CrossCutting.Auth.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {

        private readonly IGeradorTokenService _geradorTokenService;

        public AutenticacaoService(IGeradorTokenService geradorTokenService)
        {
            _geradorTokenService = geradorTokenService;
        }

        public string SignIn(UsuarioRoot usuario)
        {
            var token = _geradorTokenService.GerarToken(usuario);

            return token;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}