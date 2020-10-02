using Domain.Core.Regexes;

namespace Domain.Core.ValueObjects
{
    public class Cnpj : BaseValueObject
    {
        const int TAM_CNPJ = 14;

        //EF
        public Cnpj() { }
        public Cnpj(string cnpj)
        {
            _valorMascara = cnpj;
            Valor = ApenasNumerosRegex.Formatar(cnpj);
        }

        string _valorMascara;

        public override bool EqualsTo(object value)
        {
            return this.Valor == value.ToString();
        }
        public override bool Valido()
        {
            return CnpjValido(Valor);
        }

        private static bool CnpjValido(string cnpj)
        {
            if (cnpj.Length != TAM_CNPJ) return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}