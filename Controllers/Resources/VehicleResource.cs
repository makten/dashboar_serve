using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using dashboard.Core;
using dashboard.Controllers.Resources;
using dashboard.Core.Models;

namespace dashboard.Controllers.Resources
{
    public class VehicleResource
    {
         public int Id { get; set; } 

       
        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }
        // public string Feature { get; set; }
        public bool IsRegistered { get; set; }

        
        public ContactResource Contact { get; set; }    

        // [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdate { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }

        public VehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}