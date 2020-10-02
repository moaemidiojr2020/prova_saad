using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Domain.EasyDelivery.Autenticacao.Models;
using Domain.EasyDelivery.Autenticacao.Models.AlterarSenhas.ValueObjects;
using Domain.EasyDelivery.Autenticacao.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infra.CrossCutting.Auth.Services
{
    public class GeradorTokenService : IGeradorTokenService
    {
        private readonly IConfiguration configuration;

        public GeradorTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GerarToken(UsuarioRoot usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSettings = configuration["AuthSettings:JwtSecret"];
            var key = Encoding.ASCII.GetBytes(jwtSettings);

            var expirationDays = Convert.ToInt32(configuration["AuthSettings:JwtExpirationDays"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, configuration["AuthSettings:JwtAudience"]),
                new Claim(JwtRegisteredClaimNames.Iss, configuration["AuthSettings:JwtIssuer"]),
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Id.ToString()),
            };

            foreach (var role in usuario.RolesUsuario)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.RoleId.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(expirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string GerarTokenRecuperacaoSenha(TokenRecuperacaoSenhaRoot model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSettings = configuration["AuthSettings:JwtRecPwdSecret"];
            var key = Encoding.ASCII.GetBytes(jwtSettings);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.EmailUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, model.EmailUsuario),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = model.PrazoValidade,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
              SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

     
        public NovaSenhaTokenVO ObterTokenNovaSenha(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            return new NovaSenhaTokenVO(token.Subject, token.ValidTo);
        }
    }
}