using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReinST.Central.Helpers;

namespace ReinST.Central.UnitTest.SampleTests
{
    [TestClass]
    public class Sample1
    {
        [TestMethod]
        public void AuthenticateViaBCryptHash()
        {
            // Arrange
            string plainText = "Hello world!";
            string hash = StringHelper.GenerateBCryptHash(plainText);

            // Act
            bool isValid = StringHelper.VerifyBCryptHash(plainText, hash);

            // Assert
            Assert.IsTrue(isValid, plainText + " is correctly hashed!");
        }

        [TestMethod]
        public void TestAESEncryptor()
        {
            // Arrange
            string plainText = "Encrypt me!";

            // Act
            string encrypted = StringHelper.AESEncrypt(plainText);
            string decrypted = StringHelper.AESDecrypt(encrypted);

            // Assert
            Assert.AreEqual(plainText, decrypted, plainText + " is correctly encrypted via AES!");
        }

        [TestMethod]
        public void TestBase64Encoder()
        {
            // Arrange
            string plainText = "Encode me!";

            // Act
            string encoded = StringHelper.Base64Encode(plainText);
            string decoded = StringHelper.Base64Decode(encoded);

            // Assert
            Assert.AreEqual(plainText, decoded, plainText + " is correctly encoded in Base64!");
        }
    }
}
