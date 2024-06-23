using CoindeskExampleApiServer.Models.Option;
using CoindeskExampleApiServer.Protocols.Services;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace CoindeskExampleApiServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AesEncryption(IOptions<AesEncryptionInfo> aesEncryptionInfoOptions) : IAesEncryption
    {
        private readonly AesEncryptionInfo _aesEncryptionInfo = aesEncryptionInfoOptions.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptString(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_aesEncryptionInfo.Key);
            aesAlg.IV = Encoding.UTF8.GetBytes(_aesEncryptionInfo.IV);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string DecryptString(string cipherText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_aesEncryptionInfo.Key);
            aesAlg.IV = Encoding.UTF8.GetBytes(_aesEncryptionInfo.IV);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(Convert.FromBase64String(cipherText));
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
