using PRACT_OBS.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PRACT_OBS
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            txtKey.Text = ProgramSettings.Key;
            txtOutputFolder.Text = ProgramSettings.OutputFolder;
            txtOnScreenDuration.Text = ProgramSettings.OnScreenDuration.ToString();
            txtPooling.Text = ProgramSettings.Timer.ToString();
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            outputFolderDialog.SelectedPath = txtOutputFolder.Text;
            outputFolderDialog.ShowDialog();
            txtOutputFolder.Text = outputFolderDialog.SelectedPath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ProgramSettings.Key = txtKey.Text;
            if (!CheckKey())
                Messages.WarningMessage("This key doesn't seem to be valid!");

            ProgramSettings.OutputFolder = txtOutputFolder.Text;

            if (!CheckOutputFolder() &&
                    (Messages.YesNoMessage("The Output Folder does not exist, would you like to create it ?") ==
                    DialogResult.Yes))
            {
                try
                {
                    Directory.CreateDirectory(txtOutputFolder.Text);
                    ProgramSettings.OutputFolder = txtOutputFolder.Text;
                }
                catch
                {
                    Messages.ErrorMessage("Error while creating directory");
                }
            }
            if (!CheckOnScreenDuration())
            {
                Messages.ErrorMessage("Invalid On Screen Duration");
            }
            else
                ProgramSettings.OnScreenDuration = int.Parse(txtOnScreenDuration.Text);

            if (!CheckPoolingPeriod())
            {
                Messages.ErrorMessage("Invalid Pooling Period. The minimum value is 1");
            }
            else
                ProgramSettings.Timer = int.Parse(txtPooling.Text);
            this.Close();
        }
            
        
        private bool CheckOutputFolder()
        {
            return Directory.Exists(txtOutputFolder.Text);
        }

        private bool CheckKey()
        {
            return !string.IsNullOrWhiteSpace(txtKey.Text);
        }

        private bool CheckPoolingPeriod()
        {
            return CheckInteger(txtPooling.Text) && int.Parse(txtPooling.Text) > 0;
        }

        private bool CheckOnScreenDuration()
        {
            return CheckInteger(txtPooling.Text);
        }

        private bool CheckInteger(string i)
        {
            return (int.TryParse(i, out int newValue)) && (newValue >= 0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OptionsForm_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
