using System.Security.Cryptography;
using System.Text;

namespace IsProje.Repo
{
    public class Method
    {
        public static string Sha256_hash(string password)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(l => l.ToString("X2")));
            }
        }
    }
}
