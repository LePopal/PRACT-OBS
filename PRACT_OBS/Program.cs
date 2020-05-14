using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using PRACT_OBS.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRACT_OBS
{
    static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Rekordbox6Paths.IsRekordbox6Installed())
            {
                Configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
                    .AddCommandLine(args)
                    .Build();

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
