using CommandLine;

namespace Encrypto;

[Verb("encrypt", HelpText = "Encrypt a directory.")]
public class EncryptParameters
{
    [Option('s', "source", Required = true, HelpText = "Source directory to encrypt from.")]
    public string Source { get; set; }

    [Option('p', "password", Required = true, HelpText = "Password to encrypt the file content.")]
    public string Password { get; set; }

    [Option('d', "destination", Required = true, HelpText = "Destination file for encrypted files.")]
    public string Destination { get; set; }
}