using System.Collections.Generic;

namespace UI.Web.API.Models
{
    public class EnvelopeRespostaApi
    {
        public object Dados {get; set;}
        public List<ValidacaoChaveValor> Validacoes {get; set;}
    }

    public struct ValidacaoChaveValor
    {
        public string chave;
        public string valor;

        public ValidacaoChaveValor(string chave, string valor)
        {
            this.chave = chave;
            this.valor = valor;
        }
    }
}