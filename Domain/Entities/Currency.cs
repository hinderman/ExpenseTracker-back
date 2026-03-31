using Domain.Common;

namespace Domain.Entities
{
    public sealed class Currency : BaseEntity
    {
        public string Code { get; private set; }

        public string Symbol { get; private set; }

        public string Name { get; private set; }

        public Currency(string prmCode, string prmSymbol, string prmName)
        {
            Code = prmCode;
            Symbol = prmSymbol;
            Name = prmName;
        }

        private Currency() { }
    }
}
