using System.Collections.Generic;
using System.Linq;

namespace RaceCardViewer.Business
{
    public class RaceTypeManager
    {
        public string GetRaceTypeName(string raceTypeId)
        {
            return RaceTypeList().FirstOrDefault(rt => rt.Id == raceTypeId).Name;
        }

        private List<RaceType> RaceTypeList()
        {
            List<RaceType> trackList = new List<RaceType>
            {
                new RaceType{ Id="G1", Name="Grade I Stake/Handicap"},
                new RaceType{ Id="G2", Name="Grade II Stake/Handicap"},
                new RaceType{ Id="G3", Name="Grade III Stake/Handicap"},
                new RaceType{ Id="N", Name="Non-graded Stake/Handicap"},
                new RaceType{ Id="A", Name="Allowance"},
                new RaceType{ Id="R", Name="Starter Allowance"},
                new RaceType{ Id="T", Name="Starter Handicap"},
                new RaceType{ Id="C", Name="Claiming"},
                new RaceType{ Id="S", Name="Maiden Special Weight"},
                new RaceType{ Id="M", Name="Maiden Claiming"},
                new RaceType{ Id="AO", Name="Allowance Optional Claiming"},
                new RaceType{ Id="MO", Name="Maiden Optional Claiming"},
                new RaceType{ Id="NO", Name="Optional Claiming Stakes"},
                new RaceType{ Id="CO", Name="Optional Claiming"}
            };
            return trackList;

        }


    }


}
