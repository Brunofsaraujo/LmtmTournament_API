using System.Text;

namespace LmtmTournament.API.Extensions
{
    public static class Security
    {
        private const string CryptoSeed = "LmtmTournament";
        private const string FormatStringX2 = "x2";
        static readonly System.Security.Cryptography.MD5 Md5Crypto = System.Security.Cryptography.MD5.Create();
        private static readonly StringBuilder PasswordEncrypted = new();

        public static string EncryptPassword(this string password)
        {
            PasswordEncrypted.Clear();
            var data = Md5Crypto.ComputeHash(Encoding.Default.GetBytes(password + CryptoSeed));

            foreach (var t in data)
                PasswordEncrypted.Append(t.ToString(FormatStringX2));

            return PasswordEncrypted.ToString();
        }
    }
}
