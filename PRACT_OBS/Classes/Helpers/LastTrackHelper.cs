using Microsoft.Data.Sqlite;
using PRACT.Rekordbox6.Classes.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PRACT_OBS.Classes.Helpers
{
    public class LastTrackHelper
    {
        private const string FIELD_SEPARATOR = " || ";

        public LastTrackHelper(MasterDB MasterDB)
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
                        int i = 0;
                        LastTrack lt = new LastTrack();
                        lt.ID = edr.GetString(i++);
                        lt.created_at = edr.GetDateTime(i++);
                        lt.FolderPath = edr.GetString(i++).Replace('/', '\\');
                        lt.Title = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.Artist = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.ImagePath = edr.GetString(i++).Replace('/','\\');
                        lt.BPM = (
                                    (double)((edr.IsDBNull(i) ? 0 : edr.GetInt32(i)))
                                    /100).ToString("0.00", CultureInfo.InvariantCulture);
                        i++;
                        lt.Rating = (edr.IsDBNull(i) ? 0 : edr.GetInt32(i));
                        i++;
                        lt.ReleaseYear = (edr.IsDBNull(i) ? (int?)null : edr.GetInt32(i));
                        i++;
                        lt.ReleaseDate= (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.Length = TimeSpan.FromSeconds(edr.GetInt32(i++)).ToString(@"mm\:ss", CultureInfo.InvariantCulture);
                        lt.ColorID = (edr.IsDBNull(i) ? (int?)null : edr.GetInt32(i));
                        i++;
                        lt.TrackComment = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.ColorName = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.AlbumName = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.LabelName = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.GenreName = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.KeyName = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.RemixerName = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        i++;
                        lt.Message = (edr.IsDBNull(i) ? string.Empty : edr.GetString(i));
                        
                        lastTracks.Add(lt);
                    }
                }
            }

            return lastTracks;
        }

        public LastTrack GetLastTrack()
        {
            if(ProgramSettings.DoNotExportPastTracks)
                return GetCurrentTracksList()
                    .OrderByDescending(d => d.created_at)
                    .Where(d => d.created_at >= Program.StartupTime)
                    .FirstOrDefault();
            else
                return GetCurrentTracksList()
                    .OrderByDescending(d => d.created_at)
                    .FirstOrDefault();
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
