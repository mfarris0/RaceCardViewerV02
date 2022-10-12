using System;
using System.IO;

namespace RaceCardViewer.CoreConsole
{

    public class DirectoryManager
    {
        private const string DataFileDirectoryName = "RawDataFiles";
        private const string StarterDataFileDirectoryName = "RawDataFiles\\Starter";
        private const string LogDirectoryName = "Logs";

        public DirectoryManager(string applicationTitle)
        {
            ApplicationTitle = applicationTitle;
            InitializeThis();
        }

        private void InitializeThis()
        {
            SetMainDirectory();
            SetDataFileDirectory();
            SetStarterDataFileDirectory();
            SetLogDirectory();
        }

        public string ApplicationTitle { get; private set; }
        public DirectoryInfo MainDirectory { get; private set; }
        public DirectoryInfo DataFileDirectory { get; private set; }
        public DirectoryInfo StarterDataFileDirectory { get; private set; }
        public DirectoryInfo LogFileDirectory { get; private set; }

        private void SetMainDirectory()
        {
            MainDirectory = new DirectoryInfo
                ($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{ApplicationTitle}");
        }
        private void SetDataFileDirectory()
        {
            DataFileDirectory = new DirectoryInfo($"{MainDirectory.FullName}\\{DataFileDirectoryName}");
        }
        private void SetStarterDataFileDirectory()
        {
            StarterDataFileDirectory = new DirectoryInfo($"{MainDirectory.FullName}\\{StarterDataFileDirectoryName}");
        }
        private void SetLogDirectory()
        {
            LogFileDirectory = new DirectoryInfo($"{MainDirectory.FullName}\\{LogDirectoryName}");
        }

    }


}
