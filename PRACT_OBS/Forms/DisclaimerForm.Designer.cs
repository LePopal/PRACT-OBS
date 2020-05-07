namespace PRACT_OBS
{
    partial class DisclaimerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisclaimerForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkDisclaimer = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 237);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1075, 430);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(459, 673);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 34);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkDisclaimer
            // 
            this.chkDisclaimer.AutoSize = true;
            this.chkDisclaimer.Location = new System.Drawing.Point(796, 677);
            this.chkDisclaimer.Name = "chkDisclaimer";
            this.chkDisclaimer.Size = new System.Drawing.Size(294, 29);
            this.chkDisclaimer.TabIndex = 2;
            this.chkDisclaimer.Text = "Don\'t show this disclaimer again";
            this.chkDisclaimer.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(98, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(843, 48);
            this.label1.TabIndex = 3;
            this.label1.Text = "BACKUP YOUR REKORDBOX LIBRARY NOW !!!";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 60);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1077, 171);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // DisclaimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 719);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkDisclaimer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DisclaimerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup your Rekordbox Library NOW !";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkDisclaimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}