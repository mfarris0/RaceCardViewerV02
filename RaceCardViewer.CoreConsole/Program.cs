using RaceCardViewer.Utility;
using System;

namespace RaceCardViewer.CoreConsole
{
    class Program
    {
        
        private const string ApplicationTitle = "Race Card Viewer"; //todo 20221030: move  this to app.config
        public static bool exit = false;

        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            Console.Title = ApplicationTitle;
            DirectoryManager directoryManager = new DirectoryManager(ApplicationTitle);
            FileManager fileManager = new FileManager();
            Setup(directoryManager, fileManager);

            while (!exit)
            {
                DisplayFileList(fileManager);
            }
        }

        private static void DisplayFileList(FileManager fileManager)
        {
            foreach (var file in fileManager.DataFileList)
            {
                Console.WriteLine(file.Name);
                exit = true;
            }
        }

        #region ----- Directory and File Setup -----
        private static void Setup(DirectoryManager directoryManager, FileManager fileManager)
        {
            SetupDirectories(directoryManager);
            SetupSampleFiles(directoryManager, fileManager);
        }

        private static void SetupDirectories(DirectoryManager directoryManager)
        {
            if (!DirectoryManagerHelper.DirectoriesExist(directoryManager))
            {

                Console.Write("Creating directories... ");
                OperationResult operationResult = DirectoryManagerHelper.SetupDirectoryStructure(directoryManager);
                if (operationResult.Result == true)
                {
                    Console.WriteLine(operationResult.Message);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Directory setup failed \r\n{operationResult.Message}");
                    exit = true;
                }
            }

        }

        private static void SetupSampleFiles(DirectoryManager directoryManager, FileManager fileManager)
        {
            if (!FileManagerHelper.StarterFilesExist(directoryManager, fileManager))
            {
                Console.Write("Copying sample files to starter directory... ");
                OperationResult operationResult = FileManagerHelper.CopySampleFilesToStarterDataDirectory(directoryManager);
                if (operationResult.Result == true)
                {
                    FileManagerHelper.LoadDataFileList(directoryManager, fileManager);
                    Console.WriteLine(operationResult.Message);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Starter file setup failed \r\n{operationResult.Message}");
                    exit = true;
                }
            }
        }

        #endregion ----- Directory and File Setup -----

    }
}

