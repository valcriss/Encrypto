# Encrypto

Encrypto is a command-line utility to encrypt and decrypt directories. It compresses the contents to protect and then encrypts the archive with AES-GCM to produce a single `.crypt` file.

## Technical details
- Key derivation via PBKDF2 (SHA-256, 100,000 iterations) with a 16-byte salt.
- Symmetric AES-256 encryption in GCM mode with a 12-byte initialization vector (IV).
- The resulting file is structured as: `salt || IV || tag || ciphertext`.
- For interoperability, the encrypted content is a ZIP archive of the source directory.

## Usage
### Commands

#### encrypt
Encrypts a directory.
```bash
encrypto encrypt -s <directory> -p <password> [-d <output_file>]
```

#### decrypt
Decrypts a `.crypt` file.
```bash
encrypto decrypt -s <file.crypt> -p <password> [-d <destination_directory>]
```

### Common options
- `-s`, `--source`: path to the source directory or file.
- `-p`, `--password`: password used to derive the key.
- `-d`, `--destination`: output path. For `encrypt`, this is the generated file (default `<source>.crypt`). For `decrypt`, this is the destination directory (default a folder with the same name as the file without extension).

## License

This project is licensed under the [MIT](LICENSE.md) license.
