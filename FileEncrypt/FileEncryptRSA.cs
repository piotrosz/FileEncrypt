using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FileEncrypt
{
    public class FileEncryptRsa : IEncrypter
    {
        private readonly string _rsaParamsFileName;

        public FileEncryptRsa(string rsaParamsFileName)
        {
            _rsaParamsFileName = rsaParamsFileName;
        }

        public void Encrypt(string inputFileName)
        {
            byte[] dataToEncrypt = File.ReadAllBytes(inputFileName);
            byte[] encryptedData;

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                string rsaParams = rsaProvider.ToXmlString(true);
                File.WriteAllText(_rsaParamsFileName, rsaParams);
                encryptedData = RsaEncrypt(dataToEncrypt, rsaParams);
            }

            File.WriteAllBytes(OutputFilenameGenerator.Generate(inputFileName, EncryptAction.Encrypt), encryptedData);
        }

        public void Decrypt(string inputFileName)
        {
            var byteConverter = new UTF8Encoding();
            byte[] encryptedData = File.ReadAllBytes(inputFileName);
            string rsaParams = File.ReadAllText(_rsaParamsFileName);
            byte[] decryptedData = RsaDecrypt(encryptedData, rsaParams);

            File.WriteAllText(OutputFilenameGenerator.Generate(inputFileName, EncryptAction.Decrypt), byteConverter.GetString(decryptedData));
        }

        private static byte[] RsaEncrypt(byte[] dataToEncrypt, string rsaParams)
        {
            byte[] encryptedData;
            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                rsaProvider.FromXmlString(rsaParams);
                encryptedData = rsaProvider.Encrypt(dataToEncrypt, false);
            }
            return encryptedData;
        }

        private static byte[] RsaDecrypt(byte[] dataToDecrypt, string rsaParams)
        {
            byte[] decryptedData;
            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                rsaProvider.FromXmlString(rsaParams);
                decryptedData = rsaProvider.Decrypt(dataToDecrypt, false);
            }
            return decryptedData;
        }
    }
}
