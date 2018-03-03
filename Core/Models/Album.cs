using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dashboard.Helpers;
using dashboardp.Core.Models;
using Newtonsoft.Json;

namespace dashboard.Core.Models
{

    public class Album : BaseModel
    {
        public AlbumType AlbumType { get; set; }
        public List<Artist> Artists { get; set; }
        public List<string> AvailableMarkets { get; set; }
        public List<string> Genres { get; set; }
        public string HREF { get; set; }
        public string Id { get; set; }
        public List<SpotifyImage> Images { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDatePrecision { get; set; }
        public Track Tracks { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }


        public Album()
        {
            this.AlbumType = AlbumType.Album;
            this.Artists = new List<Artist>();
            this.AvailableMarkets = new List<string>();
            this.Genres = new List<string>();
            this.HREF = null;
            this.Id = null;
            this.Images = new List<SpotifyImage>();
            this.Name = null;
            this.Popularity = 0;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleaseDatePrecision = null;
            this.Tracks = null;
            this.Type = null;
            this.Uri = null;
        }


        public static async Task<Album> GetAlbum(string albumId)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/albums/" + albumId);
            return JsonConvert.DeserializeObject<Album>(json);
        }
    }


    public enum AlbumType
    {
        Album,
        Single,
        Compilation
    }
}