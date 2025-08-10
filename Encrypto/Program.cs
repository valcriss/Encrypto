using System;
using System.IO;

namespace Encrypto
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("  encrypto encrypt <directory> <password>");
                Console.WriteLine("  encrypto decrypt <file> <password> <destination>");
                return;
            }

            switch (args[0].ToLowerInvariant())
            {
                case "encrypt" when args.Length == 3:
                    var encrypt = new Encrypt(args[1], args[2]);
                    encrypt.EncryptProcess();
                    if (encrypt.EncryptedData != null)
                    {
                        var outputFile = Path.ChangeExtension(args[1], ".crypt");
                        File.WriteAllBytes(outputFile, encrypt.EncryptedData);
                        Console.WriteLine($"Encrypted data written to {outputFile}");
                    }
                    break;
                case "decrypt" when args.Length == 4:
                    var decrypt = new Decrypt(args[1], args[2], args[3]);
                    decrypt.DecryptProcess();
                    Console.WriteLine("Decryption completed.");
                    break;
                default:
                    Console.WriteLine("Usage:");
                    Console.WriteLine("  encrypto encrypt <directory> <password>");
                    Console.WriteLine("  encrypto decrypt <file> <password> <destination>");
                    break;
            }
        }
    }
}
