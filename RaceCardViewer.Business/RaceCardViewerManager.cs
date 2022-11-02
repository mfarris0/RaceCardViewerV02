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
                            LoadRaceDay(fields, raceCardViewer);
                        }
                        AddRace(fields, raceCardViewer);
                        previousRaceNumber = currentRaceNumber;
                    }
                    AddRaceHorse(fields, raceCardViewer);
                }
                else
                {
                    //todo 20221030: log what 'tempRaceNumber' is
                    // so, implement loggging !
                    
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

        private void LoadRaceDay(string[] fields, RaceCardViewer raceCardViewer)
        {
            raceCardViewer.RawRaceDay = new RawRaceDay(fields[1], fields[0]);
        }

        private void AddRace(string[] fields, RaceCardViewer raceCardViewer)
        {
            RawRace rawRace = new RawRace(raceCardViewer.RawRaceDay.RawRaceDayId,
                                          raceNumber: fields[2],
                                          purse: fields[11],
                                          raceType: fields[8],
                                          classification: fields[11],
                                          distance: fields[5],
                                          surface: fields[6]);
            raceCardViewer.RaceCard.Add(rawRace);
        }

        private void AddRaceHorse(string[] fields, RaceCardViewer raceCardViewer)
        {
            RawRaceHorse rawRaceHorse = new RawRaceHorse(
                                        rawRaceId: raceCardViewer.RaceCard[^1].RawRaceId,
                                        postPosition: fields[3],
                                        horseName: fields[44],
                                        morningLineOdds: fields[43],
                                        jockeyName: fields[32],
                                        weightAllowed: fields[50],
                                        trainerName: fields[27]
                                        );

            raceCardViewer.RaceHorseList.Add(rawRaceHorse);
        }


    }



}
