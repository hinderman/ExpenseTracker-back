using Domain.Common;

namespace Domain.Entities
{
    public sealed class Status : BaseEntity
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public static Status Create(string prmName, string prmCode)
        {
            return new Status(prmName, prmCode);
        }

        private Status() { }

        private Status(string prmName, string prmCode)
        {
            Name = prmName;
            Code = prmCode;
        }
    }
}
