using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace PRACT.Rekordbox6.Classes.Data
{
    public class LastTrack
    {
        [Key]
        public string ID { get; set; }
        public DateTime created_at { get; set; }
        public string FolderPath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string ImagePath { get; set; }
        public int BPM { get; set; }
        public int Rating { get; set; }
        public int? ReleaseYear { get; set; }
        public string ReleaseDate { get; set; }
        public int Length { get; set; }
        public int? ColorID { get; set; }
        public string TrackComment { get; set; }
        public string ColorName { get; set; }
        public string AlbumName { get; set; }
        public string LabelName { get; set; }
        public string GenreName { get; set; }
        public string KeyName { get; set; }
        public string RemixerName { get; set; }
        public string Message { get; set; }
    }

    public class SQLCipherDbContext : DbContext
    {
        public string Filename { get; set; }

        public SQLCipherDbContext(string filename)
        {
            Filename = filename;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Filename}");
        }
    }
}
