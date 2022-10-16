﻿using System;
using System.Collections.Generic;

namespace RaceCardViewer.Business
{
    public class RawRace
    {
        public RawRace(string rawRaceDayId, string raceNumber, string purse, string raceType, string classification, string distance, string surface, string numberOfEntries, string postTime)
        {
            RawRaceDayId = rawRaceDayId;
            RaceNumber = raceNumber;
            Purse = purse;
            RaceType = raceType;
            Classification = classification;
            Distance = distance;
            Surface = surface;
            NumberOfEntries = numberOfEntries;
            PostTime = postTime;
            SetId();
        }

        private void SetId()
        {
            RawRaceId = $"{RawRaceDayId}{RaceNumber.PadLeft(2,'0')}";
        }

        public string RawRaceId { get; private set; }
        public string RawRaceDayId { get; }
        public string RaceNumber { get; }
        public string Purse { get; }
        public string RaceType { get; }
        public string Classification { get; }
        public string Distance { get; }
        public string Surface { get; }
        public string NumberOfEntries { get; }
        public string PostTime { get; }
        public IEnumerable<RawRaceHorse> RaceHorseList { get; set; }

    }

}
