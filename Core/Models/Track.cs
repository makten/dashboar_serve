using dashboardp.Core.Models;
using Newtonsoft.Json;

namespace dashboard.Core.Models
{
    [JsonObject]
    public class Track
    {
        public Artist[] Artists { get; set; }

        public string[] AvailableMarkets { get; set; }

        public int DiscNumber { get; set; }

        public int DurationMs { get; set; }

        public bool Explicit { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public string PreviewUrl { get; set; }

        public int TrackNumber { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }

       
    }
}