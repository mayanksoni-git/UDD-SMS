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
    public class Utility
    {
        public static bool CheckDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string Image_ToBase64(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static byte[] generateSecureKey()
        {
            Aes KEYGEN = Aes.Create();
            byte[] secretKey = KEYGEN.Key;
            return secretKey;
        }

        public static void SendSMS(List<SMS_Objects> obj_SMS_Objects)
        {
            for (int i = 0; i < obj_SMS_Objects.Count; i++)
            {
                string MobileNum = obj_SMS_Objects[i].MobileNum;
                string SMS_Content = obj_SMS_Objects[i].SMS_Content;
                string SMS_Response = obj_SMS_Objects[i].SMS_Response;
                string Template_Id = obj_SMS_Objects[i].Template_Id;
                string Sid = obj_SMS_Objects[i].Sid;

                string sms_URL = "";
                bool is_Unicode = false;
                if (Sid == "" || string.IsNullOrEmpty(Sid))
                {
                    sms_URL = "http://priority.muzztech.in/sms_api/sendsms.php?username=syshimanshu&password=sys&mobile=" + MobileNum + "&sendername=SRVICE&message=" + SMS_Content + "&templateid=" + Template_Id + "";
                }
                else
                {
                    sms_URL = "http://priority.muzztech.in/sms_api/sendsms.php?username=syshimanshu&password=sys&mobile=" + MobileNum + "&sendername=" + Sid + "&message=" + SMS_Content + "&templateid=" + Template_Id + "";
                }
                //sms_URL = "http://priority.muzztech.in/sms_api/sendsms.php?username=technopvt&password=techno&mobile=" + MobileNum + "&sendername=" + Sid + "&message=" + SMS_Content + "&templateid=" + Template_Id + "";
                if (is_Unicode)
                {
                    sms_URL = sms_URL.Replace("sendsms.php", "smsUnicode.php");
                    sms_URL += "&MType=U";
                }
                try
                {
                    WebRequest request;
                    string MobileNo = MobileNum.Trim();
                    string sms = SMS_Content.Trim();
                    request = WebRequest.Create(sms_URL);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response1.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    String responseFromServer = reader.ReadToEnd();
                    SMS_Response = responseFromServer.ToString().Trim();

                    tbl_SMS obj_SMS = new tbl_SMS();
                    obj_SMS.SMS_Content = sms;
                    obj_SMS.SMS_Mobile_No = MobileNo;
                    obj_SMS.SMS_Response = SMS_Response;
                    //obj_SMS.SMS_IsPromo = is_Promo;
                    //obj_SMS.Status = "Success";
                    new DataLayer().Insert_tbl_SMS(obj_SMS);

                    reader.Close();
                    dataStream.Close();
                    response1.Close();
                }
                catch (Exception ex)
                {
                    SMS_Response = "Error";
                    tbl_SMS obj_SMS = new tbl_SMS();
                    obj_SMS.SMS_Content = SMS_Content;
                    obj_SMS.SMS_Mobile_No = MobileNum;
                    obj_SMS.SMS_Response = ex.Message;
                    //obj_SMS.SMS_IsPromo = is_Promo;
                    //obj_SMS.Status = "Failed";
                    new DataLayer().Insert_tbl_SMS(obj_SMS);
                }
            }
        }

        #region RSA
        public static string EncryptTextAsymetric(string publicKey, string text)
        {
            // Convert the text to an array of bytes 
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(text);
            byte[] encryptedData = null;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
            }
            string returnValue = Convert.ToBase64String(encryptedData);
            return returnValue;
        }

        // Method to decrypt the data withing a specific file using a RSA algorithm private key 
        public static string DecryptDataAsymetric(string privateKey, string encoded_Data)
        {
            // read the encrypted bytes from the file 
            byte[] dataToDecrypt = System.Text.ASCIIEncoding.ASCII.GetBytes(encoded_Data);

            // Create an array to store the decrypted data in it 
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                decryptedData = RSADecrypt(dataToDecrypt, RSA.ExportParameters(true), false);
            }

            // Get the string value from the decryptedData byte array 
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            return byteConverter.GetString(decryptedData);
        }
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
        #endregion

        public static string get_PNB_Public_Key()
        {
            string cer_path = AppDomain.CurrentDomain.BaseDirectory + "\\ssl\\pnb_base64.cer";
            //X509Certificate2 cert = new X509Certificate2(cer_path);
            //RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(cert.GetRSAPublicKey());

            // Load the certificate into an X509Certificate object.
            X509Certificate cert = X509Certificate.CreateFromCertFile(cer_path);

            // Get the value.
            byte[] results = cert.GetPublicKey();

            string returnValue = Convert.ToBase64String(results);
            return returnValue;
        }
        public static string EncryptAesManaged(string raw)
        {
            try
            {
                string returnValue = "";
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string    
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    // Print encrypted string    
                    returnValue = Convert.ToBase64String(encrypted);
                    return returnValue;  
                }
            }
            catch (Exception exp)
            {
                return "";
            }
        }
        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
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
            return encrypted;
        }
        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        #region Last
        public static string Do_Encryption(RSACryptoServiceProvider cryptoServiceProvider, string textToEncrypt)
        {
            var privateKey = cryptoServiceProvider.ExportParameters(true); //Generowanie klucza prywatnego
            var publicKey = cryptoServiceProvider.ExportParameters(false); //Generowanie klucza publiczny

            string publicKeyString = GetKeyString(publicKey);
            string privateKeyString = GetKeyString(privateKey);

            string encryptedText = Encrypt_RSA_2048(textToEncrypt, publicKeyString); //Szyfrowanie za pomocą klucza publicznego
            return encryptedText;
        }

        public static string Do_Decryption(RSACryptoServiceProvider cryptoServiceProvider, string encryptedText)
        {
            var privateKey = cryptoServiceProvider.ExportParameters(true); //Generowanie klucza prywatnego
            var publicKey = cryptoServiceProvider.ExportParameters(false); //Generowanie klucza publiczny

            string publicKeyString = GetKeyString(publicKey);
            string privateKeyString = GetKeyString(privateKey);
                        
            string decryptedText = Decrypt_RSA_2048(encryptedText, privateKeyString); //Odszyfrowywanie za pomocą klucza prywatnego

            return decryptedText;
        }

        public static string GetKeyString(RSAParameters publicKey)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        public static string Encrypt_RSA_2048(string textToEncrypt, string publicKeyString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string Decrypt_RSA_2048(string textToDecrypt, string privateKeyString)
        {
            var bytesToDescrypt = Encoding.UTF8.GetBytes(textToDecrypt);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {

                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKeyString);

                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private static string GenerateTestString()
        {
            Guid opportinityId = Guid.NewGuid();
            Guid systemUserId = Guid.NewGuid();
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("opportunityid={0}", opportinityId.ToString());
            sb.AppendFormat("&systemuserid={0}", systemUserId.ToString());
            sb.AppendFormat("&currenttime={0}", currentTime);

            return sb.ToString();
        }
        #endregion

        #region Cypher
        public static string Encrypt_AES(string input, string key)
        {
            try
            {
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                AesCryptoServiceProvider tripleDES = new AesCryptoServiceProvider();
                //tripleDES.KeySize = 512;
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch(Exception ee)
            {
                return "";
            }
        }
        public static string Decrypt_AES(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            AesCryptoServiceProvider tripleDES = new AesCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region From PNB
        public static string Encrypt_PNB(string plainText, string iCompleteEncodedKey)
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

        

        static public string SignData_PNB_2(string input, string ThumbPrint)
        {
            string result = "";
            byte[] Data = Encoding.UTF8.GetBytes(input);
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
                using (RSA RSA = RSACertificateExtensions.GetRSAPrivateKey(certificate))
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




        #endregion

        #region AES_256
        public static string EncryptAES(byte[] Key, byte[] IV, string data)
        {
            try
            {
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes_Aes(data, Key, IV);

                //Display the original data and the decrypted data.
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ee)
            {
                return "";
            }
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        public static string DecryptAES(string privateKey, byte[] encryptedBytes)
        {
            try
            {
                using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
                {

                    string roundtrip = DecryptStringFromBytes_Aes(encryptedBytes, myAes.Key, myAes.IV);

                    //Display the original data and the decrypted data.
                    return roundtrip;
                }
            }
            catch (Exception ee)
            {
                return "";
            }
        }
        public static AES_256_Key CreateKeyPair()
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, cspParams);

            string publicKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string privateKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            AES_256_Key obj_AES_256_Key = new AES_256_Key();
            obj_AES_256_Key.Public_Key = publicKey;
            obj_AES_256_Key.Private_Key = privateKey;
            return obj_AES_256_Key;
        }

        public static string CreateKeyAES()
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();

            string returnValue = Convert.ToBase64String(aesProvider.Key);
            return returnValue;
        }
        #endregion

        static public string SignData_AMRUT(string input, string ThumbPrint)
        {
            string result = "";
            byte[] Data = Encoding.UTF8.GetBytes(input);
            X509Certificate2 publicCert = null;
            string thumbprint = Regex.Replace(ThumbPrint, @"[^\da-fA-F]", string.Empty).ToUpper();

            X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection coll = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection coll1 = coll.Find(X509FindType.FindByThumbprint, thumbprint, false);
            foreach (var cert in coll1)
            {
                publicCert = cert;
                break;
            }

            //Fetch private key from the local machine store
            X509Certificate2 privateCert = null;
            store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.GetCertHashString() == publicCert.GetCertHashString())
                {
                    privateCert = cert;
                    break;
                }
            }

            using (RSA RSA = RSACertificateExtensions.GetRSAPublicKey(privateCert))
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
            return result;
        }
    }
}
