using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dashboard.Core.Models
{
    [Table("UpcomingEvents")]
    public class UpcomingEvent
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
