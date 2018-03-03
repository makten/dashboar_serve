namespace dashboard.Core.Models
{
    public class Player
    {
        public long timestamp { get; set; }
        public long progress_ms { get; set; }
        public bool is_playing { get; set; }
        public bool shuffle_state { get; set; }
        public string repeat_state { get; set; }
        public Device device { get; set; }
        public SpotifyContext context { get; set; }
    }
}