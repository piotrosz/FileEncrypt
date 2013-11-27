using System;
using NDesk.Options;

namespace FileEncrypt
{
    public static class ConsoleWriter
    {
        const string ProgramName = "FileEncrypt";

        public static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: {0} [OPTIONS]+", ProgramName);
            Console.WriteLine("Encrypt and decrypt files.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        public static void ShowException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Exception occured:");
            Console.WriteLine(ex.ToString());
            Console.ResetColor();
        }

        public static void WriteException(OptionException exception)
        {
            Console.Write("{0}: ", ProgramName);
            ShowException(exception);
            Console.WriteLine("Try '{0} --help' for more information.", ProgramName);
        }

        public static void ShowEncryptMessage(Settings settings)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Encrypting... ");
            Console.WriteLine("{0} -> {1}", settings.InputFileName, OutputFilenameGenerator.Generate(settings.InputFileName, EncryptAction.Encrypt));
            Console.ResetColor();
        }

        public static void ShowDecryptMessage(Settings settings)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Decrypting... ");
            Console.WriteLine("{0} -> {1}", settings.InputFileName, OutputFilenameGenerator.Generate(settings.InputFileName, EncryptAction.Decrypt));
            Console.ResetColor();
        }
    }
}
