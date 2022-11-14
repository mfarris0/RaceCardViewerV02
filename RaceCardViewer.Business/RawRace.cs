using System;
using System.Collections.Generic;
using System.Text;

namespace RaceCardViewer.Business
{
    public class RawRace
    {
        public RawRace(string rawRaceDayId, string raceNumber, string purse, string raceType, string classification, string distance, string surface, string conditions)
        {
            RawRaceDayId = rawRaceDayId;
            RaceNumber = raceNumber;
            Purse = purse;
            RaceType = raceType;
            Classification = classification;
            Distance = distance;
            Surface = surface;
            Conditions = conditions;
            SetId();
        }

        private void SetId()
        {
            RawRaceId = $"{RawRaceDayId}{RaceNumber.PadLeft(2, '0')}";
        }

        public string RawRaceId { get; private set; }
        public string RawRaceDayId { get; }
        public string RaceNumber { get; }
        public string Purse { get; }
        public string RaceType { get; }
        public string Classification { get; }
        public string Distance { get; }
        public string Surface { get; }
        public string Conditions { get; set; }
        public IEnumerable<RawRaceHorse> RaceHorseList { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

    }

    public class RawRaceManager
    {
        private const int YardsPerMile = 1760;

        public string GetDetailLine(RawRace race)
        {
            //if (race.RawRaceDayId == null) throw new ArgumentNullException($"{nameof(race.RawRaceDayId)} cannot be null");
            //if (race.RawRaceId == null) throw new ArgumentNullException($"{nameof(race.RawRaceId)} cannot be null");


            int raceNumber = int.Parse(race.RaceNumber);
            int purse = int.Parse(race.Purse);
            string raceTypeName = GetRaceTypeName(race.RaceType);
            string surfaceName = GetSurfaceName(race.Surface);
            string distanceText = GetDistanceToDisplay(race.Distance);
            string text = $"{raceNumber,2}  {purse,10:c0}  {raceTypeName,-27}  {surfaceName,-7}  {distanceText}";
            return text;
        }

        private double YardsToFurlongs(int yards)
        {
            double furlongs = yards / YardsPerMile;
            return furlongs;
        }

        private string GetRaceTypeName(string raceTypeId)
        {
            RaceTypeManager raceTypeManager = new RaceTypeManager();
            return raceTypeManager.GetRaceTypeName(raceTypeId);
        }

        private string GetSurfaceName(string surfaceId)
        {
            SurfaceManager surfaceManager = new SurfaceManager();
            return surfaceManager.GetSurfaceName(surfaceId);
        }

        private string GetDistanceToDisplay(string yardString)
        {
            DistanceManager distanceManager = new DistanceManager();
            double yards = double.Parse(yardString);
            return distanceManager.DistanceToDisplay(yards);
        }
    }

    public class Distance
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class DistanceManager
    {
        const int yardsPerFurlong = 220;
        const int yardsPerMile = 1760;


        private double YardsToFurlongs(int yards)
        {
            double furlongs = yards / 220;
            return furlongs;
        }

        public string DistanceToDisplay(double yards)
        {
            string text;
            double value;

            if (yards < 1760)
            {
                value = yards / yardsPerFurlong;
                text = $"{value:n2} furlongs";
            }
            else
            {
                value = yards / yardsPerMile;
                text = $"{value:n2} miles";
            }
            return text;
        }
    }
}
