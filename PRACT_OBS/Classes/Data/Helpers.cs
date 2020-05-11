using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace PRACT_OBS.Classes.Data
{
    public class Helpers
    {
        private const string FIELD_SEPARATOR = " || ";

        public Helpers(MasterDB MasterDB)
        {
            _MasterDB = MasterDB;
        }

        public List<LastTrack> GetCurrentTracksList()
        {
            DbConnection dbConnection = _MasterDB.MasterDBConnection;
            List<LastTrack> lastTracks = null;

            using (var qry = dbConnection.CreateCommand())
            {
                qry.CommandText = Queries.QRY_ON_AIR;
                StringBuilder sb = new StringBuilder();
                
                using (DbDataReader edr = qry.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    lastTracks = new List<LastTrack>();
                    while (edr.Read())
                    {
                        LastTrack lt = new LastTrack();
                        lt.ID = edr.GetString(0);
                        lt.created_at = edr.GetDateTime(1);
                        lt.FolderPath = edr.GetString(2).Replace('/', '\\');
                        lt.Title = (edr.IsDBNull(3) ? string.Empty : edr.GetString(3));
                        lt.Artist = (edr.IsDBNull(4) ? string.Empty : edr.GetString(4));
                        lt.ImagePath = edr.GetString(5).Replace('/','\\');
                        lastTracks.Add(lt);
                    }
                }
            }

            return lastTracks;
        }

        public LastTrack GetLastTrack()
        {
            return GetCurrentTracksList().OrderByDescending(d => d.created_at).FirstOrDefault();
        }

        public string GetCurrentTracks()
        {
            StringBuilder sb = new StringBuilder();

            foreach (LastTrack lt in GetCurrentTracksList())
                sb.Append(LastTrackToString(lt));
            return sb.ToString();

        }
        protected string LastTrackToString(LastTrack LastTrack)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(LastTrack.created_at.ToString())
                .Append(FIELD_SEPARATOR)
                .Append(LastTrack.FolderPath)
                .Append(FIELD_SEPARATOR)
                .Append(LastTrack.Artist)
                .Append(FIELD_SEPARATOR)
                .Append(LastTrack.Title);
            return sb.ToString();
        }

        [DefaultValue(null)]
        protected MasterDB _MasterDB { get; set; }
    }
}
