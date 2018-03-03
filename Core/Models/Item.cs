using dashboardp.Core.Models;

namespace dashboard.Core.Models
{
    public class Item : BaseModel
    {
        public string href { get; set; }
        public int duration_ms { get; set; }
        public bool Explicit { get; set; }
        public string  Name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public Album album { get; set; }
        public Artist[] artists { get; set; }

    }
}