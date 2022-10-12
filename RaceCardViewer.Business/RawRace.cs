using System;

namespace RaceCardViewer.Business
{
    public class RawRace
    {
        public RawRace(string rawRaceCardId, string rawRaceDayId, string postPosition, string horseName, string morningLineOdds, string jockeyName, string weightAllowed, string trainerName)
        {
            RawRaceCardId = rawRaceCardId;
            PostPosition = postPosition;
            HorseName = horseName;
            MorningLineOdds = morningLineOdds;
            JockeyName = jockeyName;
            WeightAllowed = weightAllowed;
            TrainerName = trainerName;
            SetId();
        }

        private void SetId()
        {
            RawRaceId = $"{RawRaceCardId}{PostPosition.PadLeft(2,'0')}";
        }
        public string RawRaceId { get; private set; }
        public string RawRaceCardId { get; private set; }
        public string PostPosition { get; }
        public string HorseName { get; }
        public string MorningLineOdds { get; }
        public string JockeyName { get; }
        public string WeightAllowed { get; }
        public string TrainerName { get; }
    }


}
