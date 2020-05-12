using Microsoft.Extensions.Configuration;
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
    public static class ProgramSettings
    {
        // Useful constants
        public const string PASSPHRASE_TO_MINE = "PASSPHRASE_TO_MINE";
        
        public static string OutputFolder
        {
            get
            {
                // If undefined, the OutputFolder will be the MyDocuments directory
                if (settings.OutputFolder == string.Empty)
                    return Paths.MyDocumentsFolder;
                else
                    return settings.OutputFolder.Trim();
            }
            set
            {
                settings.OutputFolder = value;
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
                    if (EncryptionKey != string.Empty)
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
        private static void MineKey()
        {
            // Load App.asar and clean/stripe it to better find what we're looking for
            string appAsarContent = Regex.Replace(File.ReadAllText(Paths.AppAsarFilePath), @"[^\u0000-\u007F]+", string.Empty)
                        .Replace(" ", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty); ;

            string pattern = "pass:\"(.*?)\"}";

            MatchCollection matches = Regex.Matches(appAsarContent, pattern);
            List<string> l = new List<string>();
            foreach (Match match in matches)
            {
                l.Add(match.Groups[1].Value);
            }
            if (l.Count > 0)
                _EncryptionPassPhrase = l[0];
            else
                throw new System.Data.DataException("Passphrase not found");
            BlowFish blowFish = new BlowFish(Encoding.ASCII.GetBytes(_EncryptionPassPhrase));
            byte[] data = Convert.FromBase64String(Paths.Rb6Options.options.Dp);
            //string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            byte[] out1 = blowFish.Decrypt_ECB(data);
            EncryptionKey = System.Text.ASCIIEncoding.ASCII.GetString(out1);
        }

        private static string _EncryptionPassPhrase { get; set; }
        private static string EncryptionKey { get; set; }
        private static Settings settings
        {
            get
            {
                return Settings.Default;
            }
        }
    }
}
