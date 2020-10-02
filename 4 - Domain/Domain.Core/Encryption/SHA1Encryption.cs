using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Core.Encryption
{
    public static class SHA1Encryption
    {
        public static string Encrypt(string value)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                var data = Encoding.ASCII.GetBytes(value);
                var sha1data = sha1.ComputeHash(data);

                return string.Join(
                string.Empty,
                sha1data.Select(x => x.ToString("x2")));
            }

        }
    }
}