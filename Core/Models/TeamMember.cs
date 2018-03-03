using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dashboard.Core.Models
{
    [Table("TeamMembers")]
    public class TeamMember
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }  
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string MiddleNames { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [Required]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public IEnumerable<Goal> Goals { get; set; }

        public TeamMember()
        {
            Goals = new List<Goal>();
        }
    }
}
