using RaceCardViewer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace RaceCardViewer.CoreConsole
{
    public class FileManagerHelper
    {
        private const string SampleFilesZipFile = "SampleFiles.zip"; //todo 20221030: move  this to app.config
        private const int StarterFileCount = 9; //todo 20221030: move  this to app.config
        public static bool StarterFilesExist(DirectoryManager directoryManager, FileManager fileManager)
        {
            bool result = false;

            LoadDataFileList(directoryManager, fileManager);
            if(fileManager.DataFileList.Count() == StarterFileCount)
            {
                result = true;
            }
            return result;
        }
        public static void LoadDataFileList(DirectoryManager directoryManager, FileManager fileManager)
        {
            fileManager.DataFileList = directoryManager.StarterDataFileDirectory.GetFiles().ToList();
        }

        public static OperationResult CopySampleFilesToStarterDataDirectory(DirectoryManager directoryManager)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                CheckStarterDirectory(directoryManager);
                FileInfo zipFileFullName = new FileInfo($"{directoryManager.StarterDataFileDirectory}\\{SampleFilesZipFile}");
                File.WriteAllBytes(zipFileFullName.FullName, Properties.Resources.SampleFiles);
                ZipFile.ExtractToDirectory(zipFileFullName.FullName, directoryManager.StarterDataFileDirectory.FullName);
                zipFileFullName.Delete();
                operationResult.Result = true;
                operationResult.Message = "done.";
            }
            catch (Exception e)
            {
                operationResult.Result = false;
                operationResult.Message = e.Message;
            }

            return operationResult;
        }

        private static void CheckStarterDirectory(DirectoryManager directoryManager)
        {
            if (Directory.Exists(directoryManager.StarterDataFileDirectory.FullName))
            {
                foreach (var file in directoryManager.StarterDataFileDirectory.GetFiles())
                    file.Delete();
            }

        }

    }


}
