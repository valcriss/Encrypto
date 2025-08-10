using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

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
            {
                using (MemoryStream fsOutput = new MemoryStream())
                {
                    using (AesManaged aes = new AesManaged())
                    {
                        aes.Key = GenerateByteValue(32);
                        aes.IV = GenerateByteValue(16);

                        // Perform encryption
                        ICryptoTransform encryptor = aes.CreateEncryptor();
                        using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                        {
                            fsInput.CopyTo(cs);
                        }
                    }
                    return fsOutput.ToArray();
                }
            }
        }

        private byte[] GenerateByteValue(int length)
        {
            int index = 0;
            string v = EncryptPassword;
            while (Encoding.UTF8.GetBytes(v).Length < length)
            {
                v += EncryptPassword[index];
                index++;
                if (index == EncryptPassword.Length)
                    index = 0;
            }

            while (Encoding.UTF8.GetBytes(v).Length > length)
            {
                v = v.Substring(v.Length - 1);
            }

            return Encoding.UTF8.GetBytes(v);
        }

    }
}
