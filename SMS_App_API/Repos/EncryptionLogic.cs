using ePayment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ePayment_API.Repos
{
    public static class EncryptionLogic
    {
        static public string Encryption_PNB(string input, string ThumbPrint)
        {
            string result = "";
            byte[] Data = Encoding.UTF8.GetBytes(input);
            //byte[] Data = Convert.FromBase64String(input);
            try
            {
                X509Certificate2 certificate = null;
                string thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();

                X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);
                foreach (var cert in coll1)
                {
                    certificate = cert;
                    break;
                }
                using (RSA RSA = RSACertificateExtensions.GetRSAPublicKey(certificate))
                {
                    RSAParameters parms = RSA.ExportParameters(false);
                    Org.BouncyCastle.Crypto.AsymmetricKeyParameter param1 = new Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters(false, new Org.BouncyCastle.Math.BigInteger(1, parms.Modulus),
                                                                                                                                                                                                                                                            new Org.BouncyCastle.Math.BigInteger(1, parms.Exponent));
                    Org.BouncyCastle.Crypto.Encodings.OaepEncoding encoding = new Org.BouncyCastle.Crypto.Encodings.OaepEncoding(new Org.BouncyCastle.Crypto.Engines.RsaBlindedEngine(),
                        new Org.BouncyCastle.Crypto.Digests.Sha256Digest(), new Org.BouncyCastle.Crypto.Digests.Sha1Digest(), null);
                    Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher buffer = new Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher(encoding);
                    buffer.Init(true, param1);
                    buffer.ProcessBytes(Data, 0, Data.Length);
                    var final = buffer.DoFinal();
                    result = Convert.ToBase64String(final);
                }
            }
            catch (CryptographicException e)
            {
                //_logger.Info(e.ToString());
            }
            return result;
        }

        static public string Encryption_AMRUT(string input, string ThumbPrint)
        {
            string result = "";
            byte[] Data = Encoding.UTF8.GetBytes(input);
            //byte[] Data = Convert.FromBase64String(input);
            try
            {
                X509Certificate2 certificate = null;
                string thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();

                X509Store store = new X509Store(StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);
                foreach (var cert in coll1)
                {
                    certificate = cert;
                    break;
                }
                using (RSA RSA = RSACertificateExtensions.GetRSAPublicKey(certificate))
                {
                    RSAParameters parms = RSA.ExportParameters(false);
                    Org.BouncyCastle.Crypto.AsymmetricKeyParameter param1 = new Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters(false, new Org.BouncyCastle.Math.BigInteger(1, parms.Modulus),
                                                                                                                                                                                                                                                            new Org.BouncyCastle.Math.BigInteger(1, parms.Exponent));
                    Org.BouncyCastle.Crypto.Encodings.OaepEncoding encoding = new Org.BouncyCastle.Crypto.Encodings.OaepEncoding(new Org.BouncyCastle.Crypto.Engines.RsaBlindedEngine(),
                        new Org.BouncyCastle.Crypto.Digests.Sha256Digest(), new Org.BouncyCastle.Crypto.Digests.Sha1Digest(), null);
                    Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher buffer = new Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher(encoding);
                    buffer.Init(true, param1);
                    buffer.ProcessBytes(Data, 0, Data.Length);
                    var final = buffer.DoFinal();
                    result = Convert.ToBase64String(final);
                }
            }
            catch (CryptographicException e)
            {
                //_logger.Info(e.ToString());
            }
            return result;
        }

        public static string Decryption_PNB(string input, string ThumbPrint)
        {
            string result = "";
            //byte[] Data = Encoding.UTF8.GetBytes(input);
            byte[] Data = Convert.FromBase64String(input);
            try
            {
                X509Certificate2 certificate = null;
                //For PNB
                X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);

                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
                var thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();
                X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);

                foreach (var cert in coll1)
                {
                    certificate = cert;
                    break;
                }
                if (certificate != null)
                {
                    using (RSA RSA = RSACertificateExtensions.GetRSAPrivateKey(certificate))
                    {
                        Org.BouncyCastle.Crypto.AsymmetricKeyParameter param1 = rsakeyparms(RSA);
                        Org.BouncyCastle.Crypto.Encodings.OaepEncoding encoding = new Org.BouncyCastle.Crypto.Encodings.OaepEncoding(new Org.BouncyCastle.Crypto.Engines.RsaBlindedEngine(),
                            new Org.BouncyCastle.Crypto.Digests.Sha256Digest(), new Org.BouncyCastle.Crypto.Digests.Sha1Digest(), null);
                        Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher buffer = new Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher(encoding);
                        buffer.Init(false, param1);
                        buffer.ProcessBytes(Data, 0, Data.Length);
                        var final = buffer.DoFinal();
                        result = Encoding.UTF8.GetString(final);
                    }
                    store.Close();
                }
                else
                {
                    
                }
            }
            catch (CryptographicException e)
            {
                
            }
            return result;
        }

        public static string Decryption_AMRUT(string input, string ThumbPrint)
        {
            string result = "";
            byte[] Data = Convert.FromBase64String(input);
            //byte[] Data = Encoding.UTF8.GetBytes(input);
            try
            {
                X509Certificate2 certificate = null;
                X509Store store = new X509Store(StoreLocation.LocalMachine);

                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
                var thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();
                X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);

                foreach (var cert in coll1)
                {
                    certificate = cert;
                    break;
                }
                if (certificate != null)
                {
                    using (RSA RSA = RSACertificateExtensions.GetRSAPrivateKey(certificate))
                    {
                        Org.BouncyCastle.Crypto.AsymmetricKeyParameter param1 = rsakeyparms(RSA);
                        Org.BouncyCastle.Crypto.Encodings.OaepEncoding encoding = new Org.BouncyCastle.Crypto.Encodings.OaepEncoding(new Org.BouncyCastle.Crypto.Engines.RsaBlindedEngine(),
                            new Org.BouncyCastle.Crypto.Digests.Sha256Digest(), new Org.BouncyCastle.Crypto.Digests.Sha1Digest(), null);
                        Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher buffer = new Org.BouncyCastle.Crypto.BufferedAsymmetricBlockCipher(encoding);
                        buffer.Init(false, param1);
                        buffer.ProcessBytes(Data, 0, Data.Length);
                        var final = buffer.DoFinal();
                        result = Encoding.UTF8.GetString(final);
                    }
                    store.Close();
                }
                else
                {

                }
            }
            catch (Exception e)
            {

            }
            return result;
        }

        internal static Org.BouncyCastle.Crypto.AsymmetricKeyParameter rsakeyparms(RSA rsaprivatekey)
        {
            RSAParameters rsaparms = rsaprivatekey.ExportParameters(true);
            return new Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters(
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.Modulus),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.Exponent),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.D),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.P),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.Q),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.DP),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.DQ),
                new Org.BouncyCastle.Math.BigInteger(1, rsaparms.InverseQ));
        }
        static public string SignData_AMRUT(string data, string ThumbPrint)
        {
            string result = "";
            try
            {
                X509Certificate2 certificate = null;
                //X509Store store = new X509Store(StoreLocation.LocalMachine);
                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
                var thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();
                X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);
                foreach (var cert in coll1)
                {
                    certificate = cert;
                    break;
                }
                using (RSA RSA = RSACertificateExtensions.GetRSAPrivateKey(certificate))
                {
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\SignData_AMRUT_PK.txt", RSA.ToString());
                    byte[] hasbbyte = new UnicodeEncoding().GetBytes(data);
                    var generatedhash = new SHA1Managed().ComputeHash(hasbbyte);
                    var resultbyte = RSA.SignHash(generatedhash, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                    result = Convert.ToBase64String(resultbyte);
                }
            }
            catch (Exception e)
            {
                //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\SignData_AMRUT_Error.txt", "Error:" + e.Message);
            }
            return result;
        }

        static public bool VerifySign_AMRUT(string sign_String, string DataString, string ThumbPrint)
        {
            bool success = false;
            X509Certificate2 certificate = null;
            X509Store store = new X509Store(StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
            var thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();
            X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);
            foreach (var cert in coll1)
            {
                certificate = cert;
                break;
            }
            using (RSA RSA = RSACertificateExtensions.GetRSAPublicKey(certificate))
            {
                byte[] hasbbyte = new UnicodeEncoding().GetBytes(DataString);
                var generatedhash = new SHA1Managed().ComputeHash(hasbbyte);
                success = RSA.VerifyHash(generatedhash, Convert.FromBase64String(sign_String), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            }
            return success;
        }

        #region AES256
        public static string GenerateKey()
        {
            int iKeySize = 256;
            AesManaged aesEncryption = new AesManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.GenerateIV();
            string ivStr = Convert.ToBase64String(aesEncryption.IV);
            aesEncryption.GenerateKey();
            string keyStr = Convert.ToBase64String(aesEncryption.Key);
            string completeKey = ivStr + "|" + keyStr;

            return Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(completeKey));
        }

        public static string Encrypt(string plainText, string iCompleteEncodedKey)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                byte[] Key = null;
                byte[] IV = null;
                IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split('|')[0]);
                Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split('|')[1]);


                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encryptedText, string iCompleteEncodedKey)
        {
            encryptedText = encryptedText.Replace(' ', '+');
            string text = encryptedText.Replace(@"\/", "/");
            byte[] cipherText = Convert.FromBase64String(text);

            byte[] iv = Convert.FromBase64String(iCompleteEncodedKey.Split('|')[0]);
            byte[] key = Convert.FromBase64String(iCompleteEncodedKey.Split('|')[1]);

            string plaintext = null;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                using (CryptoStream cs = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    var result = sr.ReadToEnd();
                    var bytearray = Encoding.UTF8.GetBytes(result);
                    plaintext = Encoding.UTF8.GetString(bytearray);
                    //plaintext = decodedplaintext.Substring(decodedplaintext.IndexOf('{'), (decodedplaintext.Length- decodedplaintext.IndexOf('{')));
                }
            }
            return plaintext;
        }

        #endregion
    }
}
