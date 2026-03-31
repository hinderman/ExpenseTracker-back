using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed partial class Email
{
    public string Value { get; }

    public static Email Create(string prmEmail)
    {
        if (string.IsNullOrWhiteSpace(prmEmail))
        {
            throw new DomainException("El email es obligatorio.");
        }

        prmEmail = prmEmail.Trim().ToLower();

        if (!IsValid(prmEmail))
        {
            throw new DomainException("El email no tiene un formato válido.");
        }

        return new Email(prmEmail);
    }

    private static bool IsValid(string prmEmail)
    {
        var regex = RegexEmail();

        return regex.IsMatch(prmEmail);
    }

    private Email(string prmEmail)
    {
        Value = prmEmail;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex RegexEmail();
}
