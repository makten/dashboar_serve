using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dashboard.Core.Models
{
    public class LastFmSettings
    {
        public string ApiKey { get; set; }
        public ICollection<string> Users { get; set; }

        public LastFmSettings()
        {
            Users = new List<string>();
        }
    }
}
