using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FileEncrypt
{
    public class FileEncryptRsa : IEncrypter
    {
        private readonly string _inputFileName;
        private readonly string _outputFileName;
        private readonly string _rsaParamsFileName;

        public FileEncryptRsa(string inputFileName, string outputFileName, string rsaParamsFileName)
        {
            _inputFileName = inputFileName;
            _outputFileName = outputFileName;
            _rsaParamsFileName = rsaParamsFileName;
        }

        public void Encrypt()
        {
            byte[] dataToEncrypt = File.ReadAllBytes(_inputFileName);
            byte[] encryptedData;

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                string rsaParams = rsaProvider.ToXmlString(true);
                File.WriteAllText(_rsaParamsFileName, rsaParams);
                encryptedData = RsaEncrypt(dataToEncrypt, rsaParams);
            }

            File.WriteAllBytes(_outputFileName, encryptedData);
        }

        public void Decrypt()
        {
            var byteConverter = new UTF8Encoding();
            byte[] encryptedData = File.ReadAllBytes(_inputFileName);
            string rsaParams = File.ReadAllText(_rsaParamsFileName);
            byte[] decryptedData = RsaDecrypt(encryptedData, rsaParams);

            File.WriteAllText(_outputFileName, byteConverter.GetString(decryptedData));
        }

        private byte[] RsaEncrypt(byte[] dataToEncrypt, string rsaParams)
        {
            byte[] encryptedData;
            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                rsaProvider.FromXmlString(rsaParams);
                encryptedData = rsaProvider.Encrypt(dataToEncrypt, false);
            }
            return encryptedData;
        }

        private byte[] RsaDecrypt(byte[] dataToDecrypt, string rsaParams)
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
