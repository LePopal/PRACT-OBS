using Microsoft.Extensions.Configuration;
using PRACT.Rekordbox6.Classes.Helpers;
using PRACT_OBS.Classes;
using PRACT_OBS.Classes.Helpers;
using PRACT_OBS.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace PRACT_OBS
{
    static class Program
    {
        public static IConfiguration Configuration;
        public static DateTime StartupTime = DateTime.Now;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Rekordbox6Paths.RekordboxBinariesFolder = ProgramSettings.RekordboxBinariesFolder;
            bool startUpPossible = string.IsNullOrWhiteSpace(ProgramSettings.Rekordbox6Executable)
                || !File.Exists(ProgramSettings.Rekordbox6Executable);
            if (startUpPossible)
                Messages.WarningMessage("The Rekordbox 6 executable could not be found. Be sure to configure it in the Options menu before proceeding : Tools / Options.");

            // We can start the application even if the exe could not be found because we have to configure it anyway
            if (startUpPossible || Rekordbox6Paths.IsRekordbox6Installed())
            {
                Configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
                    .AddCommandLine(args)
                    .Build();

                // Cleaning output files
                if(ProgramSettings.CleanFilesAtStartup)
                    OBSExport.Clean();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                Messages.ErrorMessage("Rekordbox 6 is required to run this program. Please install this software and try again.");
            }
        }
    }
}
