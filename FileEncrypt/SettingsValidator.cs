using System.Collections.Generic;

namespace FileEncrypt
{
    public class SettingsValidator
    {
        public SettingsValidator()
        {
            ErrorMessages = new List<string>();
        }

        public List<string> ErrorMessages { get; private set; } 

        public List<string> Validate(Settings settings)
        {
            if (settings.EncryptAction == EncryptAction.None)
            {
                ErrorMessages.Add("-encrypt -decrypt option is missing.");
            }

            if (string.IsNullOrEmpty(settings.InputFileName))
            {
                ErrorMessages.Add("Option -i is missing.");
            }

            if (string.IsNullOrEmpty(settings.Password))
            {
                ErrorMessages.Add("Option -p is missing.");
            }

            if (settings.EncryptAction == EncryptAction.Decrypt && !settings.InputFileName.Contains("_encrypted"))
            {
                ErrorMessages.Add("Encrypted file must end with '_encrypted' prefix (e.g. 'passwords_encrypted.txt')");
            }

            return ErrorMessages;
        }
    }
}
