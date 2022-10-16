using System;

namespace RaceCardViewer.Business
{
    public class RawRaceHorse
    {
        public RawRaceHorse(string rawRaceId, string postPosition, string horseName, string morningLineOdds, string jockeyName, string weightAllowed, string trainerName)
        {
            RawRaceId = rawRaceId;
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
            RawRaceHorseId = $"{RawRaceId}{PostPosition.PadLeft(2,'0')}";
        }
        public string RawRaceHorseId { get; private set; }
        public string RawRaceId { get; private set; }
        public string PostPosition { get; }
        public string HorseName { get; }
        public string MorningLineOdds { get; }
        public string JockeyName { get; }
        public string WeightAllowed { get; }
        public string TrainerName { get; }
    }


}
