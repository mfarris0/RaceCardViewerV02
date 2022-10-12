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
            SetupDirectories(directoryManager);
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

    }
}

