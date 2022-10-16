using System.Collections.Generic;

namespace RaceCardViewer.Business
{
    public class RaceCardViewer
    {

        public RawRaceDay RawRaceDay { get; private set; }
        public IEnumerable<RawRace> RaceCard { get; private set; }
        public IEnumerable<RawRaceHorse> RaceHorseList { get; private set; }

    }



}
