using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

namespace RaceCardViewer.Business
{
    public class RaceCardViewerManager
    {
        public RaceCardViewerManager()
        {

        }

        public void Load(FileInfo file, RaceCardViewer raceCardViewer)
        {

            TextFieldParser textFieldParser = GetTextFieldParser(file);
            int previousRaceNumber = 0;

            while (!textFieldParser.EndOfData)
            {
                var fields = textFieldParser.ReadFields();
                if(int.TryParse(fields[2], out int currentRaceNumber))
                {
                    if (currentRaceNumber > previousRaceNumber)
                    {
                        if (currentRaceNumber == 1)
                        {
                            LoadRaceDay(fields);
                        }
                        AddRace(fields);
                        previousRaceNumber = currentRaceNumber;
                    }
                    AddRaceHorse(fields);
                }
            }

        }

        private static TextFieldParser GetTextFieldParser(FileInfo file)
        {
            TextFieldParser textFieldParser = new TextFieldParser(file.FullName);
            textFieldParser.TextFieldType = FieldType.Delimited;
            textFieldParser.HasFieldsEnclosedInQuotes = true;
            textFieldParser.Delimiters = new string[] { "," };
            return textFieldParser;
        }

        private void LoadRaceDay(string[] fields)
        {
            throw new NotImplementedException();
        }

        private void AddRace(string[] fields)
        {
            throw new NotImplementedException();
        }

        private void AddRaceHorse(string[] fields)
        {
            throw new NotImplementedException();
        }


    }



}
