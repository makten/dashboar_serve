using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dashboard.Core.Models
{
    [Table("Teams")]
    public class Team
    {
        public int Id {get; set;}
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        public IEnumerable<TeamMember> Members { get; set; }
        public IEnumerable<TeamEnvironment> TeamEnvironments { get; set; }
        public IEnumerable<UpcomingEvent> UpcomingEvents { get; set; }

        public Team()
        {
            Members = new List<TeamMember>();
            TeamEnvironments = new List<TeamEnvironment>();
            UpcomingEvents = new List<UpcomingEvent>();
        }
    }
}