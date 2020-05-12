using PRACT_OBS.Classes.Data;
using PRACT_OBS.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace PRACT_OBS.Classes
{
    public static class OBSExport
    {
        public const string TOKEN_ID = "%ID%";
        public const string TOKEN_FOLDER_PATH = "%FOLDERPATH%";
        public const string TOKEN_ARTIST = "%ARTIST%";
        public const string TOKEN_TITLE = "%TITLE%";
        public const string TOKEN_IMAGE_PATH = "%IMAGEPATH%";
        public const string TOKEN_BPM = "%BPM%";
        public const string TOKEN_RATING = "%RATING%";
        public const string TOKEN_RELEASE_YEAR = "%YEAR%";
        public const string TOKEN_LENGTH = "%LENGTH%";
        public const string TOKEN_COLOR_ID = "%COLORID%";
        public const string TOKEN_COLOR_NAME = "%COLORNAME%";
        public const string TOKEN_COMMENT = "%COMMENT%";
        public const string TOKEN_ALBUM = "%ALBUM%";
        public const string TOKEN_LABEL = "%LABEL%";
        public const string TOKEN_GENRE = "%GENRE%";
        public const string TOKEN_KEY = "%KEY%";
        public const string TOKEN_REMIXER = "%REMIXER%";
        public const string TOKEN_MESSAGE = "%MESSAGE%";
        public static void ExportLastTrack(LastTrack LastTrack)
        {
            // Check if it's time to remove what's on screen or not
            bool timesUp = (PreviousUpdate == DateTime.MinValue) ||
                DateTime.Now > PreviousUpdate.Add(TimeSpan.FromSeconds((double)OnScreenDuration));
            bool rewriteArtistTitleFile = true;

            if (LastTrack != null)
            {
                // Export the artist
                // Check if the artist is the same than before, to limit unnecessary writes
                if (LastTrack.Artist != PreviousArtist)
                {
                    rewriteArtistTitleFile = true;
                    WriteMetaData(Path.Combine(OutputFolder, ARTIST_FILENAME), LastTrack.Artist);
                    PreviousArtist = LastTrack.Artist;
                    PreviousUpdate = DateTime.Now;
                    IsArtistFileEmpty = false;
                }
                else
                {
                    if (!IsArtistFileEmpty && timesUp)
                    {
                        rewriteArtistTitleFile = true;
                        WriteMetaData(Path.Combine(OutputFolder, ARTIST_FILENAME), string.Empty);
                        IsArtistFileEmpty = true;
                    }
                }
                // Export the title
                // Check if the title is the same than before, to limit unnecessary writes
                if (LastTrack.Title != PreviousTitle)
                {
                    rewriteArtistTitleFile = true;
                    WriteMetaData(Path.Combine(OutputFolder, TITLE_FILENAME), LastTrack.Title);
                    PreviousTitle = LastTrack.Title;
                    PreviousUpdate = DateTime.Now;
                    IsTitleFileEmpty = false;
                }
                else
                {
                    if (!IsTitleFileEmpty && timesUp)
                    {
                        rewriteArtistTitleFile = true;
                        WriteMetaData(Path.Combine(OutputFolder, TITLE_FILENAME), string.Empty);
                        IsTitleFileEmpty = true;
                    }
                }

                // Export the Artist+Title
                if(rewriteArtistTitleFile)
                {
                    WriteMetaData(Path.Combine(OutputFolder, ARTIST_TITLE_FILENAME),
                        string.Format("{0} {1} {2} {1}{1}{1} ", LastTrack.Artist, ProgramSettings.ArtistTitleSeparator, LastTrack.Title));
                    PreviousUpdate = DateTime.Now;
                }
                else
                {
                    if(timesUp)
                    {
                        WriteMetaData(Path.Combine(OutputFolder, ARTIST_TITLE_FILENAME), string.Empty);
                    }
                }

                // Export the artwork
                // Check if the artwork is the same than before, to limit unnecessary writes

                string artworkFile;
                if (string.IsNullOrEmpty(LastTrack.ImagePath))
                    // No artwork available, we take the default artwork if available
                    artworkFile = ProgramSettings.DefaultArtwork;
                else
                    // Artwork file is available
                    artworkFile = Paths.AnalysisDataRootPath + LastTrack.ImagePath;


                if (File.Exists(artworkFile))
                {
                    string artworkMD5 = GetMD5(artworkFile);
                    if (artworkMD5 != PreviousArtwork)
                    {
                        File.Copy(artworkFile,
                                Path.Combine(OutputFolder, ARTWORK_FILENAME),
                                true);
                        PreviousArtwork = artworkMD5;
                        PreviousUpdate = DateTime.Now;
                        IsArtworkFileEmpty = false;
                    }
                    else
                    {
                        if(!IsArtworkFileEmpty && timesUp)
                        {
                            File.Delete(Path.Combine(OutputFolder, ARTWORK_FILENAME));
                            IsArtworkFileEmpty = true;
                        }
                    }
                }
                else
                {
                    File.Delete(Path.Combine(OutputFolder, ARTWORK_FILENAME));
                    PreviousUpdate = DateTime.Now;
                    IsArtworkFileEmpty = true;
                }

            }
        }
        public static string OutputFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_OutputFolder))
                {
                    _OutputFolder = ProgramSettings.OutputFolder;
                }
                return _OutputFolder;
            }
            set
            {
                _OutputFolder = value;
            }
        }

        public static void Reset()
        {
            PreviousArtist = string.Empty;
            PreviousTitle = string.Empty;
            PreviousUpdate = DateTime.MinValue;
            PreviousArtwork = string.Empty;
            IsArtistFileEmpty = false;
            IsTitleFileEmpty = false;
            IsArtworkFileEmpty = false;
        }
        /// <summary>
        /// Gets the MD5 checksum of a given file. Usefull to check if the file is the same than another one
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetMD5(string filename)
        {
            if (File.Exists(filename))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        return Encoding.Default.GetString(md5.ComputeHash(stream));
                    }
                }
            }
            else
                return string.Empty;
        }

        private static void WriteMetaData(string FileName, string MetaData)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(MetaData);
                sw.Close();
            }
        }

        /// <summary>
        /// 0 means forever
        /// </summary>
        private static int OnScreenDuration
        {
            get
            {
                return ProgramSettings.OnScreenDuration == 0 ? int.MaxValue : ProgramSettings.OnScreenDuration;
            }
        }

        private static string CustomFormat(LastTrack lastTrack)
        {
            if (ProgramSettings.CustomExportEnabled)

                return ProgramSettings.CustomExportFormat
                            .Replace(TOKEN_ID, lastTrack.ID, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_FOLDER_PATH, lastTrack.FolderPath, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_ARTIST, lastTrack.Artist, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_TITLE, lastTrack.Title, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_IMAGE_PATH, lastTrack.ImagePath, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_BPM, lastTrack.BPM.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_RATING, lastTrack.Rating.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_RELEASE_YEAR, lastTrack.ReleaseYear.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_LENGTH, lastTrack.Length.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_COLOR_ID, lastTrack.ColorID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_COLOR_NAME, lastTrack.ColorName, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_COMMENT, lastTrack.TrackComment, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_ALBUM, lastTrack.AlbumName, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_LABEL, lastTrack.LabelName, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_GENRE, lastTrack.GenreName, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_KEY, lastTrack.KeyName, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_REMIXER, lastTrack.RemixerName, StringComparison.InvariantCultureIgnoreCase)
                            .Replace(TOKEN_MESSAGE, lastTrack.Message, StringComparison.InvariantCultureIgnoreCase);
            else
                return null;
        }

        private static string PreviousArtist { get; set; }
        private static string PreviousTitle { get; set; }
        public static DateTime PreviousUpdate { get; set; }
        private static string PreviousArtwork { get; set; }

        /// <summary>
        /// Used to know if the Artist file is empty or not. Prevents unnecessary I/O
        /// </summary>
        private static bool IsArtistFileEmpty = false;
        /// <summary>
        /// Used to know if the Title file is empty or not. Prevents unnecessary I/O
        /// </summary>
        private static bool IsTitleFileEmpty = false;
        /// <summary>
        /// Used to know if the Artwork file is empty or not. Prevents unnecessary I/O
        /// </summary>
        private static bool IsArtworkFileEmpty = false;

        private const string ARTWORK_FILENAME = "Artwork.jpg";
        private const string ARTIST_FILENAME = "Artist.txt";
        private const string TITLE_FILENAME = "Title.txt";
        private const string ARTIST_TITLE_FILENAME = "ArtistTitle.txt";
        private const string CUSTOM_FILENAME = "PRACT_OBS_Custom_Export.txt";
        private const string JSON_FILENAME = "PRACT_OBS.json";
        private static string _OutputFolder = string.Empty;
    }
}
