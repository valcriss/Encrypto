using System.IO.Compression;
using System.Security.Cryptography;

namespace Encrypto;

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
            try
            {
                EncryptedData = EncryptFile(zipFilename);
            }
            finally
            {
                if (File.Exists(zipFilename))
                {
                    File.Delete(zipFilename);
                }
            }
        }

        private string CreateZipFile()
        {
            string filename = Path.GetTempFileName();
            if (File.Exists(filename)) { File.Delete(filename); }
            // An alternative approach could use a MemoryStream or FileOptions.DeleteOnClose
            // to avoid creating a physical temporary file.
            ZipFile.CreateFromDirectory(EncryptDirectory, filename);
            return filename;
        }

        private byte[] EncryptFile(string inputFilePath)
        {
            byte[] plaintext = File.ReadAllBytes(inputFilePath);
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] iv = RandomNumberGenerator.GetBytes(12);

            using (var pbkdf2 = new Rfc2898DeriveBytes(EncryptPassword, salt, 100_000, HashAlgorithmName.SHA256))
            using (var aes = new AesGcm(pbkdf2.GetBytes(32), 16))
            {
                byte[] cipher = new byte[plaintext.Length];
                byte[] tag = new byte[16];
                aes.Encrypt(iv, plaintext, cipher, tag);

                byte[] output = new byte[salt.Length + iv.Length + tag.Length + cipher.Length];
                Buffer.BlockCopy(salt, 0, output, 0, salt.Length);
                Buffer.BlockCopy(iv, 0, output, salt.Length, iv.Length);
                Buffer.BlockCopy(tag, 0, output, salt.Length + iv.Length, tag.Length);
                Buffer.BlockCopy(cipher, 0, output, salt.Length + iv.Length + tag.Length, cipher.Length);
                return output;
            }
        }

    }