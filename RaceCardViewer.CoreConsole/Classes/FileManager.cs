using System.Collections.Generic;
using System.IO;

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

    }

}
