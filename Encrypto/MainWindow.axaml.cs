using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Encrypto;
using System;
using System.IO;

namespace Encrypto.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private async void Encrypt_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog();
        var folder = await dialog.ShowAsync(this);
        if (string.IsNullOrEmpty(folder))
            return;

        var password = PasswordBox.Text ?? string.Empty;
        var encrypt = new Encrypt(folder, password);
        encrypt.EncryptProcess();

        if (encrypt.EncryptedData != null)
        {
            var save = new SaveFileDialog { InitialFileName = Path.GetFileName(folder) + ".crypt" };
            var output = await save.ShowAsync(this);
            if (!string.IsNullOrEmpty(output))
            {
                File.WriteAllBytes(output, encrypt.EncryptedData);
            }
        }
    }

    private async void Decrypt_Click(object? sender, RoutedEventArgs e)
    {
        var open = new OpenFileDialog
        {
            AllowMultiple = false,
            Filters = { new FileDialogFilter { Name = "Encrypted Files", Extensions = { "crypt" } } }
        };
        var files = await open.ShowAsync(this);
        if (files == null || files.Length == 0)
            return;

        var destDialog = new OpenFolderDialog();
        var dest = await destDialog.ShowAsync(this);
        if (string.IsNullOrEmpty(dest))
            return;

        var password = PasswordBox.Text ?? string.Empty;
        var decrypt = new Decrypt(files[0], password, dest);
        decrypt.DecryptProcess();
    }

    private async void OnDrop(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataFormats.FileNames))
            return;

        var files = e.Data.GetFileNames();
        if (files == null)
            return;

        foreach (var path in files)
        {
            if (Directory.Exists(path))
            {
                var password = PasswordBox.Text ?? string.Empty;
                var encrypt = new Encrypt(path, password);
                encrypt.EncryptProcess();
                if (encrypt.EncryptedData != null)
                {
                    var save = new SaveFileDialog { InitialFileName = Path.GetFileName(path) + ".crypt" };
                    var output = await save.ShowAsync(this);
                    if (!string.IsNullOrEmpty(output))
                    {
                        File.WriteAllBytes(output, encrypt.EncryptedData);
                    }
                }
            }
            else if (Path.GetExtension(path).Equals(".crypt", StringComparison.OrdinalIgnoreCase))
            {
                var destDialog = new OpenFolderDialog();
                var dest = await destDialog.ShowAsync(this);
                if (string.IsNullOrEmpty(dest))
                    continue;

                var password = PasswordBox.Text ?? string.Empty;
                var decrypt = new Decrypt(path, password, dest);
                decrypt.DecryptProcess();
            }
        }
    }
}

