using System;
using System.Drawing;
using System.Windows.Forms;
using PRACT_OBS.Classes.Data;
using PRACT_OBS.Classes.Helpers;
using PRACT_OBS.Classes;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.Design;
using System.IO;

namespace PRACT_OBS
{
    public partial class MainForm : Form
    {
        private bool stopExport = false;
        private Thread exportThread;
        public MainForm()
        {
            this.Text = Assembly.Title;
            InitializeComponent();
            if (ProgramSettings.ShowDisclaimer)
            {
                DisclaimerForm disclaimer = new DisclaimerForm();
                disclaimer.ShowDialog();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {


            ProgramSettings.Key = ProgramSettings.Key;
            StartExport();
            while (exportThread.IsAlive)
            {
                Application.DoEvents();
            }

        }

        private void ContinuousExport()
        {
            string key = ProgramSettings.Key;
            MasterDB masterDB = null;
            try
            {
                masterDB = new MasterDB(Paths.DbPath, key);
            }
            catch (FileNotFoundException e)
            {
                Messages.ErrorMessage(e.Message);
                Messages.ErrorMessage("Exiting program...");
                this.Close();
            }
            Helpers h = new Helpers(masterDB);

            while (!IsDisposed && !stopExport)
            {
                LastTrack lt = h.GetLastTrack();
                if (lt != null)
                {
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
                            picArtwork.Image = Image.FromFile(Paths.AnalysisDataRootPath + lt.ImagePath);
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
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }
    }
}
