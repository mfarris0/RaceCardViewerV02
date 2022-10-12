using System;
using System.IO;
using RaceCardViewer.Utility;

namespace RaceCardViewer.CoreConsole
{
    public static class DirectoryManagerHelper
    {
        public static bool DirectoriesExist(DirectoryManager directoryManager)
        {
            bool result = false;
            
            if (Directory.Exists(directoryManager.DataFileDirectory.FullName)
                && Directory.Exists(directoryManager.StarterDataFileDirectory.FullName)
                && Directory.Exists(directoryManager.LogFileDirectory.FullName)
                )
                result = true;

            return result;
        }

        public static OperationResult SetupDirectoryStructure(DirectoryManager directoryManager)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (CreateDirectory(directoryManager.MainDirectory))
                {
                    CreateDirectory(directoryManager.DataFileDirectory);
                    CreateDirectory(directoryManager.StarterDataFileDirectory);
                    CreateDirectory(directoryManager.LogFileDirectory);
                }
                operationResult.Result = true;
                operationResult.Message = "Directory creation successful.";
            }
            catch (Exception e)
            {
                operationResult.Result = false;
                operationResult.Message = e.Message;
            }

            return operationResult;
        }

        private static bool CreateDirectory(DirectoryInfo directoryToCreate)
        {
            bool result;

            if (Directory.Exists(directoryToCreate.FullName))
                result = true;
            else
            {
                try
                {
                    directoryToCreate.Create();
                    result = true;
                }
                catch (IOException e)
                {
                    string message = $"Error! An error occurred while creating the {directoryToCreate} directory: {e}";
                    throw new Exception(message);
                }
            }

            return result;
        }

    }


}
