using System;
using System.Collections.Generic;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.Interfaces;

namespace TheEconomy.Server.Resources.Services
{
    public class Colors : IColors
    {
        private readonly Dictionary<string, dynamic> colors = [];

        public Colors()
        {
            colors.TryAdd("primaryColor", new Color(65, 171, 255));
            colors.TryAdd("secondaryColor", new Color(222, 238, 255));
            colors.TryAdd("primaryWhite", new Color(206, 212, 218));
            colors.TryAdd("secondaryWhite", new Color(233, 236, 239));
            colors.TryAdd("secondaryColor", new Color(222, 226, 230));
            colors.TryAdd("primaryBlack", new Color(33, 37, 41));
            colors.TryAdd("secondaryBlack", new Color(52, 58, 64));
            colors.TryAdd("primaryRed", new Color(255, 65, 65));
            colors.TryAdd("secondaryRed", new Color(255, 222, 222));
            colors.TryAdd("primaryYellow", new Color(255, 235, 59));
            colors.TryAdd("secondaryYellow", new Color(254, 255, 222));
            colors.TryAdd("primaryGreen", new Color(76, 175, 80));
            colors.TryAdd("secondaryGreen", new Color(225, 255, 222));
        }

        public Color ObtainRGBA(string nameColor, int alpha)
        {
            if (string.IsNullOrWhiteSpace(nameColor))
                throw new ArgumentException("El nombre del color no puede ser nulo, vacío ni contener espacios en blanco.", nameof(nameColor));

            colors.TryGetValue(nameColor, out dynamic color);
            return new Color(color.R, color.G, color.B, alpha);
        }

        public Color ObtainRGB(string nameColor)
        {
            if (string.IsNullOrWhiteSpace(nameColor))
                throw new ArgumentException("El nombre del color no puede ser nulo, vacío ni contener espacios en blanco.", nameof(nameColor));

            colors.TryGetValue(nameColor, out dynamic color);
            return new Color(color.R, color.G, color.B);
        }

        public string GetHexadecimal(string nameColor, bool includeNumeral = false, bool includeKeys = true)
        {
            if (string.IsNullOrWhiteSpace(nameColor))
                throw new ArgumentException("El nombre del color no puede ser nulo, vacío ni contener espacios en blanco.", nameof(nameColor));

            if (!colors.TryGetValue(nameColor, out dynamic color) || color == null)
                throw new ArgumentException($"El color '{nameColor}' no se encontró en el diccionario.", nameof(nameColor));

            string numeral = includeNumeral ? "#" : "";
            string colorHexadecimal = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            string result = includeKeys ? $"{{{colorHexadecimal}}}" : colorHexadecimal;

            return numeral + result;
        }
    }
}