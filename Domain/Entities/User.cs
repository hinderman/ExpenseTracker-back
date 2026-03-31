using Domain.Common;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string Name { get; private set; }

        public Email? Email { get; private set; }

        public static User Create(string prmName, Email? prmEmail = null)
        {
            if (string.IsNullOrWhiteSpace(prmName))
            {
                throw new DomainException("El nombre del usuario es obligatorio");
            }

            return new User(prmName, prmEmail);
        }

        private User() { }

        private User(string prmName, Email? prmEmail)
        {
            Name = prmName;
            Email = prmEmail;
        }
    }
}