﻿using ePayment_API.Models;
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
using System.Threading.Tasks;

namespace ePayment_API.Repos
{
    public static class X509Cert
    {
        //static void Main(string[] args)
        //{
        //    string messageToFatima = "My personal data";

        //    //retrieve certificate from store//    
        //    X509Certificate2 certificate = GetCertFromStore();

        //    //**signing data**//    

        //    //to sign we need the hash of data//    
        //    byte[] hashBytes = GetDataHash(messageToFatima);


        //    byte[] signature = GetDigitalSignature(hashBytes);
        //    bool isVerified = VerifyData(signature, messageToFatima);

        //    Console.WriteLine($ "IsVerified: {isVerified}");

        //    Console.ReadLine();
        //}

        public static bool VerifyData(byte[] signature, string messageFromAhemd)
        {
            var messageHash = GetDataHash(messageFromAhemd);

            X509Certificate2 certificate = GetCertFromStore();

            RSACryptoServiceProvider cryptoServiceProvider = (RSACryptoServiceProvider)certificate.PublicKey.Key;

            return cryptoServiceProvider.VerifyHash(messageHash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }

        public static byte[] GetDigitalSignature(byte[] hashBytes)
        {
            X509Certificate2 certificate = GetCertFromStore();
            /*use any asymmetric crypto service provider for encryption of hash   
            with private key of cert.   
            */
            RSACryptoServiceProvider rsaCryptoService = (RSACryptoServiceProvider)certificate.PrivateKey;

            /*now lets sign the hash   
            1.spevify hash bytes   
            2. and hash algorithm name to obtain the bytes   
            */
            return rsaCryptoService.SignHash(hashBytes, CryptoConfig.MapNameToOID("SHA1"));
        }

        public static byte[] GetDataHash(string sampleData)
        {
            //choose any hash algorithm    
            SHA1Managed managedHash = new SHA1Managed();

            return managedHash.ComputeHash(Encoding.Unicode.GetBytes(sampleData));
        }

        public static X509Certificate2 GetCertFromStore()
        {
            //to access to store we need to specify store name and location    
            X509Store x509Store = new X509Store("sampleCertStore", StoreLocation.CurrentUser);

            //obtain read only access to get cert    
            x509Store.Open(OpenFlags.ReadOnly);

            return x509Store.Certificates[0];
        }
    }
}
