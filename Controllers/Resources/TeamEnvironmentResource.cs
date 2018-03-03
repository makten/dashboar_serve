using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core.Models;

namespace dashboard.Controllers.Resources
{
    public class TeamEnvironmentResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtraDetails { get; set; }
        public int ClientGroupId { get; set; }
        public ClientGroup ClientGroup { get; set; }
    }
}