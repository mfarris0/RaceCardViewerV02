using System;
using System.Collections.Generic;
using System.Text;

namespace RaceCardViewer.Business
{
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class TrackManager
    {
        public List<Track> TrackList()
        {
            List<Track> trackList = new List<Track>
            {
                new Track{ Id="AP", Name="Arlington Park"},
                new Track{ Id="AQU", Name="Aqueduct"},
                new Track{ Id="BEL", Name="Belmont Park"},
                new Track{ Id="CD", Name="Churchill Downs"},
                new Track{ Id="FL", Name="Finger Lakes"},
                new Track{ Id="KEE", Name="Keeneland"},
                new Track{ Id="PIM", Name="Pimlico"},
                new Track{ Id="PRX", Name="Parx"},
                new Track{ Id="SA", Name="Santa Anita"}
            };
            return trackList;

        }
    }
}
