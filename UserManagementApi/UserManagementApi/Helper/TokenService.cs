using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UserManagementApi.Helper
{
    public class TokenService
    {
        public static string EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"].ToString();

        public static string GenerateToken(string clearText)
        {
            byte[] ClearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes Encrytor = Aes.Create())
            {
                Rfc2898DeriveBytes PseudoRandomNumber = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                Encrytor.Key = PseudoRandomNumber.GetBytes(32);
                Encrytor.IV = PseudoRandomNumber.GetBytes(16);
                using (MemoryStream MemoryStore = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(MemoryStore, Encrytor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(ClearBytes, 0, ClearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(MemoryStore.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            byte[] CipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes PseudoRandomNumber = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = PseudoRandomNumber.GetBytes(32);
                encryptor.IV = PseudoRandomNumber.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream DataStreams = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        DataStreams.Write(CipherBytes, 0, CipherBytes.Length);
                        DataStreams.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}