using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace FileEncrypt
{
    public static class FileEncrypt
    {
        public static void Encrypt(string inputFileName, string outputFileName, string password, byte [] saltValueBytes)
        {
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(password, saltValueBytes);

            RijndaelManaged alg = new RijndaelManaged();
            alg.Key = passwordKey.GetBytes(alg.KeySize / 8);
            alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);

            FileStream fs = null, outFile = null;

            byte[] fileData = null;
            ICryptoTransform encryptor = null;

            CryptoStream encryptStr = null;
            try
            {
                fs = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);

                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                encryptor = alg.CreateEncryptor();

                outFile = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write);

                encryptStr = new CryptoStream(outFile, encryptor, CryptoStreamMode.Write);
                encryptStr.Write(fileData, 0, fileData.Length);
            }
            finally
            {
                if (encryptStr != null)
                    encryptStr.Close();
                if (fs != null)
                    fs.Close();
                if (outFile != null)
                    outFile.Close();
            }
        }

        public static void Decrypt(string inputFileName, string outputFileName, string password, byte[] saltValueBytes)
        {
            Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(password, saltValueBytes);

            RijndaelManaged alg = new RijndaelManaged();
            alg.Key = passwordKey.GetBytes(alg.KeySize / 8);

            alg.IV = passwordKey.GetBytes(alg.BlockSize / 8);
            ICryptoTransform decryptor = alg.CreateDecryptor();
            FileStream fs = null, outFile = null;

            CryptoStream decrStr = null;
            byte[] fileData = null;

            try
            {
                fs = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);

                decrStr = new CryptoStream(fs, decryptor, CryptoStreamMode.Read);
                fileData = new byte[fs.Length];

                decrStr.Read(fileData, 0, (int)fs.Length);
                outFile = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write);

                outFile.Write(fileData, 0, fileData.Length);
            }
            finally
            {
                if (decrStr != null)
                    decrStr.Close();
                if (fs != null)
                    fs.Close();
                if (outFile != null)
                    outFile.Close();
            }
        }
    }
}
