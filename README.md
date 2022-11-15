# Race Card Viewer

This project will display basic thoroughbred horse race information using data pulled from datafiles provided.

First run of the application creates a 'Race Card Viewer' directory in the user's Documents directory along with data file and log directories under this main directory. The data file directory will be populated with sample files for viewing.

The user will be prompted with a list of datafiles to choose from for a given date and race track (the Race Day list). It will subsequently display the list of races. A race may then be selected to view the list of horses entered in the race along with post position, odds, jockey, trainer, weight carried.

---

## Technologies Used

The application will be written in C# and target .NET Core 3.1. It will demonstrate the use of classes along with generic list and dictionary. It will also demonstrate accessing the file system and creation of directories and files.
Further, it demonstrates a simple RaceCardViewerViewModel class as well as a manager for the class to facilitate loading data. As an extra, the application references the Microsoft.VisualBasic namespace to make use of it's TextFieldParser class. This class does a excellent job of reading a CSV file that contains string fields enclosed with double-quotes along with numeric fields with no quotes.

This app is to be run using Visual Studio 2019.

---

## Features Implemented from List Provided by Code Louisville

 - Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program.
 - Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in the program.
 - Read data from an external file, such as text, JSON, CSV, etc and use that data in the application.
 - Use a LINQ query to retrieve information from a data structure (such as a list or array) or file.

---

 ## Projects and Classes Implemented
 
 ### RaceCardViewer.Business
 - RaceCardViewerViewModel
 - RaceCardViewerViewModelManager
 - RaceType
 - RaceTypeManager	
 - RawRace
 - RawRaceDay
 - RawRaceHorse
 - Surface
 - SurfaceManager
 - Track
 - TrackManager
 ### RaceCardViewer.CoreConsole (Main App)
 - DirectoryManager
 - DirectoryManagerHelper
 - FileManager
 - FileManagerHelper
 - Menu (compliments of stackoverflow.com)
 - MenuPainter (compliments of stackoverflow.com)
 ### RaceCardViewer.Utility
 - ExtensionMethods
 - OperationResult
 