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
        public static byte _invalidResponseCount = 0;

        static void Main(string[] args)
        {
            Console.Title = ApplicationTitle;
            Setup(directoryManager, fileManager);
            Run();
        }

        private static void Run()
        {
            Console.Clear();
            _invalidResponseCount = 0;

            var mainMenu = new Menu(fileManager.DataFileList);
            var menuPainter = new MenuPainter(mainMenu);


            bool done = false;

            DisplayMainMenuPrompt();

            while (!exit)
            {
                menuPainter.Paint(0, 5);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);



                if (consoleKeyInfo.Key == ConsoleKey.Enter && mainMenu.SelectedIndex == -1)
                {
                    _invalidResponseCount++;
                    if (_invalidResponseCount == 3)
                        DisplayGoodbye();
                    else
                        DisplayMainMenuPrompt();
                }
                else
                {

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
                        RaceCardViewerViewModel viewer = DisplayRaceMenu(mainMenu, fileManager);
                        PromptForRaceNumber(viewer);
                    }
                }

            };


        }

        //private static bool ValidateMainMenuSelection(Menu mainMenu, ConsoleKeyInfo consoleKeyInfo)
        //{
        //    bool result = false;
        //    switch (consoleKeyInfo.Key)
        //    {
        //        case ConsoleKey.UpArrow:
        //            mainMenu.MoveUp();
        //            break;
        //        case ConsoleKey.DownArrow:
        //            mainMenu.MoveDown();
        //            break;
        //        case ConsoleKey.Enter:
        //            result = true;
        //            break;
        //    }

        //    return result;
        //}

        private static void DisplayMainMenuPrompt()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Racecard Viewer");
            Console.WriteLine();
            if (_invalidResponseCount > 0)
                DisplayInvalidAttemptMessage();
            else
                Console.WriteLine();
            Console.WriteLine("Use arrow keys to select a file and press [Enter]. Press 'X' to exit application.");
            Console.WriteLine();

        }

        private static RaceCardViewerViewModel DisplayRaceMenu(Menu mainMenu, FileManager fileManager)
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

            DisplayRaceNumberSelectionPrompt(viewer);
            string value = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(value))
            {
                _invalidResponseCount++;
                if (_invalidResponseCount == 3)
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

        private static void DisplayRaceNumberSelectionPrompt(RaceCardViewerViewModel viewer)
        {
            DisplayRaceCard(viewer);
            Console.WriteLine();
            DisplayRaceList(viewer);

            if (_invalidResponseCount > 0) DisplayInvalidAttemptMessage();

            Console.Write("Enter race number to view ('R' to select a different Race Day) and press [Enter]: ");
        }

        private static void DisplayInvalidAttemptMessage()
        {
            var currentColor = Console.ForegroundColor;

            if (_invalidResponseCount == 1)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine($"Your selection was invalid. Please try again. This is attempt {_invalidResponseCount + 1} of 3.");
            Console.ForegroundColor = currentColor;

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thank you for using Race Card Viewer!");
            Console.WriteLine("Goodbye.");
            Console.ForegroundColor = ConsoleColor.Gray;

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

