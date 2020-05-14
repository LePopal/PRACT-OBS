using System;
using System.Collections.Generic;
using System.Text;

namespace PRACT_OBS.Classes.Helpers
{
    public static class SystemPaths
    {
        public static string MyDocumentsFolder
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }

        public static string AppDataFolder
        {
            get
            {
                return Environment.GetEnvironmentVariable("APPDATA");
            }
        }
    }
}
