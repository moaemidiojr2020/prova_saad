using System.Text.RegularExpressions;

namespace Domain.Core.Regexes
{
    public static class ApenasNumerosRegex
    {
        const string pattern = "[^0-9]";

        public static string Formatar(string valor)
        {
            return Regex.Replace(valor, pattern, "");
        }
    }
}