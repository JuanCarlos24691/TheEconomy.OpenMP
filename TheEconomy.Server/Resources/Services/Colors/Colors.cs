using System;
using System.Collections.Generic;
using SampSharp.Entities.SAMP;
using TheEconomy.Server.Resources.Services.Colors.Interfaces;
using TheEconomy.Server.Resources.Services.Colors.Data;

namespace TheEconomy.Server.Resources.Services.Colors;

public class Colors : IColors
{
    private readonly Dictionary<string, Color> colors;

    public Colors()
    {
        colors = ColorData.GetDefaultColors();
    }

    public Color ObtainRGB(string nameColor)
    {
        if (string.IsNullOrWhiteSpace(nameColor))
            throw new ArgumentException("El nombre del color no puede estar vacío.", nameof(nameColor));

        return colors.TryGetValue(nameColor, out var color) ? color : Color.White;
    }

    public Color ObtainRGBA(string nameColor, float alpha)
    {
        var color = ObtainRGB(nameColor);
        return new Color(color.R, color.G, color.B, alpha);
    }

    public string GetHexadecimal(string nameColor, bool includeNumeral = false, bool includeKeys = true)
    {
        Color color = ObtainRGB(nameColor);
        
        string hexadecimal = $"{color.R:X2}{color.G:X2}{color.B:X2}";
        
        if (includeKeys) 
            hexadecimal = "{" + hexadecimal + "}";
        if (includeNumeral) 
            hexadecimal = "#" + hexadecimal;

        return hexadecimal;
    }
}