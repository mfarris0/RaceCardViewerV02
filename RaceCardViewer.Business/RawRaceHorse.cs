using System;

namespace RaceCardViewer.Business
{
    public class RawRaceHorse
    {
        public RawRaceHorse()
        {

        }

        public RawRaceHorse(string rawRaceId, string postPosition, string horseName, string morningLineOdds, string jockeyName, string weightAllowed, string trainerName)
        {
            this.RawRaceId = rawRaceId;
            this.PostPosition = postPosition;
            this.HorseName = horseName;
            this.MorningLineOdds = morningLineOdds;
            this.JockeyName = jockeyName;
            this.WeightAllowed = weightAllowed;
            this.TrainerName = trainerName;
            SetId();
        }

        private void SetId()
        {
            RawRaceHorseId = $"{RawRaceId}{PostPosition.PadLeft(2,'0')}";
        }
        public string RawRaceHorseId { get; private set; }
        public string RawRaceId { get; }
        public string PostPosition { get; }
        public string HorseName { get; }
        public string MorningLineOdds { get; }
        public string JockeyName { get; }
        public string WeightAllowed { get; }
        public string TrainerName { get; }

        public override string ToString()
        {
            string text = $"{PostPosition,2}  {HorseName,-25}  {MorningLineOdds,5}  {JockeyName,-25}  {TrainerName, -30}{WeightAllowed}";
            return text;
        }
    }


}
