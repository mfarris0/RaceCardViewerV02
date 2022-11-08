using RaceCardViewer.CoreConsole.Classes;
using RaceCardViewer.Utility;
using System;
using System.IO;
using RaceCardViewer.Business;

namespace RaceCardViewer.CoreConsole
{
    class Program
    {
        private const string ApplicationTitle = "Race Card Viewer"; //todo 20221030: move  this to app.config
        
        private static DirectoryManager directoryManager = new DirectoryManager(ApplicationTitle);
        private static FileManager fileManager = new FileManager();

        public static bool exit = false;
        public static byte _noResponseCount = 0;

        static void Main(string[] args)
        {
            Console.Title = ApplicationTitle;
            Setup(directoryManager, fileManager);
            Run();
        }

        private static void Run()
        {
            Console.Clear();
            _noResponseCount = 0;

            var mainMenu = new Menu(fileManager.DataFileList);
            var menuPainter = new MenuPainter(mainMenu);


            bool done = false;

            Console.WriteLine("User arrow keys to select a file and press [Enter]. Press 'X' to exit application.");
            while (!exit)
            {

                menuPainter.Paint(0, 2);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);


                if (consoleKeyInfo.Key == ConsoleKey.X)
                    DisplayGoodbye();
                else
                {
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            mainMenu.MoveUp();
                            break;
                        case ConsoleKey.DownArrow:
                            mainMenu.MoveDown();
                            break;
                        case ConsoleKey.Enter:
                            done = true;
                            break;
                    }
                }


                if (done == true)
                {
                    RaceCardViewerViewModel viewer = DisplayRaceList(mainMenu, fileManager);
                    PromptForRaceNumber(viewer);
                }

            };


        }

        private static RaceCardViewerViewModel DisplayRaceList(Menu mainMenu, FileManager fileManager)
        {
            FileInfo file = fileManager.GetDataFile(mainMenu.SelectedOption);
            var viewer = new RaceCardViewerViewModel();
            var manager = new RaceCardViewerViewModelManager();
            manager.Load(file, viewer);
            return viewer;
        }

        private static void DisplayRaceList(RaceCardViewerViewModel viewer)
        {
            foreach (var race in viewer.RaceCard)
            {
                Console.WriteLine($"{race.RaceNumber} {race.Distance} {race.Surface} {race.Classification} {race.Purse} {race.RaceType}");
                Console.WriteLine();

            }
        }

        private static void PromptForRaceNumber(RaceCardViewerViewModel viewer)
        {
            Console.Clear();
            DisplayRaceCard(viewer);
            Console.WriteLine();
            DisplayRaceList(viewer);

            Console.Write("Enter race number to view ('R' to select a different Race Day) and press [Enter]: ");
            string value = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(value))
            {
                _noResponseCount++;
                if (_noResponseCount == 3)
                    DisplayGoodbye();
                else
                    PromptForRaceNumber(viewer);
            }
            else
            {
                ValidateSelection(value);
                return;

            }


        }

        private static void ValidateSelection(string value)
        {
            if (value.ToLower() == "r")
                Run();
            else if (value.IsNumeric())
            {
                throw new NotImplementedException("Validate race number");
            }
            else
            {
                throw new NotImplementedException("Race number is not valid.");
            }
        }


        private static void DisplayRaceCard(RaceCardViewerViewModel viewer)
        {
            Console.WriteLine();
            Console.WriteLine($"{viewer.RawRaceDay.RaceDate} {viewer.RawRaceDay.Track}");

        }

        private static void DisplayGoodbye()
        {
            exit = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Thank you for using Race Card Viewer");
        }

        private static void DisplayRaceCard(string selectedOption)
        {
            Console.WriteLine(selectedOption);
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

