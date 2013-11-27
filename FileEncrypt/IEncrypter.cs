namespace FileEncrypt
{
    interface IEncrypter
    {
        void Encrypt(string inputFileName);
        void Decrypt(string inputFileName);
    }
}
