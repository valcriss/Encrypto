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
            UnZipFile();
        }

        private void UnZipFile()
        {
            if (DecryptedData == null) return;
            string filename = Path.GetTempFileName();
            File.WriteAllBytes(filename, DecryptedData);
            ZipFile.ExtractToDirectory(filename, Destination);
        }

        private byte[] DecryptFile(string inputFilePath)
        {
            byte[] fileBytes = File.ReadAllBytes(inputFilePath);

            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            Buffer.BlockCopy(fileBytes, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(fileBytes, salt.Length, iv, 0, iv.Length);

            int cipherStart = salt.Length + iv.Length;
            byte[] cipher = new byte[fileBytes.Length - cipherStart];
            Buffer.BlockCopy(fileBytes, cipherStart, cipher, 0, cipher.Length);

            using (var pbkdf2 = new Rfc2898DeriveBytes(EncryptPassword, salt, 100_000, HashAlgorithmName.SHA256))
            using (Aes aes = Aes.Create())
            {
                aes.Key = pbkdf2.GetBytes(32);
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipher, 0, cipher.Length);
                    }
                    return ms.ToArray();
                }
            }

        }
    }
}
