using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Operacoes;
using Domain.EasyDelivery.Autenticacao.Models.LoginSocial.ValueObjects;
using Domain.EasyDelivery.Autenticacao.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infra.CrossCutting.Auth.Services
{
    public class AppleLoginService : IAppleLoginService
    {
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration configuration;


        public AppleLoginService(IHttpClientFactory httpClientFactory
        , IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<RespostaCommand> AutenticarComAppleAsync(string tokenTemporario, string grant_type)
        {
            try
            {


                var resposta = new RespostaCommand();

                const string checkUrl = "https://appleid.apple.com/auth/token";


                var clientId = configuration["AuthSettings:AppleClienteId"];
                var clientSecret = configuration["AuthSettings:AppleSecret"];
                var redirectUrl = configuration["AuthSettings:AppleRedirectUrl"];


                using (var http = _httpClientFactory.CreateClient())
                {
                    // var paramsDic = new Dictionary<string, string>();
                    var stringContent = $@"client_id={clientId}&client_secret={clientSecret}&code={tokenTemporario}&grant_type=authorization_code&redirect_uri={redirectUrl}";
                    // paramsDic.Add("client_id", clientId);
                    // paramsDic.Add("client_secret", clientSecret);
                    // paramsDic.Add("code", tokenTemporario);
                    // paramsDic.Add("grant_type", "authorization_code");
                    // paramsDic.Add("redirect_uri", redirectUrl);

                    var request = new HttpRequestMessage(HttpMethod.Post, checkUrl);
                    request.Content = new StringContent(stringContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                    // request.Content.Headers.ContentType.CharSet = "UTF-8";

                    var response = await http.SendAsync(request);
                    var tokenOk = response.StatusCode == System.Net.HttpStatusCode.OK ? true : false;
                    resposta.Dados = tokenOk;

                    if (!tokenOk)
                    {
                        resposta.AdicionarErro(nameof(AppleLoginService), "Token inválido");
                        return resposta;
                    }

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<AppleTokenVerification>(stringResult);


                    //"eyJraWQiOiI4NkQ4OEtmIiwiYWxnIjoiUlMyNTYifQ.eyJpc3MiOiJodHRwczovL2FwcGxlaWQuYXBwbGUuY29tIiwiYXVkIjoiY29tLnRlc3RlLmVhc3lkZWxpdmVyeSIsImV4cCI6MTYwMTE1NTY1MywiaWF0IjoxNjAxMDY5MjUzLCJzdWIiOiIwMDE3NTkuN2Y0NjI4ZDIyNjQzNDBkNDhjNjJjMGJlNDgzMmExNWEuMjE1MiIsIm5vbmNlIjoiMGRlOGY1Mzg1Zjc2MzdiYWViZDMzOTZmODg3YjkyN2Y4YmMyMDllNzYzMzk4NmQxZDg1MmVlNWRhMWVhOWVjNSIsImF0X2hhc2giOiJ3QktlMGFsLXZuSkM5d2d5SzBsRTd3IiwiZW1haWwiOiJyYW1vbi5tYWlhLmxvYm9AZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOiJ0cnVlIiwiYXV0aF90aW1lIjoxNjAxMDY5MjIwLCJub25jZV9zdXBwb3J0ZWQiOnRydWV9.DhvSkHi_aLrQKjn9divA6oVyA9wKcNSh4C47pU8vttK1IyZMm7ftPEL49hGHqF3meboprxoKovj7EPevD26GUZwnPxdzQfjeMZE9rQCi0DVdYBIiEspWlTtrh_03DJIs-EzToyxyex-UmjfJrju75XiGpxRo9dUkkB19_Ph4s2aYYEIcY2dLiuwVQd78Uxk14HzKvNabQtN-hHZbh7Ro40rREDXifY4Ua4YpQKD7d3-F_-xcQrzE5lDZvHddaovUh_8HkEgAgEhG2fpNwB-FPRJADN-tZbQmCIu-WgS2_xbsVUc0r7Jzu1iSq0GAQYIkGDUeFsiqdc7f92wj7fHycA";
                    var stream = json.id_token;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                    var email = tokenS.Claims.First(x => x.Type == "email").Value;
                    var sub = tokenS.Claims.First(x => x.Type == "sub").Value;

                    var appleUserData = new AppleUserData()
                    {
                        email = email,
                        userId = sub
                    };

                    resposta.Dados = appleUserData;

                    return resposta;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}