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
        static void Main(string[] args)
        {
            var settings = new Settings();
            var optionSet = OptionSetHelper.DefineOptions(settings);

            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                ConsoleWriter.WriteException(e);
            }

            if (settings.ShowHelp)
            {
                ConsoleWriter.ShowHelp(optionSet);
                return;
            }

            var errorMessage = ValidateSettings(settings);
            if (errorMessage.Length > 0)
            {
                Console.WriteLine(errorMessage.ToString());
                ConsoleWriter.ShowHelp(optionSet);
                return;
            }

            try
            {
                var saltStore = new SaltStore(settings.SaltFileName);
                if (!saltStore.SaltCreated())
                {
                    saltStore.Save(SaltCreator.Create());
                }

                var encrypter = new FileEncryptRijndael(settings.InputFileName, settings.OutputFileName, settings.Password, saltStore.Get());

                switch (settings.CryptAction)
                {
                    case CryptAction.Decrypt:

                        ConsoleWriter.ShowDecryptMessage(settings);
                        encrypter.Decrypt();

                        return;
                    case CryptAction.Encrypt:

                        ConsoleWriter.ShowEncryptMessage(settings);
                        encrypter.Encrypt();

                        return;
                }
            }
            //catch (CryptographicException ex)
            //catch (IOException ex)
            catch (Exception ex)
            {
                ConsoleWriter.ShowException(ex);
            }
        }

        private static StringBuilder ValidateSettings(Settings settings)
        {
            var errorMessage = new StringBuilder();

            if (settings.CryptAction == CryptAction.None)
            {
                errorMessage.AppendLine("Encrypt/Decrypt option is missing.");
            }

            if (string.IsNullOrEmpty(settings.InputFileName))
            {
                errorMessage.AppendLine("Option -i is missing.");
            }

            if (string.IsNullOrEmpty(settings.OutputFileName))
            {
                errorMessage.AppendLine("Option -o is missing.");
            }

            if (string.IsNullOrEmpty(settings.Password))
            {
                errorMessage.AppendLine("Option -p is missing.");
            }

            return errorMessage;
        }
    }
}
