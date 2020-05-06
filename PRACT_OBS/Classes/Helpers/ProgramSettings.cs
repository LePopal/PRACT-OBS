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
                if (Settings.Default.OutputFolder == string.Empty)
                    return Paths.MyDocumentsFolder;
                else
                    return Settings.Default.OutputFolder;
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
                // If not set, the program will try to mine for the database passphrase
                // otherwise it will use the key stored in the config file
                if (Settings.Default.PassphraseToMine)
                {
                    if (EncryptionKey != string.Empty)
                    {
                        MineKey();
                    }
                    return EncryptionKey;
                }
                else
                    return Settings.Default.Key;

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

        public static bool PassphraseToMine
        {
            get
            {
                return Settings.Default.PassphraseToMine;
            }
            set
            {
                Settings.Default.PassphraseToMine = value;
                Settings.Default.Key = PASSPHRASE_TO_MINE;
                Settings.Default.Save();
            }
        }

        public static string ArtistTitleSeparator
        {
            get
            {
                return Settings.Default.ArtistTitleSeparator;
            }
            set
            {
                Settings.Default.ArtistTitleSeparator = value;
                Settings.Default.Save();
            }
        }
        public static string DefaultArtwork
        {
            get
            {
                return Settings.Default.DefaultArtwork;
            }
            set
            {
                Settings.Default.DefaultArtwork = value;
                Settings.Default.Save();
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
    }
}
