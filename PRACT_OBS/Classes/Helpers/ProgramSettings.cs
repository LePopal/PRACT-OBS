using Microsoft.Extensions.Configuration;
using PRACT_OBS.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.Json;

namespace PRACT_OBS.Classes.Helpers
{
    public static class ProgramSettings
    {

        
        public static string OutputFolder
        {
            get
            {
                return Settings.Default.OutputFolder;//Program.Configuration.GetValue<string>("OutputFolder");
            }
            set
            {
                Settings.Default.OutputFolder = value;
                Settings.Default.Save();
            }
        }

        public static string Key
        {
            get
            {
                return  Settings.Default.Key;
            }
            set
            {
                Settings.Default.Key = value;
                Settings.Default.Save();
            }
        }

        public static int Timer
        {
            get
            {
                return
                      Settings.Default.Timer;
            }
            set
            {
                Settings.Default.Timer = value;
                Settings.Default.Save();
            }
        }
        public static bool ShowDisclaimer
        {
            get
            {
                return
                      Settings.Default.ShowDisclaimerOnStartup;
            }
            set
            {
                Settings.Default.ShowDisclaimerOnStartup = value;
                Settings.Default.Save();
            }
        }

        public static int OnScreenDuration 
        {
            get
            {
                return
                      Settings.Default.OnScreenDuration;
            }
            set
            {
                Settings.Default.OnScreenDuration = value;
                Settings.Default.Save();
            }
        }

    }
}
