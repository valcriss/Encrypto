using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Encrypto
{
    public class Encrypt
    {
        private string EncryptDirectory { get; set; }
        private string EncryptPassword { get; set; }

        public byte[]? EncryptedData { get; set; }

        public Encrypt(string encryptDirectory, string encryptPassword)
        {
            EncryptDirectory = encryptDirectory;
            EncryptPassword = encryptPassword;
            EncryptedData = null;
        }

        public void EncryptProcess()
        {
            string zipFilename = CreateZipFile();
            EncryptedData = EncryptFile(zipFilename);
        }

        private string CreateZipFile()
        {
            string filename = Path.GetTempFileName();
            if (File.Exists(filename)) { File.Delete(filename); }
            ZipFile.CreateFromDirectory(EncryptDirectory, filename);
            return filename;
        }

        private byte[] EncryptFile(string inputFilePath)
        {
            using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
            using (MemoryStream fsEncrypted = new MemoryStream())
            {
                byte[] salt = RandomNumberGenerator.GetBytes(16);
                byte[] iv = RandomNumberGenerator.GetBytes(16);

                using (var pbkdf2 = new Rfc2898DeriveBytes(EncryptPassword, salt, 100_000, HashAlgorithmName.SHA256))
                using (Aes aes = Aes.Create())
                {
                    aes.Key = pbkdf2.GetBytes(32);
                    aes.IV = iv;

                    using (var cryptoStream = new CryptoStream(fsEncrypted, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        fsInput.CopyTo(cryptoStream);
                    }
                }

                byte[] cipher = fsEncrypted.ToArray();
                byte[] output = new byte[salt.Length + iv.Length + cipher.Length];
                Buffer.BlockCopy(salt, 0, output, 0, salt.Length);
                Buffer.BlockCopy(iv, 0, output, salt.Length, iv.Length);
                Buffer.BlockCopy(cipher, 0, output, salt.Length + iv.Length, cipher.Length);
                return output;
            }
        }

    }
}
