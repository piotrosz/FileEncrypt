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
                    "a|action=",
                    "Encrypt (c) or decrypt (d) the file",
                    v => { 
                        if(v.ToLower() == "c") { settings.CryptAction = CryptAction.Encrypt; }
                        else if (v.ToLower() == "d") { settings.CryptAction = CryptAction.Decrypt; }
                    }
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
