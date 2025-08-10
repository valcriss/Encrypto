using Encrypto;
using Xunit;

namespace encrypto.tests;

public class EncryptionTests
{
    [Fact]
    public void EncryptDecrypt_RoundTripRestoresFiles()
    {
        string password = "P@ssw0rd!";
        string sourceDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(sourceDir);
        string originalFilePath = Path.Combine(sourceDir, "test.txt");
        File.WriteAllText(originalFilePath, "Hello World");

        string encryptedFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        string decryptedDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(decryptedDir);

        try
        {
            var encrypt = new Encrypt(sourceDir, password);
            encrypt.EncryptProcess();
            Assert.NotNull(encrypt.EncryptedData);
            File.WriteAllBytes(encryptedFilePath, encrypt.EncryptedData!);

            var decrypt = new Decrypt(encryptedFilePath, password, decryptedDir);
            decrypt.DecryptProcess();

            string decryptedFilePath = Path.Combine(decryptedDir, "test.txt");
            Assert.True(File.Exists(decryptedFilePath));
            string decryptedContent = File.ReadAllText(decryptedFilePath);
            Assert.Equal("Hello World", decryptedContent);
        }
        finally
        {
            if (Directory.Exists(sourceDir))
                Directory.Delete(sourceDir, true);
            if (File.Exists(encryptedFilePath))
                File.Delete(encryptedFilePath);
            if (Directory.Exists(decryptedDir))
                Directory.Delete(decryptedDir, true);
        }
    }
}
