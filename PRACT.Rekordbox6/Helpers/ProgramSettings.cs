using PRACT.Rekordbox6.Classes;
using PRACT.Rekordbox6.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PRACT.Rekordbox6.Helpers
{
    public class ProgramSettings
    {
        // Useful constants
        public const string PASSPHRASE_TO_MINE = "PASSPHRASE_TO_MINE";


        protected static void MineKey()
        {
            // Load App.asar and clean/stripe it to better find what we're looking for
            string appAsarContent = Regex.Replace(File.ReadAllText(Rekordbox6Paths.AppAsarFilePath), @"[^\u0000-\u007F]+", string.Empty)
                        .Replace(" ", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty); ;

            string pattern = "pass:\"(.*?)\",?}";

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
            byte[] data = Convert.FromBase64String(Rekordbox6Paths.Rb6Options.options.Dp);
            //string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            byte[] out1 = blowFish.Decrypt_ECB(data);
            EncryptionKey = System.Text.ASCIIEncoding.ASCII.GetString(out1);
        }


        private static string _EncryptionPassPhrase { get; set; }
        protected static string EncryptionKey { get; set; }
    }
}
