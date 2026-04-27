using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.Colors.Interfaces;

public interface IColors
{
    Color ObtainRGBA(string nameColor, float alpha);
    Color ObtainRGB(string nameColor);
    string GetHexadecimal(string nameColor, bool includeNumeral = false, bool includeKeys = true);
}