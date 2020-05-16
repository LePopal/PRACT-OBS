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
            chkMine.Checked = ProgramSettings.PassphraseToMine;
            if (ProgramSettings.PassphraseToMine)
            {
                txtKey.Text = ProgramSettings.PASSPHRASE_TO_MINE;
                txtKey.Enabled = false;
            }
            else
            {
                txtKey.Text = ProgramSettings.Key;
            }
            txtOutputFolder.Text = ProgramSettings.OutputFolder;
            txtOnScreenDuration.Text = ProgramSettings.OnScreenDuration.ToString();
            txtPooling.Text = ProgramSettings.Timer.ToString();
            txtArtistTitleSeparator.Text = ProgramSettings.ArtistTitleSeparator;
            txtDefaultArtwork.Text = ProgramSettings.DefaultArtwork;
            txtCustomFormat.Text = ProgramSettings.CustomExportFormat;
            chkCustomExport.Checked = ProgramSettings.CustomExportEnabled;
            txtCustomFormat.Enabled = chkCustomExport.Checked;
            chkCleanStartup.Checked = ProgramSettings.CleanFilesAtStartup;
            chkCleanExit.Checked = ProgramSettings.CleanFilesAtShutDown;
            chkDoNot.Checked = ProgramSettings.DoNotExportPastTracks;
            this.ttipOptions.AutoPopDelay = 5000;
            this.ttipOptions.InitialDelay = 1000;
            this.ttipOptions.ReshowDelay = 500;
            this.ttipOptions.ShowAlways = true;
            this.ttipOptions.SetToolTip(this.txtKey, "Provide an explicit Rekordbox database encryption key");
            this.ttipOptions.SetToolTip(this.chkMine, "Checked: tries to automatically find the encryption key");
            this.ttipOptions.SetToolTip(this.txtOutputFolder, "Folder where to export the files to OBS");
            this.ttipOptions.SetToolTip(this.txtOnScreenDuration, "How long in seconds the current track info should be displayed in OBS. 0 means forever");
            this.ttipOptions.SetToolTip(this.txtArtistTitleSeparator, "A separator for the ArtistTitle.txt file");
            this.ttipOptions.SetToolTip(this.txtPooling, "How often should the program query Rekordbox for new played tracks. New played tracks don't appear before 1 minute in the Rekordbox history. Default is 10 seconds. A too short value my degrade Rekordbox performance and corrupt data.");
            this.ttipOptions.SetToolTip(this.txtDefaultArtwork, "Path to a picture to display when none wwas found or none exists");
            this.ttipOptions.SetToolTip(this.txtCustomFormat, "Specify a custom format for the custom format file. Values include : %ID% %FOLDERPATH% %ARTIST% %TITLE% %IMAGEPATH% %BPM% %RATING% %YEAR% %LENGTH% %COLORID% %COLORNAME% %COMMENT% %ALBUM% %LABEL% %GENRE% %KEY%  %REMIXER%  %MESSAGE%");
            this.ttipOptions.SetToolTip(this.chkCustomExport, "Enable or disable the custom format file");
            this.ttipOptions.SetToolTip(this.chkCleanStartup, "Automatically delete the exported files on startup");
            this.ttipOptions.SetToolTip(this.chkCleanStartup, "Automatically delete the exported files on shutdown");
            this.ttipOptions.SetToolTip(this.chkDoNot, "Don't load previously played track when the program starts");
            this.ttipOptions.SetToolTip(this.chkJSON, "Enable or disable the JSON export");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ProgramSettings.PassphraseToMine = chkMine.Checked;
            if (chkMine.Checked)
                ProgramSettings.Key = ProgramSettings.PASSPHRASE_TO_MINE;
            else
                ProgramSettings.Key = txtKey.Text;

            if (!CheckKey())
                Messages.WarningMessage("This key doesn't seem to be valid!");

            ProgramSettings.OutputFolder = txtOutputFolder.Text.Trim();

            if (!CheckOutputFolder() &&
                    (Messages.YesNoMessage("The Output Folder does not exist, would you like to create it ?") ==
                    DialogResult.Yes))
            {
                try
                {
                    Directory.CreateDirectory(txtOutputFolder.Text);
                    ProgramSettings.OutputFolder = txtOutputFolder.Text.Trim();
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

            ProgramSettings.ArtistTitleSeparator = txtArtistTitleSeparator.Text;
            ProgramSettings.JSONExportEnabled = chkJSON.Checked;
            ProgramSettings.DoNotExportPastTracks = chkDoNot.Checked;
            ProgramSettings.CleanFilesAtShutDown = chkCleanExit.Checked;
            ProgramSettings.CleanFilesAtStartup = chkCleanStartup.Checked;

            if (!string.IsNullOrWhiteSpace(txtDefaultArtwork.Text)
                && !File.Exists(txtDefaultArtwork.Text))
            {
                Messages.ErrorMessage(string.Format("Impossible to set default artwork to {0}. The file doesn't exist !", txtDefaultArtwork.Text));
            }
            else
                ProgramSettings.DefaultArtwork = txtDefaultArtwork.Text.Trim();

            if(!CheckCustomExport())
            {
                Messages.ErrorMessage("Custom Export Format malformed. Please correct it.");
                return;
            }
            else
            {
                ProgramSettings.CustomExportEnabled = chkCustomExport.Checked;
                ProgramSettings.CustomExportFormat = txtCustomFormat.Text;
            }

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

        private bool CheckCustomExport()
        {
            return true; // Todo
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

        private void chkMine_CheckedChanged(object sender, EventArgs e)
        {
            txtKey.Enabled = !chkMine.Checked;
        }

        private void btnDefaultArtwork_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtDefaultArtwork.Text))
                fileDefaultArtwork.FileName = txtDefaultArtwork.Text;
            else
                fileDefaultArtwork.FileName = string.Empty;
            fileDefaultArtwork.ShowDialog();
            txtDefaultArtwork.Text = fileDefaultArtwork.FileName;

        }

        private void btnClearDefaultArtwork_Click(object sender, EventArgs e)
        {
            txtDefaultArtwork.Text = string.Empty;

        }
        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            outputFolderDialog.SelectedPath = txtOutputFolder.Text;
            outputFolderDialog.ShowDialog();
            txtOutputFolder.Text = outputFolderDialog.SelectedPath;
        }

        private void chkCustomExport_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomFormat.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
