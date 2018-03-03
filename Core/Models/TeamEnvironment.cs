using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dashboard.Core.Models
{
   [Table("TeamEnvironments")]
    public class TeamEnvironment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string ExtraDetails { get; set; }
        public int ClientGroupId { get; set; }
        public ClientGroup ClientGroup { get; set; }
    }
}
