using System.IO;
using Xunit;

namespace FileEncrypt.Test.Functional
{
    public class FileEncryptRsaFacts
    {
        [Fact]
        public void decrypted_file_should_be_the_same_as_original()
        {
            // Arrange
            const string inputFileName = @"TestFiles\SampleTextFileToEncrypt2.txt";
            string fileContents = File.ReadAllText(inputFileName);
            var target = new FileEncryptRsa("public_and_private_key.txt");

            // Act
            target.Encrypt(inputFileName);
            target.Decrypt(OutputFilenameGenerator.Generate(inputFileName, EncryptAction.Encrypt));

            // Assert
            string descyptedFileContent = File.ReadAllText(inputFileName);

            Assert.True(descyptedFileContent.StartsWith(fileContents));
        }
    }
}
