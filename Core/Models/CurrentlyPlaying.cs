namespace dashboard.Core.Models
{
    public class CurrentlyPlaying : BaseModel
    {
        
        public long? timestamp { get; set; }
        public long? progress_ms { get; set; }
        public bool? is_playing { get; set; }
        public Item item   { get; set; }

        public CurrentlyPlaying()
        {
            item = null;
        }

    }
}
