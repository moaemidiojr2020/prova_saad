using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core.Operacoes;
using Domain.EasyDelivery.Autenticacao.Commands;
using Domain.EasyDelivery.Autenticacao.Models.LoginSocial.ValueObjects;
using Domain.EasyDelivery.Autenticacao.Services;
using Google.Apis.Auth;
using static Domain.EasyDelivery.Autenticacao.Models.Roles;

namespace Infra.CrossCutting.Auth.Services
{
    public class GoogleLoginService : IGoogleLoginService
    {
        public async Task<RespostaCommand> AutenticarComGoogleAsync(string tokenTemporario)
        {
            var resposta = new RespostaCommand();

            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(
                    tokenTemporario, new GoogleJsonWebSignature.ValidationSettings());

                var googlePayload = new GooglePayload(payload.Subject, payload.Email,
                 payload.GivenName, payload.FamilyName, payload.JwtId, payload);

                resposta.Dados = googlePayload;
                return resposta;
            }
            catch
            {
                resposta.ValidationResult.Errors.Add( new FluentValidation.Results.ValidationFailure(
                    nameof(AutenticarComGoogleAsync), "Token google inválido"));
                return resposta;
            }
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}