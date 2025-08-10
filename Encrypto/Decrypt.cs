using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
            {
                using (MemoryStream fsOutput = new MemoryStream())
                {
                    using (AesManaged aes = new AesManaged())
                    {
                        aes.Key = GenerateByteValue(32);
                        aes.IV = GenerateByteValue(16);

                        // Perform encryption
                        ICryptoTransform decryptor = aes.CreateDecryptor();
                        using (CryptoStream cs = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
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
