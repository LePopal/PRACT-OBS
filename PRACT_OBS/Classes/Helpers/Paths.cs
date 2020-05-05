using PRACT_OBS.Classes.Data.Rekordbox6OptionsModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRACT_OBS.Classes.Helpers
{
    public static class Paths
    {
        public static string RekordboxRoot()
        {
            return Path.Combine(GetAppDataFolder(), "Rekordbox");
        }

        public static string Rekordbox6AgentOptionsFilePath ()
        {
            return Path.Combine(GetAppDataFolder(), Rekordbox6AgentRoot, Rekordbox6AgentOptionsFile);
        }

        public static string AnalysisDataRootPath
        {
            get
            {
                return Rb6Options.options.AnalysisDataRootPath;
            }
        }

        public static string SettingsRootPath
        {
            get
            {
                return Rb6Options.options.SettingsRootPath;
            }
        }

        /// <summary>
        /// This folder is deduced from the lang settings
        /// The binaries folder is the parent folder of the file containing the language settings
        /// </summary>
        public static string RekordboxBinariesFolder
        {
            get
            {
                FileInfo fi = new FileInfo(Rb6Options.options.LangPath);
                return fi.Directory.Parent.FullName;
            }
        }

        public static string AppAsarFilePath
        {
            get
            {
                return Path.Combine(RekordboxBinariesFolder, @"rekordboxAgent-win32-x64\resources\app.asar");
            }
        }

        public static string DbPath
        {
            get
            {
                return Rb6Options.options.DbPath;
            }
        }

        private static Rekordbox6Options LoadRekordboxOptions()
        {
            string jsonString = File.ReadAllText(Rekordbox6AgentOptionsFilePath());
            return JsonSerializer.Deserialize<Rekordbox6Options>(RewriteRekordboxAgentOptions(jsonString));
        }

        private static string GetAppDataFolder()
        {
            return Environment.GetEnvironmentVariable("APPDATA");
        }

        /// <summary>
        /// The Rekordbox Agent Options file can't be processed as is by System.Text.Json so we tidy it a bit
        /// This method is absolutely awful and needs refactoring :)
        /// </summary>
        /// <param name="JsonString"></param>
        /// <returns>A rewritten deserializable Json string</returns>
        private static string RewriteRekordboxAgentOptions(string JsonString)
        {
            int optionsSectionStart = JsonString.IndexOf("\"options\"");
            int defaultSectionStart = JsonString.IndexOf("\"defaults\"");
            int optionSectionEnd = -1;
            bool optionSectionPopulated = false;

            Stack<int> stack = new Stack<int>();
            for (int i = optionsSectionStart; i < defaultSectionStart; i++)
            {
                switch (JsonString[i])
                {
                    case '[':
                        stack.Push(i);
                        optionSectionPopulated = false;
                        break;
                    case ']':
                        if (stack.Any())
                        {
                            optionSectionEnd = i;
                            optionSectionPopulated = true;
                        }
                        break;
                    default:
                        break;
                }
                if (optionSectionPopulated & !stack.Any())
                {
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            
            bool becomesComma = true;
            for (int i = 0; i<JsonString.Length; i++)
            {
                if (i<= optionsSectionStart | i>optionSectionEnd)
                {
                    sb.Append(JsonString[i]);
                } else 
                {
                    if (JsonString[i] == ',')
                    {
                        if (becomesComma)
                        {
                            sb.Append(':');
                        }
                        else
                        {
                            sb.Append(',');
                        }
                        becomesComma = !becomesComma;
                    }
                    else
                    {
                        sb.Append(JsonString[i].ToString()
                            .Replace("[", string.Empty)
                            .Replace("]", string.Empty));
                    }
                }
            }
            sb = sb
                .Replace("\"options\":", "\"options\":{")
                .Replace(",\"defaults\":", "},\"defaults\":");

            return sb.ToString();
        }

        public static Rekordbox6Options Rb6Options
        {
            get
            {
                if (_Rb6Options == null)
                    _Rb6Options = LoadRekordboxOptions();
                return _Rb6Options;
            }
        }
            

        private static Rekordbox6Options _Rb6Options = null;

        private const string Rekordbox6AgentRoot = @"Pioneer\rekordboxAgent\storage";
        private const string Rekordbox6AgentOptionsFile = "options.json";

    }
}
