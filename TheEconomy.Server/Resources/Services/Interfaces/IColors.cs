using SampSharp.Entities.SAMP;

namespace TheEconomy.Server.Resources.Services.Interfaces
{
    public interface IColors
    {
        public Color ObtainRGBA(string nameColor, int alpha);
        public Color ObtainRGB(string nameColor);
        public string GetHexadecimal(string nameColor, bool includeNumeral = false, bool includeKeys = true);
    }
}