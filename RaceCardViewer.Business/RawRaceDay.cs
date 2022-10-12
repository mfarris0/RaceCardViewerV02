namespace RaceCardViewer.Business
{
    public class RawRaceDay
    {
        public RawRaceDay(string raceDate, string track)
        {
            RaceDate = raceDate;
            Track = track;
            SetId();
        }

        private void SetId()
        {
            RawRaceDayId = $"{RaceDate}{Track}";
        }

        public string RawRaceDayId { get; private set; }

        public string RaceDate { get; }
        public string Track { get; }

    }

}
