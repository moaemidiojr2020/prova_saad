namespace Domain.Core.ValueObjects
{
    public class Rg : BaseValueObject
    {
        public const int RG_MIN_TAM = 3;
        public Rg() { }

        public Rg(string rg)
        {
            Valor = rg;
        }

        public override bool EqualsTo(object value)
        {
            return this.Valor == value.ToString();
        }
        
        public override bool Valido()
        {
            return !string.IsNullOrEmpty(Valor) && Valor.Length > RG_MIN_TAM;
        }
    }
}