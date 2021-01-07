using Microsoft.Data.Sqlite;
using PRACT.Rekordbox6.Classes.Data;
using PRACT.Rekordbox6.Helpers;
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
                        lt.ID = DBNullHelper.SafeGetString(edr, i++);
                        lt.created_at = edr.GetDateTime(i++);
                        lt.FolderPath = DBNullHelper.SafeGetString(edr, i++).Replace('/', '\\');
                        lt.Title = DBNullHelper.SafeGetString(edr, i++);
                        lt.Artist = DBNullHelper.SafeGetString(edr, i++);
                        lt.ImagePath = DBNullHelper.SafeGetString(edr, i++).Replace('/','\\');
                        lt.BPM = (
                                    (double)(DBNullHelper.SafeGetInt32(edr, i++, 0))
                                    / 100).ToString("0.00", CultureInfo.InvariantCulture);

                        lt.Rating = DBNullHelper.SafeGetInt32(edr, i++, 0);
                        lt.ReleaseYear = DBNullHelper.SafeGetInt32(edr, i++, 0);
                        lt.ReleaseDate= DBNullHelper.SafeGetString(edr, i++);
                        lt.Length = TimeSpan.FromSeconds(edr.GetInt32(i++)).ToString(@"mm\:ss", CultureInfo.InvariantCulture);
                        lt.ColorID = DBNullHelper.SafeGetInt32(edr, i++, 0);
                        lt.TrackComment = DBNullHelper.SafeGetString(edr, i++);
                        lt.ColorName = DBNullHelper.SafeGetString(edr, i++);
                        lt.AlbumName = DBNullHelper.SafeGetString(edr, i++);
                        lt.LabelName = DBNullHelper.SafeGetString(edr, i++);
                        lt.GenreName = DBNullHelper.SafeGetString(edr, i++);
                        lt.KeyName = DBNullHelper.SafeGetString(edr, i++);
                        lt.RemixerName = DBNullHelper.SafeGetString(edr, i++);
                        lt.Message = DBNullHelper.SafeGetString(edr, i++);

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
