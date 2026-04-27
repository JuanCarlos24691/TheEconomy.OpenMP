using System;
using System.Text.RegularExpressions;
using TheEconomy.Server.Resources.Services.VerifyMail.Interfaces;

namespace TheEconomy.Server.Resources.Services.VerifyMail;

public class VerifyMail : IVerifyMail
{
    public bool ObtainVerification(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El correo electrónico no puede estar vacío, nulo ni contener espacios en blanco.", nameof(email));

        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z]{2,}$";

        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
}