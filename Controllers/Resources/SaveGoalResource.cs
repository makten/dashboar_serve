using System;
using System.ComponentModel.DataAnnotations;
using dashboard.Core.Models;

namespace dashboard.Controllers.Resources
{
    public class SaveGoalResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string SprintCode { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime? GoalStart { get; set; }
        public bool Done { get; set; }
        public DateTime? GoalEnd { get; set; }
        public int TeamMemberId { get; set; }
    }
}
