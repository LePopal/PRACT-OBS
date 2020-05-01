using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace PRACT_OBS.Classes.Data
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
    }

    public class SQLCipherDbContext : DbContext
    {
        public string Filename { get; set; }
        //public DbSet<LastTrack> LastTracks { get; set; }

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
