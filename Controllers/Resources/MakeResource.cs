using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dashboard.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        // Borrows Id and Name from KeyValuePairResource
        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}