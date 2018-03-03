using System.Collections.Generic;
using dashboard.Core.Models;
using Newtonsoft.Json;

namespace dashboardp.Core.Models
{
    [JsonObject]
    public class Artist : BaseModel
    {
        public List<string> Genres { get; set; }
        public string HREF { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

        public Artist()
        {
            this.Genres = new List<string>();
            this.HREF = null;
            this.Id = null;
            this.Name = null;
            this.Popularity = 0;
            this.Type = null;
            this.Uri = null;
        }
    }
}