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
                    artworkFile = Path.Combine(Paths.AnalysisDataRootPath, LastTrack.ImagePath);


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
        private static string _OutputFolder = string.Empty;
    }
}
