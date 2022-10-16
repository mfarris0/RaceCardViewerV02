using RaceCardViewer.Utility;
using System;

namespace RaceCardViewer.CoreConsole
{
    class Program
    {
        private const string ApplicationTitle = "Race Card Viewer";
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

        }

        private static void Setup(DirectoryManager directoryManager, FileManager fileManager)
        {
            SetupDirectories(directoryManager);
            SetupStarterFiles(directoryManager, fileManager);
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

        private static void SetupStarterFiles(DirectoryManager directoryManager, FileManager fileManager)
        {
            if (!FileManagerHelper.StarterFilesExist(directoryManager, fileManager))
            {
                Console.Write("Copying starter files... ");
                OperationResult operationResult = FileManagerHelper.CopyStarterFilesToDataDirectory(directoryManager);
                if (operationResult.Result == true)
                {
                    FileManagerHelper.LoadFiles(directoryManager, fileManager);
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

    }
}

