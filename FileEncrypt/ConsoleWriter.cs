using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Console.WriteLine("Exception occured:");
            Console.WriteLine(ex.ToString());
        }

        public static void WriteException(OptionException exception)
        {
            Console.Write("{0}: ", ProgramName);
            Console.WriteLine(exception.Message);
            Console.WriteLine("Try '{0} --help' for more information.", ProgramName);
        }

        public static void ShowEncryptMessage(Settings settings)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Encrypting");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("{0} -> {1}", settings.InputFileName, settings.OutputFileName);
            Console.ResetColor();
        }

        public static void ShowDecryptMessage(Settings settings)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Decrypting");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0} -> {1}", settings.InputFileName, settings.OutputFileName);
            Console.ResetColor();
        }
    }
}
