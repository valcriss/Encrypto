using System;
using System.IO;
using Xunit;

namespace Encrypto.Tests
{
    public class TempFileTests
    {
        [Fact]
        public void EncryptProcess_DoesNotLeaveTempFiles()
        {
            string testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "test.txt"), "content");

            int before = Directory.GetFiles(Path.GetTempPath()).Length;
            var encrypt = new Encrypt(testDir, "password");
            encrypt.EncryptProcess();
            int after = Directory.GetFiles(Path.GetTempPath()).Length;

            Assert.Equal(before, after);
            Directory.Delete(testDir, true);
        }

        [Fact]
        public void DecryptProcess_DoesNotLeaveTempFiles()
        {
            // Prepare a directory to encrypt
            string sourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(sourceDir);
            File.WriteAllText(Path.Combine(sourceDir, "test.txt"), "content");

            var encrypt = new Encrypt(sourceDir, "password");
            encrypt.EncryptProcess();

            // Write encrypted data to a file for decryption
            string encryptedFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            File.WriteAllBytes(encryptedFile, encrypt.EncryptedData!);

            string destDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(destDir);

            int before = Directory.GetFiles(Path.GetTempPath()).Length;
            var decrypt = new Decrypt(encryptedFile, "password", destDir);
            decrypt.DecryptProcess();
            int after = Directory.GetFiles(Path.GetTempPath()).Length;

            Assert.Equal(before, after);

            // cleanup
            Directory.Delete(sourceDir, true);
            Directory.Delete(destDir, true);
            File.Delete(encryptedFile);
        }
    }
}
