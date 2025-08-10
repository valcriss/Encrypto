using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Encrypto
{
    public class Decrypt
    {
        private string EncryptFile { get; set; }
        private string EncryptPassword { get; set; }
        private string Destination { get; set; }
        public byte[]? DecryptedData { get; set; }

        public Decrypt(string encryptFile, string encryptPassword, string destination)
        {
            Destination = destination;
            EncryptFile = encryptFile;
            EncryptPassword = encryptPassword;
            DecryptedData = null;
        }

        public void DecryptProcess()
        {
            DecryptedData = DecryptFile(EncryptFile);
            string filename = Path.GetTempFileName();
            try
            {
                UnZipFile(filename);
            }
            finally
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        private void UnZipFile(string filename)
        {
            if (DecryptedData == null) return;
            // Using a MemoryStream with ZipArchive could remove the need for this temporary file.
            File.WriteAllBytes(filename, DecryptedData);
            ZipFile.ExtractToDirectory(filename, Destination);
        }

        private byte[] DecryptFile(string inputFilePath)
        {
            byte[] fileBytes = File.ReadAllBytes(inputFilePath);

            byte[] salt = new byte[16];
            byte[] iv = new byte[12];
            byte[] tag = new byte[16];
            Buffer.BlockCopy(fileBytes, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(fileBytes, salt.Length, iv, 0, iv.Length);
            Buffer.BlockCopy(fileBytes, salt.Length + iv.Length, tag, 0, tag.Length);

            int cipherStart = salt.Length + iv.Length + tag.Length;
            byte[] cipher = new byte[fileBytes.Length - cipherStart];
            Buffer.BlockCopy(fileBytes, cipherStart, cipher, 0, cipher.Length);

            using (var pbkdf2 = new Rfc2898DeriveBytes(EncryptPassword, salt, 100_000, HashAlgorithmName.SHA256))
            using (var aes = new AesGcm(pbkdf2.GetBytes(32), 16))
            {
                byte[] plaintext = new byte[cipher.Length];
                try
                {
                    aes.Decrypt(iv, cipher, tag, plaintext);
                }
                catch (CryptographicException ex)
                {
                    throw new CryptographicException("Authentication tag mismatch.", ex);
                }
                return plaintext;
            }

        }
    }
}
