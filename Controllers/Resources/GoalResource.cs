using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core.Models;

namespace dashboard.Controllers.Resources
{
    public class GoalResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SprintCode { get; set; }
        public string Description { get; set; }
        public DateTime? GoalStart { get; set; }
        public DateTime? GoalEnd { get; set; }
        public int TeamMemberId { get; set; }
        public TeamMember TeamMember { get; set; }
    }
}
