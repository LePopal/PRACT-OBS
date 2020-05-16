using PRACT.Rekordbox6.Classes.Helpers;
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
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            sb.Append("Key=")
                .AppendLine(ProgramSettings.PassphraseToMine ? ProgramSettings.PASSPHRASE_TO_MINE : ProgramSettings.Key)
                .Append("OutputFolder=")
                .AppendLine(ProgramSettings.OutputFolder)
                .Append("PoolingPeriod=")
                .AppendLine(ProgramSettings.Timer.ToString())
                .Append("OnScreenDuration=")
                .AppendLine(ProgramSettings.OnScreenDuration.ToString())
                .Append("AnalysisRootPath=")
                .AppendLine(Rekordbox6Paths.AnalysisDataRootPath)
                .Append("MasterDB=")
                .AppendLine(Rekordbox6Paths.DbPath)
                .Append("SettingsRootPath=")
                .AppendLine(Rekordbox6Paths.SettingsRootPath)
                .Append("RekordboxRootPath=")
                .AppendLine(Rekordbox6Paths.RekordboxRoot)
                .Append("RekordboxBinariesFolder=")
                .AppendLine(Rekordbox6Paths.RekordboxBinariesFolder)
                .Append("App.asar=")
                .AppendLine(Rekordbox6Paths.AppAsarFilePath)
                .Append("ArtistTitleSeparator=")
                .AppendLine(ProgramSettings.ArtistTitleSeparator)
                .Append("DefaultArtwork=")
                .AppendLine(ProgramSettings.DefaultArtwork)
                ;
            txtDebugInfo.Text = sb.ToString();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
