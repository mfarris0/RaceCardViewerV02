using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RaceCardViewer.CoreConsole
{
    public class FileManager
    {
        public FileManager()
        {
            InitializeThis();
        }

        private void InitializeThis()
        {
            DataFileList = new List<FileInfo>();
        }

        public  IEnumerable<FileInfo> DataFileList { get; set; }

        internal FileInfo GetDataFile(string fileName)
        {
            FileInfo file = DataFileList.FirstOrDefault(f => f.Name == fileName);
            return file;
        }
    }

}
