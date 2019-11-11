using ControlPanel.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Services
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string plainText);
    }

    public class EncryptionService : IEncryptionService
    {
        private static Config _config;

        public EncryptionService(IOptions<Config> configAccessor)
        {
            _config = configAccessor.Value;
        }

        public byte[] Encrypt(string plainText)
        {
            byte[] key = Encoding.ASCII.GetBytes(_config.Encryption.Key);
            byte[] iv = Encoding.ASCII.GetBytes(_config.Encryption.IV);

            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {  
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {   
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            
            return encrypted;
        }
    }
}
