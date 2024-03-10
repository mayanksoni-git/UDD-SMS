﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class PasswordEncryption
{
    private const string EncryptionKey = "jnupepayment"; // The key used to encrypt and decrypt the password.

    // Encrypts a password using the specified encryption key.
    public static string EncryptPassword(string password)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(password);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }, 1000);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                password = Convert.ToBase64String(ms.ToArray());
            }
        }
        return password;
    }

    // Decrypts a password using the specified encryption key.
    public static string DecryptPassword(string encryptedPassword)
    {
        encryptedPassword = encryptedPassword.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(encryptedPassword);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }, 1000);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                encryptedPassword = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return encryptedPassword;
    }
}
