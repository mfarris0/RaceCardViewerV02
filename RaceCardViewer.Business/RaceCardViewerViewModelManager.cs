using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

namespace RaceCardViewer.Business
{
    public class RaceCardViewerViewModelManager
    {
        public RaceCardViewerViewModelManager()
        {

        }


        public void Load(FileInfo file, RaceCardViewerViewModel viewer)
        {

            TextFieldParser textFieldParser = GetTextFieldParser(file);
            int previousRaceNumber = 0;
            //string currentRaceId;

            while (!textFieldParser.EndOfData)
            {
                var fields = textFieldParser.ReadFields();
                string tempRaceNumber = fields[2];
                if (int.TryParse(tempRaceNumber, out int currentRaceNumber))
                {
                    if (currentRaceNumber > previousRaceNumber)
                    {
                        if (currentRaceNumber == 1)
                        {
                            LoadRaceDay(fields, viewer);
                        }
                        AddRace(fields, viewer);
                        previousRaceNumber = currentRaceNumber;
                    }
                    AddRaceHorse(fields, viewer);
                }

            }

        }

        private static TextFieldParser GetTextFieldParser(FileInfo file)
        {
            TextFieldParser textFieldParser = new TextFieldParser(file.FullName)
            {
                TextFieldType = FieldType.Delimited,
                HasFieldsEnclosedInQuotes = true,
                Delimiters = new string[] { "," }
            };
            return textFieldParser;
        }

        private void LoadRaceDay(string[] fields, RaceCardViewerViewModel viewer)
        {
            viewer.RawRaceDay = new RawRaceDay(fields[1], fields[0]);
        }

        private void AddRace(string[] fields, RaceCardViewerViewModel raceCardViewer)
        {
            RawRace rawRace = new RawRace(raceCardViewer.RawRaceDay.RawRaceDayId,
                                          raceNumber: fields[2],
                                          purse: fields[11],
                                          raceType: fields[8],
                                          classification: fields[10],
                                          distance: fields[5],
                                          surface: fields[6],
                                          conditions: fields[15]);
            raceCardViewer.RaceCard.Add(rawRace);
        }

        private void AddRaceHorse(string[] fields, RaceCardViewerViewModel viewer)
        {
            RawRaceHorse rawRaceHorse = new RawRaceHorse(
                                        rawRaceId: viewer.RaceCard[^1].RawRaceId,
                                        postPosition: fields[3],
                                        horseName: fields[44],
                                        morningLineOdds: fields[43],
                                        jockeyName: fields[32],
                                        weightAllowed: fields[50],
                                        trainerName: fields[27]
                                        );

            viewer.RaceHorseList.Add(rawRaceHorse);
        }


    }



}
