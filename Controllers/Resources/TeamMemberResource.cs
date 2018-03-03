using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core.Models;

namespace dashboard.Controllers.Resources
{
    public class TeamMemberResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNames { get; set; }
        public string Avatar { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public IEnumerable<Goal> Goals { get; set; }

        public TeamMemberResource()
        {
            Goals = new List<Goal>();
        }
    }
}
