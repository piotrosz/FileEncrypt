using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileEncrypt
{
    public static class OptionSetHelper
    {
        public static OptionSet DefineOptions(Settings settings)
        {
            return new OptionSet {
                { 
                    "encrypt",
                    "Encrypt  the file",
                    v => settings.CryptAction = CryptAction.Encrypt
                },
                {
                    "decrypt",
                    "Decrypt  the file",
                    v => settings.CryptAction = CryptAction.Decrypt
                },
                { 
                    "o|out=", 
                    "the output {FILENAME}", 
                    v => settings.OutputFileName = v
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
