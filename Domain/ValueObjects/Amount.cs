using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed partial class Amount
    {
        public decimal Value { get; }

        public static Amount Create(decimal prmAmount)
        {
            if (prmAmount <= 0)
            {
                throw new DomainException("El valor debe ser mayor a cero");
            }

            return new Amount(prmAmount);
        }

        private Amount(decimal prmAmount)
        {
            Value = prmAmount;
        }
    }
}
