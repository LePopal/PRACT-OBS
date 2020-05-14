namespace PRACT_OBS
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.outputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOnScreenDuration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPooling = new System.Windows.Forms.TextBox();
            this.chkMine = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtArtistTitleSeparator = new System.Windows.Forms.TextBox();
            this.btnDefaultArtwork = new System.Windows.Forms.Button();
            this.txtDefaultArtwork = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fileDefaultArtwork = new System.Windows.Forms.OpenFileDialog();
            this.btnClearDefaultArtwork = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCustomFormat = new System.Windows.Forms.TextBox();
            this.chkCustomExport = new System.Windows.Forms.CheckBox();
            this.chkJSON = new System.Windows.Forms.CheckBox();
            this.chkDoNot = new System.Windows.Forms.CheckBox();
            this.chkCleanStartup = new System.Windows.Forms.CheckBox();
            this.chkCleanExit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(218, 13);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(816, 31);
            this.txtKey.TabIndex = 1;
            this.txtKey.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(13, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output Folder";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(218, 50);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(928, 31);
            this.txtOutputFolder.TabIndex = 2;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(1152, 50);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(55, 34);
            this.btnOutputFolder.TabIndex = 3;
            this.btnOutputFolder.Text = "...";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(424, 474);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 34);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(659, 474);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(13, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "On Screen Duration";
            // 
            // txtOnScreenDuration
            // 
            this.txtOnScreenDuration.Location = new System.Drawing.Point(218, 90);
            this.txtOnScreenDuration.Name = "txtOnScreenDuration";
            this.txtOnScreenDuration.Size = new System.Drawing.Size(990, 31);
            this.txtOnScreenDuration.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(12, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pooling Period";
            // 
            // txtPooling
            // 
            this.txtPooling.Location = new System.Drawing.Point(217, 127);
            this.txtPooling.Name = "txtPooling";
            this.txtPooling.Size = new System.Drawing.Size(990, 31);
            this.txtPooling.TabIndex = 1;
            // 
            // chkMine
            // 
            this.chkMine.AutoSize = true;
            this.chkMine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkMine.Location = new System.Drawing.Point(1040, 13);
            this.chkMine.Name = "chkMine";
            this.chkMine.Size = new System.Drawing.Size(179, 29);
            this.chkMine.TabIndex = 5;
            this.chkMine.Text = "Mine for the key";
            this.chkMine.UseVisualStyleBackColor = true;
            this.chkMine.CheckedChanged += new System.EventHandler(this.chkMine_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(13, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Artist/Title Separator";
            // 
            // txtArtistTitleSeparator
            // 
            this.txtArtistTitleSeparator.Location = new System.Drawing.Point(218, 164);
            this.txtArtistTitleSeparator.Name = "txtArtistTitleSeparator";
            this.txtArtistTitleSeparator.Size = new System.Drawing.Size(989, 31);
            this.txtArtistTitleSeparator.TabIndex = 1;
            // 
            // btnDefaultArtwork
            // 
            this.btnDefaultArtwork.Location = new System.Drawing.Point(1152, 201);
            this.btnDefaultArtwork.Name = "btnDefaultArtwork";
            this.btnDefaultArtwork.Size = new System.Drawing.Size(55, 34);
            this.btnDefaultArtwork.TabIndex = 3;
            this.btnDefaultArtwork.Text = "...";
            this.btnDefaultArtwork.UseVisualStyleBackColor = true;
            this.btnDefaultArtwork.Click += new System.EventHandler(this.btnDefaultArtwork_Click);
            // 
            // txtDefaultArtwork
            // 
            this.txtDefaultArtwork.Location = new System.Drawing.Point(218, 201);
            this.txtDefaultArtwork.Name = "txtDefaultArtwork";
            this.txtDefaultArtwork.Size = new System.Drawing.Size(867, 31);
            this.txtDefaultArtwork.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(13, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Default Artwork";
            // 
            // fileDefaultArtwork
            // 
            this.fileDefaultArtwork.Filter = "Pictures (*.png, *.jpg)|*.png;*.jpg";
            // 
            // btnClearDefaultArtwork
            // 
            this.btnClearDefaultArtwork.Location = new System.Drawing.Point(1091, 201);
            this.btnClearDefaultArtwork.Name = "btnClearDefaultArtwork";
            this.btnClearDefaultArtwork.Size = new System.Drawing.Size(55, 34);
            this.btnClearDefaultArtwork.TabIndex = 3;
            this.btnClearDefaultArtwork.Text = "X";
            this.btnClearDefaultArtwork.UseVisualStyleBackColor = true;
            this.btnClearDefaultArtwork.Click += new System.EventHandler(this.btnClearDefaultArtwork_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(13, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Custom Format";
            // 
            // txtCustomFormat
            // 
            this.txtCustomFormat.Location = new System.Drawing.Point(218, 241);
            this.txtCustomFormat.Multiline = true;
            this.txtCustomFormat.Name = "txtCustomFormat";
            this.txtCustomFormat.Size = new System.Drawing.Size(961, 89);
            this.txtCustomFormat.TabIndex = 1;
            // 
            // chkCustomExport
            // 
            this.chkCustomExport.AutoSize = true;
            this.chkCustomExport.Location = new System.Drawing.Point(1185, 245);
            this.chkCustomExport.Name = "chkCustomExport";
            this.chkCustomExport.Size = new System.Drawing.Size(22, 21);
            this.chkCustomExport.TabIndex = 6;
            this.chkCustomExport.UseVisualStyleBackColor = true;
            this.chkCustomExport.CheckedChanged += new System.EventHandler(this.chkCustomExport_CheckedChanged);
            // 
            // chkJSON
            // 
            this.chkJSON.AutoSize = true;
            this.chkJSON.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkJSON.Location = new System.Drawing.Point(321, 371);
            this.chkJSON.Name = "chkJSON";
            this.chkJSON.Size = new System.Drawing.Size(146, 29);
            this.chkJSON.TabIndex = 7;
            this.chkJSON.Text = "JSON Export";
            this.chkJSON.UseVisualStyleBackColor = true;
            // 
            // chkDoNot
            // 
            this.chkDoNot.AutoSize = true;
            this.chkDoNot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkDoNot.Location = new System.Drawing.Point(321, 336);
            this.chkDoNot.Name = "chkDoNot";
            this.chkDoNot.Size = new System.Drawing.Size(438, 29);
            this.chkDoNot.TabIndex = 8;
            this.chkDoNot.Text = "Do not load previously played tracks at startup";
            this.chkDoNot.UseVisualStyleBackColor = true;
            // 
            // chkCleanStartup
            // 
            this.chkCleanStartup.AutoSize = true;
            this.chkCleanStartup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkCleanStartup.Location = new System.Drawing.Point(13, 337);
            this.chkCleanStartup.Name = "chkCleanStartup";
            this.chkCleanStartup.Size = new System.Drawing.Size(216, 29);
            this.chkCleanStartup.TabIndex = 9;
            this.chkCleanStartup.Text = "Clean files at Startup";
            this.chkCleanStartup.UseVisualStyleBackColor = true;
            // 
            // chkCleanExit
            // 
            this.chkCleanExit.AutoSize = true;
            this.chkCleanExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkCleanExit.Location = new System.Drawing.Point(13, 372);
            this.chkCleanExit.Name = "chkCleanExit";
            this.chkCleanExit.Size = new System.Drawing.Size(189, 29);
            this.chkCleanExit.TabIndex = 10;
            this.chkCleanExit.Text = "Clean files on Exit";
            this.chkCleanExit.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1220, 520);
            this.Controls.Add(this.chkCleanExit);
            this.Controls.Add(this.chkCleanStartup);
            this.Controls.Add(this.chkDoNot);
            this.Controls.Add(this.chkJSON);
            this.Controls.Add(this.chkCustomExport);
            this.Controls.Add(this.txtCustomFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClearDefaultArtwork);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDefaultArtwork);
            this.Controls.Add(this.btnDefaultArtwork);
            this.Controls.Add(this.txtArtistTitleSeparator);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkMine);
            this.Controls.Add(this.txtPooling);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOnScreenDuration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OptionsForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.FolderBrowserDialog outputFolderDialog;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOnScreenDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPooling;
        private System.Windows.Forms.CheckBox chkMine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtArtistTitleSeparator;
        private System.Windows.Forms.Button btnDefaultArtwork;
        private System.Windows.Forms.TextBox txtDefaultArtwork;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog fileDefaultArtwork;
        private System.Windows.Forms.Button btnClearDefaultArtwork;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCustomFormat;
        private System.Windows.Forms.CheckBox chkCustomExport;
        private System.Windows.Forms.CheckBox chkJSON;
        private System.Windows.Forms.CheckBox chkDoNot;
        private System.Windows.Forms.CheckBox chkCleanStartup;
        private System.Windows.Forms.CheckBox chkCleanExit;
    }
}