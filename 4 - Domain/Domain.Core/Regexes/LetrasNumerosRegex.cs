using System.Text.RegularExpressions;

namespace Domain.Core.Regexes
{
     public static class LetrasNumerosRegex
    {
        const string pattern = "^[a-zA-Z0-9]+$";

        public static string Formatar(string valor)
        {
            return Regex.Replace(valor, pattern, "").Replace("_", "").Replace("-", "");
        }

        public static bool Valido(string valor)
        {
            return Regex.IsMatch(valor, pattern);
        }
    }
}