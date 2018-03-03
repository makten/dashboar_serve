namespace dashboard.Core.Models
{
    public class SpotifyImage : BaseModel
    {
        public int? height { get; set; }
        public string url { get; set; } = "none";
        public int? width { get; set; }
    }
}