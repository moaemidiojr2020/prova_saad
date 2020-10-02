using Domain.Core.Regexes;

namespace Domain.Core.ValueObjects
{
    public class Cep : BaseValueObject
    {
        public const int TAM_CEP = 8;
        public Cep() { }

        public Cep(string cep)
        {
            _valorMascara = cep;
            Valor = ApenasNumerosRegex.Formatar(cep);
        }

        string _valorMascara;

        public override bool EqualsTo(object value)
        {
            return this.Valor == value.ToString();
        }
        public override bool Valido()
        {
            return Validar(Valor);
        }

        private static bool Validar(string cep)
        {
            if (cep.Length != TAM_CEP) return false;

            return true;
        }
    }
}