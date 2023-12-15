namespace Encrypto
{
    partial class General
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            encryptButton = new Button();
            groupBox2 = new GroupBox();
            dropFiles = new Label();
            groupBox1 = new GroupBox();
            passwordInput = new TextBox();
            quitButton = new Button();
            saveFileDialog1 = new SaveFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(encryptButton);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(quitButton);
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = Color.WhiteSmoke;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(306, 253);
            panel1.TabIndex = 0;
            // 
            // encryptButton
            // 
            encryptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            encryptButton.Enabled = false;
            encryptButton.FlatStyle = FlatStyle.Flat;
            encryptButton.Location = new Point(13, 209);
            encryptButton.Name = "encryptButton";
            encryptButton.Size = new Size(102, 31);
            encryptButton.TabIndex = 4;
            encryptButton.Text = "Encrypt";
            encryptButton.UseVisualStyleBackColor = true;
            encryptButton.Click += encryptButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(dropFiles);
            groupBox2.Location = new Point(13, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(280, 127);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            // 
            // dropFiles
            // 
            dropFiles.AllowDrop = true;
            dropFiles.Dock = DockStyle.Fill;
            dropFiles.ForeColor = Color.DarkGray;
            dropFiles.Location = new Point(3, 19);
            dropFiles.Name = "dropFiles";
            dropFiles.Size = new Size(274, 105);
            dropFiles.TabIndex = 1;
            dropFiles.Text = "Drop directory or crypted file here";
            dropFiles.TextAlign = ContentAlignment.MiddleCenter;
            dropFiles.DragDrop += dropFiles_DragDrop;
            dropFiles.DragEnter += dropFiles_DragEnter;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(passwordInput);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox1.Location = new Point(13, 139);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(10, 3, 10, 3);
            groupBox1.Size = new Size(280, 53);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // passwordInput
            // 
            passwordInput.BackColor = Color.FromArgb(68, 68, 68);
            passwordInput.BorderStyle = BorderStyle.FixedSingle;
            passwordInput.Dock = DockStyle.Fill;
            passwordInput.ForeColor = Color.WhiteSmoke;
            passwordInput.Location = new Point(10, 19);
            passwordInput.Name = "passwordInput";
            passwordInput.Size = new Size(260, 23);
            passwordInput.TabIndex = 0;
            passwordInput.TextAlign = HorizontalAlignment.Center;
            passwordInput.TextChanged += passwordInput_TextChanged;
            // 
            // quitButton
            // 
            quitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            quitButton.FlatStyle = FlatStyle.Flat;
            quitButton.Location = new Point(191, 209);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(102, 31);
            quitButton.TabIndex = 0;
            quitButton.Text = "Quitter";
            quitButton.UseVisualStyleBackColor = true;
            quitButton.Click += quitButton_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "crypt";
            saveFileDialog1.Filter = "Fichier Crypt|*.crypt";
            // 
            // General
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(306, 253);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "General";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Encrypto";
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button quitButton;
        private GroupBox groupBox2;
        private Label dropFiles;
        private GroupBox groupBox1;
        private Button encryptButton;
        private TextBox passwordInput;
        private SaveFileDialog saveFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}