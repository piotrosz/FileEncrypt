using System;
using NDesk.Options;

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

            var validator = new SettingsValidator();

            var errorMessages = validator.Validate(settings);
            if (errorMessages.Count > 0)
            {
                errorMessages.ForEach(Console.WriteLine);
                ConsoleWriter.ShowHelp(optionSet);
                return;
            }

            try
            {
                var saltStore = new SaltStore(settings.SaltFileName);
                var encrypter = new FileEncryptRijndael(settings.Password, saltStore.CreateAndGet());

                switch (settings.EncryptAction)
                {
                    case EncryptAction.Decrypt:
                        ConsoleWriter.ShowDecryptMessage(settings);
                        encrypter.Decrypt(settings.InputFileName);
                        break;

                    case EncryptAction.Encrypt:
                        ConsoleWriter.ShowEncryptMessage(settings);
                        encrypter.Encrypt(settings.InputFileName);
                        break;
                }
            }
            //catch (CryptographicException ex)
            //catch (IOException ex)
            catch (Exception ex)
            {
                ConsoleWriter.ShowException(ex);
            }
        }
    }
}
