using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data.Common;
using System.IO;

namespace PRACT_OBS.Classes.Data
{
    public class MasterDB
    {
        public MasterDB(string Path, string EncryptionKey)
        {
            this.EncryptionKey = EncryptionKey;
            SetPath(Path);
        }


        public DbConnection MasterDBConnection
        {
            get
            {
                if (_MasterDBConection == null)
                {
                    SQLCipherDbContext context = new SQLCipherDbContext(Path);
                    var connection = context.Database.GetDbConnection();
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"PRAGMA key = '{EncryptionKey}'; ";

                        command.ExecuteNonQuery();
                    }
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"PRAGMA cipher_default_compatibility = 4;";
                        command.ExecuteNonQuery();
                    }
                    context.Database.EnsureCreated();
                    _MasterDBConection = connection;

                }
                return _MasterDBConection;
            }
        }
        public string Path
        {
            get
            {
                return _Path;
            }
        }
        [DefaultValue("")]
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Checks the new Path then stores id
        /// </summary>
        /// <param name="Path"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public void SetPath (string Path)
        {
            if (File.Exists(Path))
                _Path = Path;
            else
                throw new FileNotFoundException(string.Format("The Master DB could not be located at {0}", Path));
        }
        #region Private Members
        private string _Path = string.Empty;
        private DbConnection _MasterDBConection = null;
        #endregion
    }
}
