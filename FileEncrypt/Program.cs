using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using System.Security.Cryptography;
using System.IO;

namespace FileEncrypt
{
    class Program
    {
        static string programName = "FileEncrypt";

        enum CryptAction
        {
            None = 0,
            Invalid = -1,
            Encrypt = 1,
            Decrypt = 2,
        }

        static void Main(string[] args)
        {
            bool show_help = false;
            CryptAction action = CryptAction.None;
            string outputFileName = "";
            string inputFileName = "";
			string saltFileName = "salt";
            string password = "";

            var p = new OptionSet() {
                { 
                    "a|action=",
                    "Encrypt (c) or decrypt (d) the file",
                    v => { 
                        if(v.ToLower() == "c") { action = CryptAction.Encrypt; }
                        else if (v.ToLower() == "d") { action = CryptAction.Decrypt; }
                        else { action = CryptAction.Invalid; }
                    }
                },
                { 
                    "o|out=", 
                    "the output {FILENAME}", 
                    v => outputFileName = v
                },
                {
                    "i|in=",
                    "the input {FILENAME}",
                    v => inputFileName = v
                },
                {
                    "p|password=",
                    "the {PASSWORD}",
                    v => password = v
                },
				{
					"s|salt=",
					"the {SALT} file (default is 'salt')",
					v => saltFileName = v
				},
                { 
                    "h|help",  
                    "show this message and exit", 
                    v => show_help = v != null 
                }
            };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", programName);
                Console.WriteLine(e.Message);
                Console.WriteLine("Try '{0} --help' for more information.", programName);
                return;
            }

            if (show_help)
            {
                ShowHelp(p);
                return;
            }

            var errorMessage = new StringBuilder();
            if (action == CryptAction.None)
                errorMessage.AppendLine("Option -a is missing.");
            else if (action == CryptAction.Invalid)
                errorMessage.AppendLine("Option -a is invalid. Possible values are 'c' (for encrypting) or 'd' (for decrypting).");

            if (string.IsNullOrEmpty(inputFileName))
                errorMessage.AppendLine("Option -i is missing.");

            if (string.IsNullOrEmpty(outputFileName))
                errorMessage.AppendLine("Option -o is missing.");

            if (string.IsNullOrEmpty(password))
                errorMessage.AppendLine("Option -p is missing.");

            string errmsg = errorMessage.ToString();
            if (!string.IsNullOrEmpty(errmsg))
            {
                Console.WriteLine(errmsg);
                ShowHelp(p);
                return;
            }

            try
            {
                if (action == CryptAction.Encrypt)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Crypting");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("{0} -> {1}", inputFileName, outputFileName);
                    Console.ResetColor();
					
					SaltStore saltStore = new SaltStore(saltFileName);
					if(!saltStore.SaltCreated())
						saltStore.Save(SaltCreator.Create());
					
                    FileEncrypt.Encrypt(inputFileName, outputFileName, password, saltStore.Get());
                }
                else if (action == CryptAction.Decrypt)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Decrypting");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("{0} -> {1}", inputFileName, outputFileName);
                    Console.ResetColor();
					
					SaltStore saltStore = new SaltStore(saltFileName);
					
                    FileEncrypt.Decrypt(inputFileName, outputFileName, password, saltStore.Get());
                }
            }
            catch (CryptographicException ex) { ShowException(ex); }
            catch (IOException ex) { ShowException(ex); }
            catch (Exception ex) { ShowException(ex); }
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: {0} [OPTIONS]+", programName);
            Console.WriteLine("Encrypt and decrypt files.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        static void ShowException(Exception ex)
        {
            Console.WriteLine("Exception occured:");
            Console.WriteLine(ex.ToString());
        }
    }
}
