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

## Format du fichier chiffré

Pour l'interopérabilité, le fichier résultant du chiffrement est la concaténation des
champs suivants :

1. **Sel** : 16 octets utilisés pour la dérivation de clé (PBKDF2).
2. **IV** : 12 octets aléatoires utilisés comme vecteur de démarrage pour AES‑GCM.
3. **Tag** : 16 octets de tag d'authentification généré par AES‑GCM.
4. **Ciphertext** : les données chiffrées.

L'ordre est strictement `sel || IV || tag || ciphertext`.
