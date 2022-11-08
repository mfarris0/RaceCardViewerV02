using System.Collections.Generic;

namespace RaceCardViewer.Business
{
    public class RaceCardViewerViewModel
    {

        public RawRaceDay RawRaceDay { get; set; }
        public List<RawRace> RaceCard { get; set; } = new List<RawRace>();
        public List<RawRaceHorse> RaceHorseList { get; set; } = new List<RawRaceHorse>();

    }



}
