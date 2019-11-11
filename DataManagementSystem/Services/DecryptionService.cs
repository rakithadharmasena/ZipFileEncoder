using DataManagementSystem.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataManagementSystem.Services
{
    public interface IDecryptionService
    {
        string Decrypt(byte[] cipherText);
    }

    public class DecryptionService: IDecryptionService
    {
        private static Config _config;
        public DecryptionService(IOptions<Config> configAccessor)
        {
            _config = configAccessor.Value;
        }

        public string Decrypt(byte[] cipherText)
        {
            byte[] key = Encoding.ASCII.GetBytes(_config.Encryption.Key);
            byte[] iv = Encoding.ASCII.GetBytes(_config.Encryption.IV);

            string plaintext = string.Empty;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream ms = new MemoryStream(cipherText))
                { 
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    { 
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
