using System;
using System.Text;
using TheEconomy.Server.Resources.Services.CorrectTextStrings.Interfaces;

namespace TheEconomy.Server.Resources.Services.CorrectTextStrings;

public class CorrectTextStrings : ICorrectTextStrings
{
    public string ObtainCorrection(string message, bool replaceSpaces = true)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("El mensaje no puede estar vacío, nulo ni contener espacios en blanco.", nameof(message));

        StringBuilder correctedMessage = new(message);

        for (int i = 0; i < correctedMessage.Length; i++)
        {
            switch (correctedMessage[i])
            {
                case '%': correctedMessage[i] = (char)37; break;
                case '&': correctedMessage[i] = (char)38; break;
                case '@': correctedMessage[i] = (char)58; break;
                case '¡': correctedMessage[i] = (char)64; break;
                case 'J': correctedMessage[i] = (char)74; break;
                case 'j': correctedMessage[i] = (char)106; break;
                case '°': correctedMessage[i] = (char)124; break;
                case 'À': correctedMessage[i] = (char)128; break;
                case 'Á': correctedMessage[i] = (char)129; break;
                case 'Â': correctedMessage[i] = (char)130; break;
                case 'Ä': correctedMessage[i] = (char)131; break;
                case 'Ã': correctedMessage[i] = (char)131; break;
                case 'Ç': correctedMessage[i] = (char)133; break;
                case 'È': correctedMessage[i] = (char)134; break;
                case 'É': correctedMessage[i] = (char)135; break;
                case 'Ê': correctedMessage[i] = (char)136; break;
                case 'Ë': correctedMessage[i] = (char)137; break;
                case 'Ì': correctedMessage[i] = (char)138; break;
                case 'Í': correctedMessage[i] = (char)139; break;
                case 'Î': correctedMessage[i] = (char)140; break;
                case 'Ï': correctedMessage[i] = (char)141; break;
                case 'Ò': correctedMessage[i] = (char)142; break;
                case 'Ó': correctedMessage[i] = (char)143; break;
                case 'Ô': correctedMessage[i] = (char)144; break;
                case 'Ö': correctedMessage[i] = (char)145; break;
                case 'Õ': correctedMessage[i] = (char)145; break;
                case 'Ù': correctedMessage[i] = (char)146; break;
                case 'Ú': correctedMessage[i] = (char)147; break;
                case 'Û': correctedMessage[i] = (char)148; break;
                case 'Ü': correctedMessage[i] = (char)149; break;
                case 'à': correctedMessage[i] = (char)151; break;
                case 'á': correctedMessage[i] = (char)152; break;
                case 'â': correctedMessage[i] = (char)153; break;
                case 'ä': correctedMessage[i] = (char)154; break;
                case 'ã': correctedMessage[i] = (char)154; break;
                case 'ç': correctedMessage[i] = (char)156; break;
                case 'è': correctedMessage[i] = (char)157; break;
                case 'é': correctedMessage[i] = (char)158; break;
                case 'ê': correctedMessage[i] = (char)159; break;
                case 'ë': correctedMessage[i] = (char)160; break;
                case 'ì': correctedMessage[i] = (char)161; break;
                case 'í': correctedMessage[i] = (char)162; break;
                case 'î': correctedMessage[i] = (char)163; break;
                case 'ï': correctedMessage[i] = (char)164; break;
                case 'ò': correctedMessage[i] = (char)165; break;
                case 'ó': correctedMessage[i] = (char)166; break;
                case 'ô': correctedMessage[i] = (char)167; break;
                case 'ö': correctedMessage[i] = (char)168; break;
                case 'õ': correctedMessage[i] = (char)168; break;
                case 'ù': correctedMessage[i] = (char)169; break;
                case 'ú': correctedMessage[i] = (char)170; break;
                case 'û': correctedMessage[i] = (char)171; break;
                case 'ü': correctedMessage[i] = (char)172; break;
                case 'Ñ': correctedMessage[i] = (char)173; break;
                case 'ñ': correctedMessage[i] = (char)174; break;
                case '¿': correctedMessage[i] = (char)175; break;
                case '`': correctedMessage[i] = (char)177; break;
                case ' ':
                    if (replaceSpaces)
                        correctedMessage[i] = '_';
                    break;
                default:
                    break;
            }
        }

        return Convert.ToString(correctedMessage);
    }
}