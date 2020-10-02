using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Core.Operacoes;
using Domain.EasyDelivery.Autenticacao.Models.LoginSocial.ValueObjects;
using Domain.EasyDelivery.Autenticacao.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infra.CrossCutting.Auth.Services
{
    public class FacebookLoginService : IFacebookLoginService
    {
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration configuration;
        public FacebookLoginService(IHttpClientFactory httpClientFactory,
         IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<RespostaCommand> AutenticarComFacebookAsync(string tokenTemporario)
        {
            var resposta = new RespostaCommand();

            try
            {
                var verificacaoTask = ValidateFacebookTokenAsync(tokenTemporario);
                var usuarioTask = ObterDadosUsuarioFacebookAsync(tokenTemporario);

                await Task.WhenAll(verificacaoTask, usuarioTask);

                if (!verificacaoTask.Result.data.is_valid)
                {
                    resposta.ValidationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        nameof(AutenticarComFacebookAsync), "Token Facebook inválido"
                    ));
                }

                resposta.Dados = usuarioTask.Result;

                return resposta;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        protected async Task<FacebookTokenVerificationRoot> ValidateFacebookTokenAsync(string token)
        {

            //garantir que token emitido é valido
            var appId = configuration["AuthSettings:FacebookAppId"];
            var appSecret = configuration["AuthSettings:FacebookAppSecret"];

            var checkUrl = $"https://graph.facebook.com/debug_token?input_token={token}&access_token={appId}|{appSecret}";


            using (var http = _httpClientFactory.CreateClient())
            {
                try
                {
                    var response = await http.GetStringAsync(checkUrl);
                    var json = JsonConvert.DeserializeObject<FacebookTokenVerificationRoot>(response);

                    return json;
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }
        }

        protected async Task<FacebookUserRoot> ObterDadosUsuarioFacebookAsync(string token)
        {
            var userInfoUrl = $@"https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={token}";

            using (var http = _httpClientFactory.CreateClient())
            {
                var response = await http.GetStringAsync(userInfoUrl);
                var json = JsonConvert.DeserializeObject<FacebookUserRoot>(response);

                return json;
            }
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}