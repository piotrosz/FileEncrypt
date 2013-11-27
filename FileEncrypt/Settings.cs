namespace FileEncrypt
{
    public class Settings
    {
        public Settings()
        {
            SaltFileName = "salt";
        }

        public bool ShowHelp { get; set; }
        public EncryptAction EncryptAction { get; set; }
        public string  InputFileName { get; set; }
        public string SaltFileName { get; set; }
        public string Password { get; set; }
    }
}
