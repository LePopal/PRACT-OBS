using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace PRACT.Common.IO
{
    public class FileFinder
    {
        public string Root { get; set; }
        public string Filename { get; set; }
        protected List<string> FolderList
        {
            get
            {
                if (_FoldersList == null)
                    _FoldersList = new List<string>();
                return _FoldersList;
            }
        }
        private List<string> _FoldersList;
        public FileFinder(string root, string filename)
        {
            this.Root = root;
            this.Filename = filename;
        }
        public List<string> FindFile()
        {
            return FindFile(Root, Filename);
            
        }

        private List<string> FindFile(string path, string pattern)
        {
            var files = new List<string>();
            Trace.WriteLine(path);
            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                    files.AddRange(FindFile(directory, pattern));
            }
            catch (UnauthorizedAccessException) { }

            return files;
        }
    }
}
