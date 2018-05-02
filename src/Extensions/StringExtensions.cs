namespace Bizy.OuinneBiseSharp.Extensions
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExtension
    {
        public static string Encrypt(this string data, string key)
        {
            if (string.IsNullOrWhiteSpace(data)) throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            var rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.ImportCspBlob(Convert.FromBase64String(key));
            var plainBytes = Encoding.UTF8.GetBytes(data);
            var encryptedBytes = rsaProvider.Encrypt(plainBytes, false);
            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
