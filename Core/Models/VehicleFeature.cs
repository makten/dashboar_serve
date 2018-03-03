using System.ComponentModel.DataAnnotations.Schema;

namespace dashboard.Core.Models
{
    [Table("VehicleFeatures")]
    //Workaround for many to many relationship n<->n
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }  
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }

        //Composite property "Fluent API"

    }
}