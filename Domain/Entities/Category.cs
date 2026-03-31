using Domain.Common;

namespace Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        private Category() { }

        private Category(string prmName, string prmCode)
        {
            Name = prmName;
            Code = prmCode;
        }
    }
}
