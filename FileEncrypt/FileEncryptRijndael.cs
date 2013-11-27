using System.Security.Cryptography;
using System.IO;

namespace FileEncrypt
{
    public class FileEncryptRijndael : IEncrypter
    {
        private readonly string _password;
        private readonly byte[] _saltValueBytes;

        public FileEncryptRijndael(string password, byte[] saltValueBytes)
        {
            _password = password;
            _saltValueBytes = saltValueBytes;
        }

        public void Encrypt(string inputFileName)
        {
            RijndaelManaged algorithm;
            using (var passwordKey = new Rfc2898DeriveBytes(_password, _saltValueBytes))
            {
                algorithm = new RijndaelManaged();
                algorithm.Key = passwordKey.GetBytes(algorithm.KeySize / 8);
                algorithm.IV = passwordKey.GetBytes(algorithm.BlockSize / 8);
            }

            FileStream inputFileStream = null, outputFileStream = null;
            CryptoStream cryptoStream = null;

            try
            {
                inputFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);

                var fileData = new byte[inputFileStream.Length];
                inputFileStream.Read(fileData, 0, (int)inputFileStream.Length);
                ICryptoTransform encryptor = algorithm.CreateEncryptor();

                outputFileStream = new FileStream(OutputFilenameGenerator.Generate(inputFileName, EncryptAction.Encrypt), FileMode.OpenOrCreate, FileAccess.Write);

                cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(fileData, 0, fileData.Length);
            }
            finally
            {
                if (cryptoStream != null)
                {
                    cryptoStream.Close();
                }
                    
                if (inputFileStream != null)
                {
                    inputFileStream.Close();
                }

                if (outputFileStream != null) 
                { 
                    outputFileStream.Close(); 
                }
            }
        }

        public void Decrypt(string inputFileName)
        {
            var passwordKey = new Rfc2898DeriveBytes(_password, _saltValueBytes);

            ICryptoTransform decryptor;
            using (var algorithm = new RijndaelManaged())
            {
                algorithm.Key = passwordKey.GetBytes(algorithm.KeySize / 8);
                algorithm.IV = passwordKey.GetBytes(algorithm.BlockSize / 8);

                decryptor = algorithm.CreateDecryptor();
            }

            FileStream inputFileStrean = null, outputFileStream = null;
            CryptoStream cryptoStream = null;

            try
            {
                inputFileStrean = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);

                cryptoStream = new CryptoStream(inputFileStrean, decryptor, CryptoStreamMode.Read);
                var fileData = new byte[inputFileStrean.Length];

                cryptoStream.Read(fileData, 0, (int)inputFileStrean.Length);
                outputFileStream = new FileStream(OutputFilenameGenerator.Generate(inputFileName, EncryptAction.Decrypt), FileMode.OpenOrCreate, FileAccess.Write);

                outputFileStream.Write(fileData, 0, fileData.Length);
            }
            finally
            {
                if (cryptoStream != null)
                {
                    cryptoStream.Close();
                }
                    
                if (inputFileStrean != null)
                {
                    inputFileStrean.Close();
                }
                    
                if (outputFileStream != null)
                {
                    outputFileStream.Close();
                }
            }
        }
    }
}
