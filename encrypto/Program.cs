using CommandLine;

namespace Encrypto
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<EncryptParameters, DecryptParameters>(args)
                .MapResult(
                    (EncryptParameters opts) => RunEncrypt(opts),
                    (DecryptParameters opts) => RunDecrypt(opts),
                    errs => 1
                );
        }

        private static int RunEncrypt(EncryptParameters opts)
        {
            string destination = !string.IsNullOrEmpty(opts.Destination)
                ? opts.Destination
                : Path.Combine(Environment.CurrentDirectory, $"{Path.GetFileNameWithoutExtension(opts.Source)}.crypt");
            Console.WriteLine($"[ENCRYPT] Source: {opts.Source}, Destination: {destination}");
            try
            {
                var encrypt = new Encrypt(opts.Source, opts.Password);
                encrypt.EncryptProcess();

                if (encrypt.EncryptedData != null)
                {
                    File.WriteAllBytes(destination, encrypt.EncryptedData);
                    Console.WriteLine($"Encrypted data written to {destination}");
                }
                else
                {
                    Console.WriteLine("Encryption failed, no data was produced.");
                    return 2;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Encryption failed.");
                Console.WriteLine(e);
                return 1;
            }

            return 0;
        }

        private static int RunDecrypt(DecryptParameters opts)
        {
            string destination = !string.IsNullOrEmpty(opts.Destination)
                ? opts.Destination
                : Path.Combine(Environment.CurrentDirectory, Path.GetFileNameWithoutExtension(opts.Source));
            Console.WriteLine($"[DECRYPT] Source: {opts.Source}, Destination: {destination}");
            try
            {
                if (File.Exists(destination))
                {
                    Console.WriteLine($"Destination path points to an existing file: {destination}");
                    return 1;
                }

                if (!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }

                var decrypt = new Decrypt(opts.Source, opts.Password, destination);
                decrypt.DecryptProcess();
                Console.WriteLine("Decryption completed successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed. Bad password or file format?");
                return 1;
            }

            return 0;
        }
    }
}