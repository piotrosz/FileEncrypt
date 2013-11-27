using NDesk.Options;

namespace FileEncrypt
{
    public static class OptionSetHelper
    {
        public static OptionSet DefineOptions(Settings settings)
        {
            return new OptionSet 
            {
                { 
                    "encrypt",
                    "Encrypt  the file",
                    v => settings.EncryptAction = EncryptAction.Encrypt
                },
                {
                    "decrypt",
                    "Decrypt  the file",
                    v => settings.EncryptAction = EncryptAction.Decrypt
                },
                {
                    "i|in=",
                    "the input {FILENAME}",
                    v => settings.InputFileName = v
                },
                {
                    "p|password=",
                    "the {PASSWORD}",
                    v => settings.Password = v
                },
				{
					"s|salt=",
					"the {SALT} file (default is 'salt')",
					v => settings.SaltFileName = v
				},
                { 
                    "h|help",  
                    "show this message and exit", 
                    v => settings.ShowHelp = v != null 
                }
            };
        }
    }
}
