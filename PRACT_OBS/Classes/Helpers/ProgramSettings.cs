using Microsoft.Extensions.Configuration;
using PRACT.Rekordbox6.Classes.Helpers;
using PRACT_OBS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PRACT_OBS.Classes.Helpers
{
    public class ProgramSettings  : PRACT.Rekordbox6.Helpers.ProgramSettings
    {

        public static string OutputFolder
        {
            get
            {
                // If undefined, the OutputFolder will be the MyDocuments directory
                if (settings.OutputFolder == string.Empty)
                    return SystemPaths.MyDocumentsFolder;
                else
                    return settings.OutputFolder.Trim();
            }
            set
            {
                settings.OutputFolder = value;
                settings.Save();
            }
        }



        public static int Timer
        {
            get
            {
                return
                      settings.Timer;
            }
            set
            {
                settings.Timer = value;
                settings.Save();
            }
        }
        public static bool ShowDisclaimer
        {
            get
            {
                return
                      settings.ShowDisclaimerOnStartup;
            }
            set
            {
                settings.ShowDisclaimerOnStartup = value;
                settings.Save();
            }
        }

        public static int OnScreenDuration 
        {
            get
            {
                return
                      settings.OnScreenDuration;
            }
            set
            {
                settings.OnScreenDuration = value;
                settings.Save();
            }
        }

        public static bool PassphraseToMine
        {
            get
            {
                return settings.PassphraseToMine;
            }
            set
            {
                settings.PassphraseToMine = value;
                settings.Key = PASSPHRASE_TO_MINE;
                settings.Save();
            }
        }

        public static string ArtistTitleSeparator
        {
            get
            {
                return settings.ArtistTitleSeparator;
            }
            set
            {
                settings.ArtistTitleSeparator = value;
                settings.Save();
            }
        }
        public static string DefaultArtwork
        {
            get
            {
                return settings.DefaultArtwork;
            }
            set
            {
                settings.DefaultArtwork = value;
                settings.Save();
            }
        }

        public static string CustomExportFormat
        {
            get
            {
                return settings.CustomExportFormat;
            }
            set
            {
                settings.CustomExportFormat = value;
                if (settings.CustomExportEnabled && string.IsNullOrWhiteSpace(value))
                    settings.CustomExportEnabled = false;
                settings.Save();
            }
        }

        public static bool CustomExportEnabled
        {
            get
            {
                return settings.CustomExportEnabled;
            }
            set
            {
                settings.CustomExportEnabled = value;
                settings.Save();
            }
        }

        public static bool JSONExportEnabled
        {
            get
            {
                return settings.JSONExportEnabled;
            }
            set
            {
                settings.JSONExportEnabled = value;
                settings.Save();
            }
        }
        public static bool CleanFilesAtStartup
        {
            get
            {
                return settings.CleanFilesAtStartup;
            }
            set
            {
                settings.CleanFilesAtStartup = value;
                settings.Save();
            }
        }
        public static bool CleanFilesAtShutDown
        {
            get
            {
                return settings.CleanFilesAtShutDown;
            }
            set
            {
                settings.CleanFilesAtShutDown = value;
                settings.Save();
            }
        }
        public static bool DoNotExportPastTracks
        {
            get
            {
                return settings.DoNotExportPastTracks;
            }
            set
            {
                settings.DoNotExportPastTracks = value;
                settings.Save();
            }
        }

        public static string RekordboxBinariesFolder
        {
            get
            {
                return Path.GetDirectoryName(Rekordbox6Executable);
            }
        }

        public static string Rekordbox6Executable
        {
            get
            {
                return settings.Rekordbox6Exe;
            }
            set
            {
                settings.Rekordbox6Exe = value;
                Rekordbox6Paths.RekordboxBinariesFolder = value;
                settings.Save();
            }
        }
        public static string Key
        {
            get
            {
                // If not set, the program will try to mine for the database passphrase
                // otherwise it will use the key stored in the config file
                if (settings.PassphraseToMine)
                {
                    if (string.IsNullOrWhiteSpace(EncryptionKey))
                    {
                        MineKey();
                    }
                    return EncryptionKey;
                }
                else
                    return settings.Key;

            }
            set
            {
                settings.Key = value;
                settings.Save();
            }
        }

        private static Settings settings
        {
            get
            {
                return Settings.Default;
            }
        }
    }
}
