using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class CryptoHelper
{
    private static readonly string EncryptionKey = "uddsms@dlb2024"; // Change this key

    public static string Encrypt(string plainText)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
        using (Aes encryptor = Aes.Create())
        {
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x20, 0x4B, 0x6F, 0x74, 0x68, 0x4F, 0x77, 0x65 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string encryptedText)
    {
        encryptedText = encryptedText.Replace(" ", "+"); // Handle spaces in query string
        byte[] cipherBytes = Convert.FromBase64String(encryptedText);
        using (Aes encryptor = Aes.Create())
        {
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x20, 0x4B, 0x6F, 0x74, 0x68, 0x4F, 0x77, 0x65 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                return Encoding.Unicode.GetString(ms.ToArray());
            }
        }
    }
}
