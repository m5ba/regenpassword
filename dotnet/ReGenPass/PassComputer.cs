using System.Security.Cryptography;
using System.Text;

namespace ReGenPass
{
    public class PassComputer
    {
        private const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
        private const string symbolSet = "!@#$%^~*()";

        private const int MinLength = 16;
        private const int MaxLength = 20;
        private const int NumberOfSymbols = 3;

        public static string Compute(PasswordContext passwordContext)
        {
            StringBuilder sb = new();
            sb.Append(passwordContext.Resource);
            sb.Append(passwordContext.Login);
            sb.Append(passwordContext.Iteration);
            sb.Append(passwordContext.Secret);

            HashSet<int> symbolsIndexes = new();
            byte[] hash;
            hash = SHA512.HashData(Encoding.UTF8.GetBytes(sb.ToString()));
            int length = MinLength + hash[0] % (MaxLength - MinLength);
            for (int i = 0; i < NumberOfSymbols; i++)
            {
                int idx = hash[1 + i] % length;
                symbolsIndexes.Add(idx);
            }
            StringBuilder res = new();
            for (int i = 0; i < length; i++)
            {
                res.Append(SelectElement(symbolsIndexes.Contains(i) ? symbolSet : base64Chars, hash[i + 4]));
            }
            return res.ToString();
        }

        private static char SelectElement(string baseEncoding, byte val)
        {
            return baseEncoding.ElementAt(val % baseEncoding.Length);
        }
    }
}
