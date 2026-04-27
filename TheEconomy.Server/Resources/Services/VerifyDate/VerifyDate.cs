using System;
using TheEconomy.Server.Resources.Services.VerifyDate.Interfaces;

namespace TheEconomy.Server.Resources.Services.VerifyDate;

public class VerifyDate : IVerifyDate
{
    public bool ObtainVerification(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            throw new ArgumentException("La fecha no puede ser nula, vacía ni contener espacios en blanco.", nameof(date));

        return DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
    }
}