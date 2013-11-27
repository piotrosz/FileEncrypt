using Xunit;

namespace FileEncrypt.Test
{
    public class SettingsValidatorFacts
    {
        public class ValidateMethod
        {
            [Fact]
            public void validation_should_pass_for_encrypt_option_input_filename_and_password()
            {
                // Arrange
                var settings = new Settings
                    {
                        EncryptAction = EncryptAction.Encrypt,
                        InputFileName = "test",
                        Password = "test"
                    };
                var target = new SettingsValidator();

                // Act
                var result = target.Validate(settings);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(0, result.Count);
            }

            [Fact]
            public void validation_should_fail_for_filename_which_does_not_follow_convention()
            {
                // Arrange
                var settings = new Settings
                    {
                        EncryptAction = EncryptAction.Decrypt,
                        InputFileName = "wrongname",
                        Password = "test"
                    };
                var target = new SettingsValidator();

                // Act
                var result = target.Validate(settings);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Count);
            }
        }
    }
}
