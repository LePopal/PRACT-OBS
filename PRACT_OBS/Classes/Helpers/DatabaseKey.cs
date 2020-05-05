using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PRACT_OBS.Classes.Helpers
{
    public class DatabaseKey
    {
        public DatabaseKey() { 

        }

        public void MineKey()
        {
            string appAsarContent = CleanString(File.ReadAllText(Paths.AppAsarFilePath));
            string pattern = "pass:\"(.*?)\"}";

            MatchCollection matches = Regex.Matches(appAsarContent, pattern);
            List<string> l = new List<string>();
            foreach (Match match in matches)
            {
                Trace.TraceInformation(match.Groups[1].Value);
                l.Add(match.Groups[1].Value);
            }
            if (l.Count > 0)
                _EncryptionPassPhrase = l[0];
            else
                throw new DataException("Passphrase not found");
            BlowFish blowFish = new BlowFish(Encoding.ASCII.GetBytes(_EncryptionPassPhrase));
            byte[] data = Convert.FromBase64String(Paths.Rb6Options.options.Dp);
            //string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            byte[] out1 = blowFish.Decrypt_ECB(data);
            EncryptionKey = System.Text.ASCIIEncoding.ASCII.GetString(out1);
        }

        public string DatabasePassPhrase
        {
            get
            {
                return _DatabasePassPhrase;
            }
        }

        private string CleanString(string stringToClean)
        {
            string s = Regex.Replace(stringToClean, @"[^\u0000-\u007F]+", string.Empty);
            return s
                        .Replace(" ", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty);

        }
        private string _EncryptionPassPhrase { get; set; }
        private string _DatabasePassPhrase { get; set; }
        private string EncryptionKey { get; set; }
    }
}
