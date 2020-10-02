using System.Linq;
using Domain.Core.Regexes;

namespace Domain.Core.ValueObjects
{
    public class Cnh : BaseValueObject
    {
        public const int TAM_CNH = 11;
        public Cnh() { }

        public Cnh(string cnh)
        {
            _valorSemMascara = cnh;
            Valor = ApenasNumerosRegex.Formatar(cnh);
        }

        string _valorSemMascara;

        public override bool EqualsTo(object value)
        {
            return this.Valor == value.ToString();
        }
        
        public override bool Valido()
        {
            return ValidarCNH(Valor);
        }
        private static bool ValidarCNH(string cnh)
        {
            if (cnh.Length != TAM_CNH) return false;

            //todos caracteres iguais
            if (cnh.Distinct().Count() == 1) return false;
            return true;
        }
    }
}