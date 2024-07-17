using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MilkShop.config
{
    public class AesEncryption
    {
        public  string Encrypt(string plainText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, aes.Key.Length); 
                aes.Key = keyBytes;

                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                using (var ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText, string key)
        {
            try
            {
                byte[] fullCipher = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    byte[] iv = new byte[aes.BlockSize / 8];
                    byte[] cipher = new byte[fullCipher.Length - iv.Length];

                    Array.Copy(fullCipher, iv, iv.Length);
                    Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    Array.Resize(ref keyBytes, aes.Key.Length);
                    aes.Key = keyBytes;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                    using (var ms = new MemoryStream(cipher))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (FormatException ex)
            {
                // Handle the format exception (log, notify user, etc.)
                Console.WriteLine($"Error decoding Base64 string: {ex.Message}");
                throw; // Optionally rethrow or handle accordingly
            }
        }

    }
}
