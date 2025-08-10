# Encrypto

Application de chiffrement/déchiffrement basée sur Avalonia.

## Publication et exécution sous Linux

### Prérequis
- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)

### Publier
Utiliser le profil de publication Linux fourni :

```bash
./publish-linux.sh
```

Le binaire sera généré dans `Encrypto/bin/Release/net8.0/linux-x64/publish/`.

### Lancer l'application

```bash
./run-linux.sh
```

Ce script publie l'application si nécessaire puis exécute le binaire généré.
