using System.Diagnostics;

namespace Encrypto
{
    public partial class General : Form
    {
        private const string DROP_FILE = "Drop directory or crypted file here";
        private const string ENCRYPTING = "Encrypting...";
        private const string ENCRYPT_BUTTON_ENCRYPT = "Encrypt";
        private const string ENCRYPT_BUTTON_DECRYPT = "Decrypt";

        private bool EncryptingDirectory = true;

        private string? DirectoryToEncrypt { get; set; }

        private string? FileToDecrypt { get; set; }
        private string? EncryptPassword { get; set; }
        public General()
        {
            InitializeComponent();
            DirectoryToEncrypt = null;
            FileToDecrypt = null;
            UpdatePasswordInput("");
        }

        private void dropFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e == null || e.Data == null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void dropFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e == null || e.Data == null) return;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0 || files.Length > 1)
            {
                UpdateDropFilesLabel(DROP_FILE);
                UpdatePasswordInput("");
                DirectoryToEncrypt = null;
                FileToDecrypt = null;
                MessageBox.Show("Only one directory can be processed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string file = files[0];
            if (file == null)
            {
                UpdateDropFilesLabel(DROP_FILE);
                UpdatePasswordInput("");
                DirectoryToEncrypt = null;
                FileToDecrypt = null;
                return;
            }

            if (File.Exists(file) && Path.GetExtension(file) != ".crypt")
            {
                UpdateDropFilesLabel(DROP_FILE);
                UpdatePasswordInput("");
                DirectoryToEncrypt = null;
                FileToDecrypt = null;
                MessageBox.Show("Only .crypt files can be dropped", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(file) && !Directory.Exists(file))
            {
                UpdateDropFilesLabel(DROP_FILE);
                UpdatePasswordInput("");
                DirectoryToEncrypt = null;
                FileToDecrypt = null;
                MessageBox.Show("Selected item is not a directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EncryptingDirectory = Directory.Exists(file);

            string? itemName = Path.GetFileNameWithoutExtension(file);
            if (itemName == null)
            {
                UpdateDropFilesLabel(DROP_FILE);
                UpdatePasswordInput("");
                DirectoryToEncrypt = null;
                FileToDecrypt = null;
                return;
            }
            UpdateDropFilesLabel(itemName);

            DirectoryToEncrypt = EncryptingDirectory ? file : null;
            FileToDecrypt = EncryptingDirectory ? null : file;

            if (!CheckEncryptButton())
                passwordInput.Focus();
        }

        private void UpdateDropFilesLabel(string value)
        {
            dropFiles.Text = value;
        }

        private void UpdatePasswordInput(string value)
        {
            passwordInput.Text = value;
        }

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {
            CheckEncryptButton();
        }

        private bool CheckEncryptButton()
        {
            if (EncryptingDirectory)
            {
                encryptButton.Text = ENCRYPT_BUTTON_ENCRYPT;
                if (string.IsNullOrEmpty(DirectoryToEncrypt))
                {
                    UpdateEncryptButton(false);
                    return false;
                }

                if (passwordInput.Text.Length < 8)
                {
                    UpdateEncryptButton(false);
                    return false;
                }

                UpdateEncryptButton(true);
            }
            else
            {
                encryptButton.Text = ENCRYPT_BUTTON_DECRYPT;
                if (string.IsNullOrEmpty(FileToDecrypt))
                {
                    UpdateEncryptButton(false);
                    return false;
                }
                if (passwordInput.Text.Length < 8)
                {
                    UpdateEncryptButton(false);
                    return false;
                }
                UpdateEncryptButton(true);
            }
            EncryptPassword = passwordInput.Text;
            return true;
        }

        private void UpdateEncryptButton(bool value)
        {
            encryptButton.Enabled = value;
        }

        private void Lock()
        {
            EncryptPassword = passwordInput.Text;
            UpdatePasswordInput("");
            UpdateDropFilesLabel(ENCRYPTING);
            encryptButton.Enabled = false;
            quitButton.Enabled = false;
            dropFiles.AllowDrop = false;
            passwordInput.Enabled = false;
        }

        private void UnLock()
        {
            encryptButton.Enabled = true;
            quitButton.Enabled = true;
            dropFiles.AllowDrop = true;
            passwordInput.Enabled = true;
            UpdateDropFilesLabel(DROP_FILE);
            UpdatePasswordInput("");
            CheckEncryptButton();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if ((DirectoryToEncrypt == null && FileToDecrypt == null) || EncryptPassword == null) { return; }
            Lock();
            if (EncryptingDirectory)
            {
                Encrypt encryptDecrypt = new Encrypt(DirectoryToEncrypt, EncryptPassword);
                encryptDecrypt.EncryptProcess();
                if (encryptDecrypt.EncryptedData != null)
                {
                    saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(DirectoryToEncrypt) + ".crypt";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog1.FileName, encryptDecrypt.EncryptedData);
                    }
                }
            }
            else
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && FileToDecrypt != null)
                {
                    Decrypt decrypt = new Decrypt(FileToDecrypt, EncryptPassword, Path.Combine(folderBrowserDialog1.SelectedPath, Path.GetFileNameWithoutExtension(FileToDecrypt)));
                    decrypt.DecryptProcess();
                }
            }
            UnLock();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}