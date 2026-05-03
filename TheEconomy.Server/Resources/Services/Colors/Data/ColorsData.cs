using System.Collections.Generic;
using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.Colors.Data;

public static class ColorData
{
    public static Dictionary<string, Color> GetDefaultColors()
    {
        return new Dictionary<string, Color>
        {
            // Rojo (Principales)
            { "primaryColor", new Color(255, 65, 65) },
            { "secondaryColor", new Color(255, 222, 222) },

            // Blancos y Grises
            { "primaryWhite", new Color(206, 212, 218) },
            { "secondaryWhite", new Color(233, 236, 239) },

            // Negros
            { "primaryBlack", new Color(33, 37, 41) },
            { "secondaryBlack", new Color(52, 58, 64) },

            // Rojo (Errores / Peligro)
            { "primaryRed", new Color(255, 65, 65) },
            { "secondaryRed", new Color(255, 222, 222) },

            // Amarillo (Advertencias)
            { "primaryYellow", new Color(255, 235, 59) },
            { "secondaryYellow", new Color(254, 255, 222) },

            // Verde (Éxito / Dinero)
            { "primaryGreen", new Color(76, 175, 80) },
            { "secondaryGreen", new Color(225, 255, 222) }
        };
    }
}