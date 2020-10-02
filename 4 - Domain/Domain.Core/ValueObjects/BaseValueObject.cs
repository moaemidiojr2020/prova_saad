namespace Domain.Core.ValueObjects
{
    public abstract class BaseValueObject
    {
        public virtual string Valor { get; protected set; }
        public abstract bool Valido();
        public abstract bool EqualsTo(object value);
    }
}