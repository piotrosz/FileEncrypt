using System;
using System.IO;

namespace FileEncrypt
{
    public static class OutputFilenameGenerator
    {
        public static string Generate(string fileName, EncryptAction encryptAction)
        {
            switch (encryptAction)
            {
                case EncryptAction.Encrypt:
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    return fileName.Replace(fileNameWithoutExtension, fileNameWithoutExtension + "_encrypted");
                case EncryptAction.Decrypt:
                    return fileName.Replace("_encrypted", "");
                default:
                    throw new ArgumentOutOfRangeException("encryptAction");
            }
        }

    }
}
