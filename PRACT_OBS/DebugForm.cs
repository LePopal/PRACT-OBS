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
                .AppendLine(ProgramSettings.Key)
                .Append("OutputFolder=")
                .AppendLine(ProgramSettings.OutputFolder)
                .Append("PoolingPeriod=")
                .AppendLine(ProgramSettings.Timer.ToString())
                .Append("OnScreenDuration=")
                .AppendLine(ProgramSettings.OnScreenDuration.ToString())
                .Append("AnalysisRootPath=")
                .AppendLine(Paths.AnalysisDataRootPath)
                .Append("MasterDB=")
                .AppendLine(Paths.DbPath)
                .Append("SettingsRootPath=")
                .AppendLine(Paths.SettingsRootPath)
                .Append("RekordboxRootPath=")
                .AppendLine(Paths.RekordboxRoot())
                .Append("RekordboxBinariesFolder=")
                .AppendLine(Paths.RekordboxBinariesFolder)
                .Append("App.asar=")
                .AppendLine(Paths.AppAsarFilePath)
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
