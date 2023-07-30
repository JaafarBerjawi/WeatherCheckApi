using System.Security.Cryptography;
using System.Text;

namespace Security.Business
{
    public class EncryptionService
    {
        public string Encrypt(string password)
        {
            var hashedBytes = ComputeSha256Hash(password);
            return ConvertBytesToHexString(hashedBytes);
        }

        private static byte[] ComputeSha256Hash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return SHA256.HashData(inputBytes);
        }

        private static string ConvertBytesToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
