using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FileEncrypt
{
    public class FileEncryptRSA
    {
        private string inputFileName;
        private string outputFileName;
        private string rsaParamsFileName;

        public FileEncryptRSA(string inputFileName, string outputFileName, string rsaParamsFileName)
        {
            this.inputFileName = inputFileName;
            this.outputFileName = outputFileName;
            this.rsaParamsFileName = rsaParamsFileName;
        }

        public void Encrypt()
        {
            byte[] dataToEncrypt = File.ReadAllBytes(inputFileName);
            byte[] encryptedData;

            using (var RSA = new RSACryptoServiceProvider())
            {
                string rsaParams = RSA.ToXmlString(true);
                File.WriteAllText(rsaParamsFileName, rsaParams);
                encryptedData = RSAEncrypt(dataToEncrypt, rsaParams);
            }

            File.WriteAllBytes(outputFileName, encryptedData);
        }

        public void Decrypt()
        {
            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] encryptedData = File.ReadAllBytes(inputFileName);
            string rsaParams = File.ReadAllText(rsaParamsFileName);
            byte[] decryptedData;
            using (var RSA = new RSACryptoServiceProvider())
            {
                decryptedData = RSADecrypt(encryptedData, rsaParams);
            }

            File.WriteAllText(outputFileName, byteConverter.GetString(decryptedData));
        }

        private byte[] RSAEncrypt(byte[] DataToEncrypt, string rsaParams)
        {
            try
            {
                byte[] encryptedData;
                using (var RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(rsaParams);
                    encryptedData = RSA.Encrypt(DataToEncrypt, false);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private byte[] RSADecrypt(byte[] DataToDecrypt, string rsaParams)
        {
            try
            {
                byte[] decryptedData;
                using (var RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(rsaParams);
                    decryptedData = RSA.Decrypt(DataToDecrypt, false);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
