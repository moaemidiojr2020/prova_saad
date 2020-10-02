using System.Text.RegularExpressions;

namespace Domain.Core.Regexes
{
      public static class ApenasLetrasRegex
    {
        const string pattern = "^[a-zA-Z]+$";

        public static string Formatar(string valor)
        {
            return Regex.Replace(valor, pattern, "");
        }

        public static bool Valido(string valor)
        {
            return Regex.IsMatch(valor, pattern);
        }
    }
}