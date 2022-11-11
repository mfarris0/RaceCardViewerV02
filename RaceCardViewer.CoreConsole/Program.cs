using RaceCardViewer.CoreConsole.Classes;
using RaceCardViewer.Utility;
using System;
using System.IO;
using RaceCardViewer.Business;
using System.Linq;

namespace RaceCardViewer.CoreConsole
{
    class Program
    {
        private const string ApplicationTitle = "Race Card Viewer"; //todo 20221030: move  this to app.config

        private static DirectoryManager directoryManager = new DirectoryManager(ApplicationTitle);
        private static FileManager fileManager = new FileManager();

        public static bool exit = false;
        public static byte _invalidResponseCount = 0;

        const string line = "-----------------------------------------------------------------------------------------------------";

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

        private static void PromptForRaceNumber(RaceCardViewerViewModel viewer)
        {
            Console.Clear();

            DisplayRaceNumberSelectionPrompt(viewer);
            string value = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(value))
            {
                DisplayInvalidRaceNumberPrompt(viewer);
            }
            else
            {
                ValidateRaceNumberInput(value, viewer);
            }

        }

        private static void DisplayRaceNumberSelectionPrompt(RaceCardViewerViewModel viewer)
        {
            DisplayRaceDayHeader(viewer);
            Console.WriteLine();
            DisplayRaceList(viewer);

            if (_invalidResponseCount > 0) DisplayInvalidAttemptMessage();

            Console.Write("Enter race number to view ('R' to select a different Race Day) and press [Enter]: ");
        }

        //private static void DisplayRaceCard(RaceCardViewerViewModel viewer)
        //{
        //    Console.WriteLine();
        //    DisplayRaceDayHeader(viewer);
        //}

        private static void DisplayRaceDayHeader(RaceCardViewerViewModel viewer)
        {
            DateTime raceDate = ConvertStringToDate(viewer.RawRaceDay.RaceDate);
           string trackName = GetTrackName(viewer.RawRaceDay.Track);

            Console.WriteLine(line);
            Console.WriteLine($"{raceDate:D}");
            Console.WriteLine($"{trackName}");
            Console.WriteLine(line);
        }

        private static string GetTrackName(string track)
        {
            TrackManager trackManager = new TrackManager();
            return trackManager.TrackList().FirstOrDefault(l => l.Id == track).Name;

        }

        private static DateTime ConvertStringToDate(string raceDate)
        {
            if (!raceDate.IsNumeric()) throw new ArgumentException("RaceDate is not a number.", nameof(raceDate));
            if (int.Parse(raceDate) <= 0) throw new ArgumentException("RaceDate is an invalid date", nameof(raceDate));
            
            int year = int.Parse(raceDate.Substring(0, 4));
            int month = int.Parse(raceDate.Substring(4, 2));
            int day = int.Parse(raceDate.Substring(6, 2));
            DateTime result = new DateTime(year, month, day);

            return result;
        }

        private static void DisplayRaceList(RaceCardViewerViewModel viewer)
        {
            foreach (var race in viewer.RaceCard)
            {
                Console.WriteLine($"{race.RaceNumber} {race.Distance} {race.Surface} {race.Classification} {race.Purse} {race.RaceType}");
                Console.WriteLine();
            }
        }


        private static RaceCardViewerViewModel DisplayRaceMenu(Menu mainMenu, FileManager fileManager)
        {
            FileInfo file = fileManager.GetDataFile(mainMenu.SelectedOption);
            var viewer = new RaceCardViewerViewModel();
            var manager = new RaceCardViewerViewModelManager();
            manager.Load(file, viewer);
            return viewer;
        }


        private static void ValidateRaceNumberInput(string value, RaceCardViewerViewModel viewer)
        {
            if (value.ToLower() == "r")
                Run();
            else if (value.IsNumeric())
            {
                var race = viewer.RaceCard.FirstOrDefault(r => r.RaceNumber == value);
                if (race == null)
                    DisplayInvalidRaceNumberPrompt(viewer);
                else
                {
                    DisplayRaceHorseList(viewer, race);
                    DisplayRaceHorseListPrompt();
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.D:
                            PromptForRaceNumber(viewer);
                            break;
                        case ConsoleKey.C:
                            Run();
                            break;
                        case ConsoleKey.E:
                            DisplayGoodbye();
                            break;
                        default:
                            DisplayInvalidAttemptMessage();
                            ValidateRaceNumberInput(value, viewer);
                            break;

                    }

                }
            }
            else
            {
                DisplayInvalidRaceNumberPrompt(viewer);
            }

        }


        private static void DisplayRaceHorseList(RaceCardViewerViewModel viewer, RawRace race)
        {
            Console.Clear();
            Console.WriteLine(race.Conditions);
            var raceHorseList = viewer.RaceHorseList.Where(r => r.RawRaceId == race.RawRaceId);

            DisplayRaceHorseHeader();
            foreach (var raceHorse in raceHorseList)
            {
                Console.WriteLine(raceHorse);
            }
        }

        private static void DisplayRaceHorseHeader()
        {
            const string PostPosition = "PP";
            const string HorseName = "Horse";
            const string MorningLineOdds = "Odds";
            const string JockeyName = "Jockey";
            const string TrainerName = "Trainer";
            const string WeightAllowed = "Weight";

            string text = $"{PostPosition,2}  {HorseName,-25}  {MorningLineOdds,5}  {JockeyName,-25}  {TrainerName,-30}{WeightAllowed}";

            Console.WriteLine(line);
            Console.WriteLine(text);
            Console.WriteLine(line);
        }

        private static void DisplayRaceHorseListPrompt()
        {
            Console.WriteLine();
            Console.WriteLine($"(D)isplay another Race | (C)hoose another Race Day | (E)xit Application");

        }

        private static void DisplayInvalidRaceNumberPrompt(RaceCardViewerViewModel viewer)
        {
            _invalidResponseCount++;
            if (_invalidResponseCount == 3)
                DisplayGoodbye();
            else
                PromptForRaceNumber(viewer);
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

