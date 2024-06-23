using CoindeskExampleApiServer.Models.Option;
using CoindeskExampleApiServer.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace CoindeskExampleApiServerTest
{
    public class AesEncryptionTest
    {
        private AesEncryption _aesEncryption;
        private Mock<IOptions<AesEncryptionInfo>> _mockOptions;
        private AesEncryptionInfo _aesEncryptionInfo;

        [SetUp]
        public void SetUp()
        {
            _aesEncryptionInfo = new AesEncryptionInfo
            {
                Key = "aiLGClgiILxrtVMK2V0eQApe4RGZVFTa",
                IV = "bx25imVzXlRp7w9b"
            };

            _mockOptions = new Mock<IOptions<AesEncryptionInfo>>();
            _mockOptions.Setup(o => o.Value).Returns(_aesEncryptionInfo);

            _aesEncryption = new AesEncryption(_mockOptions.Object);
        }

        [Test]
        public void EncryptString_ValidInput_ShouldReturnEncryptedString()
        {
            // Arrange
            var plainText = "Hello, World!";

            // Act
            var encryptedText = _aesEncryption.EncryptString(plainText);

            // Assert
            Assert.That(encryptedText, Is.Not.Null);
            Assert.That(encryptedText, Is.Not.Empty);
        }

        [Test]
        public void DecryptString_ValidInput_ShouldReturnDecryptedString()
        {
            // Arrange
            var plainText = "Hello, World!";
            var encryptedText = _aesEncryption.EncryptString(plainText);

            // Act
            var decryptedText = _aesEncryption.DecryptString(encryptedText);

            // Assert
            Assert.That(decryptedText, Is.Not.Null);
            Assert.That(decryptedText, Is.Not.Empty);
            Assert.That(decryptedText, Is.EqualTo(plainText));
        }
    }
}
