namespace FileEncrypt
{
    public class Settings
    {
        public Settings()
        {
            SaltFileName = "salt";
        }

        public bool ShowHelp { get; set; }
        public CryptAction CryptAction { get; set; }
        public string OutputFileName { get; set; }
        public string  InputFileName { get; set; }
        public string SaltFileName { get; set; }
        public string Password { get; set; }
    }
}
