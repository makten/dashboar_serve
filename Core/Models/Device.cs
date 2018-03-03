namespace dashboard.Core.Models
{
    public class Device
    {
        public string id { get; set; }
        public bool is_active { get; set; }
        public bool is_restricted { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int volumn_percent { get; set; }
    }
}