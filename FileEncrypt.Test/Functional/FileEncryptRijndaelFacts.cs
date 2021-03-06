﻿using System.IO;
using Xunit;

namespace FileEncrypt.Test.Functional
{
    public class FileEncryptRijndaelFacts
    {
        [Fact]
        public void decrypted_file_should_be_the_same_as_original()
        {
            // Arrange
            const string inputFileName = @"TestFiles\SampleTextFileToEncrypt.txt";
            string fileContents = File.ReadAllText(inputFileName);
            const string password = "pass";
            var target = new FileEncryptRijndael(password, new SaltStore("salt").CreateAndGet());

            // Act
            target.Encrypt(inputFileName);
            target.Decrypt(OutputFilenameGenerator.Generate(inputFileName, EncryptAction.Encrypt));

            // Assert
            string descyptedFileContent = File.ReadAllText(inputFileName);

            Assert.True(descyptedFileContent.StartsWith(fileContents));
        }
    }
}
