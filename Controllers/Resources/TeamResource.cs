using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core.Models;

namespace dashboard.Controllers.Resources
{
    public class TeamResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public IEnumerable<TeamMember> Members { get; set; }
        public IEnumerable<TeamEnvironment> TeamEnvironments { get; set; }
        public IEnumerable<UpcomingEvent> UpcomingEvents { get; set; }

        public TeamResource()
        {
            Members = new List<TeamMember>();
            TeamEnvironments = new List<TeamEnvironment>();
            UpcomingEvents = new List<UpcomingEvent>();
        }
    }
}
