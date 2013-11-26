using System.Security.Cryptography;
using System.IO;

namespace FileEncrypt
{
    public class FileEncryptRijndael : IEncrypter
    {
        private readonly string _inputFileName;
        private readonly string _outputFileName;
        private readonly string _password;
        private readonly byte[] _saltValueBytes;

        public FileEncryptRijndael(string inputFileName, string outputFileName, string password, byte[] saltValueBytes)
        {
            _inputFileName = inputFileName;
            _outputFileName = outputFileName;
            _password = password;
            _saltValueBytes = saltValueBytes;
        }

        public void Encrypt()
        {
            var passwordKey = new Rfc2898DeriveBytes(_password, _saltValueBytes);

            var algorithm = new RijndaelManaged();
            algorithm.Key = passwordKey.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = passwordKey.GetBytes(algorithm.BlockSize / 8);

            FileStream fileStream = null, outFile = null;

            CryptoStream cryptoStream = null;
            try
            {
                fileStream = new FileStream(_inputFileName, FileMode.Open, FileAccess.Read);

                var fileData = new byte[fileStream.Length];
                fileStream.Read(fileData, 0, (int)fileStream.Length);
                ICryptoTransform encryptor = algorithm.CreateEncryptor();

                outFile = new FileStream(_outputFileName, FileMode.OpenOrCreate, FileAccess.Write);

                cryptoStream = new CryptoStream(outFile, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(fileData, 0, fileData.Length);
            }
            finally
            {
                if (cryptoStream != null)
                {
                    cryptoStream.Close();
                }
                    
                if (fileStream != null)
                {
                    fileStream.Close();
                }

                if (outFile != null) 
                { 
                    outFile.Close(); 
                }
            }
        }

        public void Decrypt()
        {
            var passwordKey = new Rfc2898DeriveBytes(_password, _saltValueBytes);

            var algorithm = new RijndaelManaged();
            algorithm.Key = passwordKey.GetBytes(algorithm.KeySize / 8);

            algorithm.IV = passwordKey.GetBytes(algorithm.BlockSize / 8);
            ICryptoTransform decryptor = algorithm.CreateDecryptor();
            FileStream fileStream = null, outFile = null;

            CryptoStream cryptoStream = null;

            try
            {
                fileStream = new FileStream(_inputFileName, FileMode.Open, FileAccess.Read);

                cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read);
                var fileData = new byte[fileStream.Length];

                cryptoStream.Read(fileData, 0, (int)fileStream.Length);
                outFile = new FileStream(_outputFileName, FileMode.OpenOrCreate, FileAccess.Write);

                outFile.Write(fileData, 0, fileData.Length);
            }
            finally
            {
                if (cryptoStream != null)
                {
                    cryptoStream.Close();
                }
                    
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                    
                if (outFile != null)
                {
                    outFile.Close();
                }
            }
        }
    }
}
