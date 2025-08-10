using CommandLine;

namespace Encrypto;

[Verb("decrypt", HelpText = "Decrypt a file.")]
public class DecryptParameters
{
    [Option('s', "source", Required = true, HelpText = "Source file to decrypt from.")]
    public string Source { get; set; }

    [Option('p', "password", Required = true, HelpText = "Password to decrypt the file content.")]
    public string Password { get; set; }

    [Option('d', "destination", Default = null, HelpText = "Destination directory to extract decrypted files.")]
    public string? Destination { get; set; } = null;
}