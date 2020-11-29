﻿using System;
using System.Drawing;
using System.Windows.Forms;
using PRACT_OBS.Classes.Helpers;
using PRACT_OBS.Classes;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.Design;
using System.IO;
using System.Collections.Generic;
using PRACT.Rekordbox6.Classes.Helpers;
using PRACT.Rekordbox6.Classes.Data;

namespace PRACT_OBS
{
    public partial class MainForm : Form
    {
        private bool stopExport = false;
        private Thread exportThread;
        private LastTrack lt = null;
        private List<LastTrack> localHistory = new List<LastTrack>();
        
        public MainForm()
        {
            this.Text = Application.ProductName;


            InitializeComponent();
            if (ProgramSettings.ShowDisclaimer)
            {
                DisclaimerForm disclaimer = new DisclaimerForm();
                disclaimer.ShowDialog();
            }
            this.ttipMainform.AutoPopDelay = 5000;
            this.ttipMainform.InitialDelay = 1000;
            this.ttipMainform.ReshowDelay = 500;
            this.ttipMainform.ShowAlways = true;
            this.ttipMainform.SetToolTip(this.btnPush, "Force the export of the current track data");
            this.ttipMainform.SetToolTip(this.btnStart, "Start the track history monitoring");
            this.ttipMainform.SetToolTip(this.btnStop, "Stop the track history monitoring");
            this.ttipMainform.SetToolTip(this.radContinuousExport, "Contiously export data");
            this.ttipMainform.SetToolTip(this.txtArtist, "Current artist");
            this.ttipMainform.SetToolTip(this.txtTitle, "Current track title");
            this.ttipMainform.SetToolTip(this.txtLastExport, "Last time track data has been exported");
            this.ttipMainform.SetToolTip(this.txtHistory, "Time it was added to the Rekordbox track history");
            this.ttipMainform.SetToolTip(this.lnkFilename, "Click to show the file in the Explorer");
            this.ttipMainform.SetToolTip(this.picArtwork, "Current artwork");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ProgramSettings.IsRekordbox6Configured)
            {
                StartExport();
                while (exportThread.IsAlive)
                {
                    Application.DoEvents();
                }
            }
            else
                Messages.WarningMessage("The Rekordbox 6 executable could not be found. Be sure to configure it in the Options menu before proceeding : Tools / Options.");
        }

        private void ContinuousExport()
        {
            while (!IsDisposed && !stopExport)
            {
                // If normal mode (= not edit mode), the file is read from Rekordbox
                // otherwise we export what's written in the text boxes
                if(!radEditMode.Checked)
                    lt = h.GetLastTrack();
                else
                {
                    lt = new LastTrack();
                    lt.Artist = txtArtist.Text;
                    lt.Title = txtTitle.Text;
                }
                localHistory.Add(lt);
                if (lt != null)
                {
                    if(radContinuousExport.Checked)
                        OBSExport.ExportLastTrack(lt);
                    txtArtist.Invoke((Action)delegate
                    {
                        txtArtist.Text = lt.Artist;
                    });
                    txtTitle.Invoke((Action)delegate
                    {
                        txtTitle.Text = lt.Title;
                    });

                    lnkFilename.Invoke((Action)delegate
                    {
                        lnkFilename.Text = lt.FolderPath;
                    });


                    picArtwork.Invoke((Action)delegate
                    {
                        if (!string.IsNullOrEmpty(lt.ImagePath))
                        {
                            picArtwork.Image = Image.FromFile(Rekordbox6Paths.AnalysisDataRootPath + lt.ImagePath);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ProgramSettings.DefaultArtwork))
                                picArtwork.Image = Image.FromFile(ProgramSettings.DefaultArtwork);
                            else
                                picArtwork.Image = null;
                        }
                    }
                    );

                    txtHistory.Invoke((Action)delegate
                    {
                        txtHistory.Text = lt.created_at.ToString();
                    });

                    txtLastExport.Invoke((Action)delegate
                    {
                        txtLastExport.Text = OBSExport.PreviousUpdate.ToString();
                    });
                }
                Thread.Sleep(ProgramSettings.Timer * 1000);
            }

            // Allow the process to be restarted now it's been stopped
            stopExport = false;
        }

        private void OneTimeExport()
        {
            LastTrack lt;
            OBSExport.Clean();
            
            if (!radEditMode.Checked)
                lt = h.GetLastTrack();
            else
            {
                lt = new LastTrack();
                lt.Artist = txtArtist.Text;
                lt.Title = txtTitle.Text;
            }
            OBSExport.ExportLastTrack(lt);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            StopExport();
        }

        private void lnkFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel ll = (LinkLabel)sender;
            Process.Start("explorer.exe", "/select,\"" + ll.Text + "\"");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exportThread != null && exportThread.IsAlive)
            {
                StopExport();
                e.Cancel = true;
                var timer = new System.Timers.Timer();
                timer.AutoReset = false;
                timer.SynchronizingObject = this;
                timer.Interval = 1000;
                timer.Elapsed +=
                  (sender, args) =>
                  {
            // Do a fast check to see if the worker thread is still running.
            if (exportThread.Join(0))
                      {
                // Reissue the form closing event.
                Close();
                      }
                      else
                      {
                // Keep restarting the timer until the worker thread ends.
                timer.Start();
                      }
                  };
                timer.Start();
            }
            if (ProgramSettings.CleanFilesAtShutDown)
                OBSExport.Clean();
        }

        private void StopExport()
        {
            stopExport = true;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
        }

        private void StartExport()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            exportThread = new Thread(new ThreadStart(ContinuousExport));
            exportThread.Start();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm options = new OptionsForm();
            options.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DebugForm debugForm = new DebugForm();
            debugForm.ShowDialog();
        }

        /// <summary>
        /// Keyboard shortcuts management
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.F5:
                    if (btnStart.Enabled)
                    {
                        StartExport();
                        return true;
                    }
                    else
                        break;
                case Keys.F8:
                    if (btnStop.Enabled)
                    {
                        StopExport();
                        return true;
                    }
                    else
                        break;
                case Keys.F9:
                    if (!radContinuousExport.Checked)
                    {
                        OneTimeExport();
                        return true;
                    }
                    else
                        break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private string key
        {
            get
            {
                return ProgramSettings.Key;
            }
        }
        private MasterDB masterDB
        {
            get
            {
                if(_MasterDB == null)
                    _MasterDB = new MasterDB(Rekordbox6Paths.DbPath, key);
                return _MasterDB;
            }
        }

        private LastTrackHelper h
        {
            get
            {
                if(_helper==null)
                    _helper = new LastTrackHelper(masterDB);
                return _helper;
            }
        }

        private MasterDB _MasterDB = null;
        private LastTrackHelper _helper = null;

        private void chkContinuousExport_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            OneTimeExport();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OBSExport.Clean();
        }

        private void chkEdit_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radEditMode_CheckedChanged(object sender, EventArgs e)
        {
            ToggleEditMode();
        }

        private void radContinuousExport_CheckedChanged(object sender, EventArgs e)
        {
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            txtArtist.ReadOnly = !radEditMode.Checked;
            txtTitle.ReadOnly = !radEditMode.Checked;
            if (radEditMode.Checked)
            {
                if (!string.IsNullOrEmpty(ProgramSettings.DefaultArtwork))
                    picArtwork.Image = Image.FromFile(ProgramSettings.DefaultArtwork);
                else
                    picArtwork.Image = null;
            }
            btnPush.Enabled = radEditMode.Checked;
        }

    }
}
