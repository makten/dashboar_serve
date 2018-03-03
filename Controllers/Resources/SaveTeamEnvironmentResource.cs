using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace dashboard.Controllers.Resources
{
    public class SaveTeamEnvironmentResource
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string ExtraDetails { get; set; }
        public int ClientGroupId { get; set; }
        public int TeamId { get; set; }

    }
}