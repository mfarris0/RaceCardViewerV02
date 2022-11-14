using System.Collections.Generic;
using System.Linq;

namespace RaceCardViewer.Business
{
    public class SurfaceManager
    {
        public string GetSurfaceName(string surfaceId)
        {

            return GetSurfaceList().FirstOrDefault(s => s.Id == surfaceId).Name;
        }

        private List<Surface> GetSurfaceList()
        {
            List<Surface> surfaceList = new List<Surface>
            {
                new Surface{ Id = "D", Name= "Dirt" },
                new Surface{ Id = "T", Name = "Turf"}

            };
            return surfaceList;
        }

    }
}
