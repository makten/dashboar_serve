using System.Collections.Generic;
using System.Threading.Tasks;
using dashboard.Helpers;
using Newtonsoft.Json;


namespace dashboard.Core.Models
{
    public class SpotifyUser : BaseModel
    {
        public string Country { get; set; }
        public string Display_Name { get; set; }
        public string email { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<SpotifyImage> images { get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

        public SpotifyUser()
        {
            images = new List<SpotifyImage>();
        }


        public async static Task<SpotifyUser> GetCurrentUserProfile(AuthenticationToken token)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/me", token);

            return JsonConvert.DeserializeObject<SpotifyUser>(json);
        }


        public async Task<Playlist> GetPlaylists(AuthenticationToken token)
        {
            return await Playlist.GetUsersPlaylists(this.id, token);
        }


        public async Task<CurrentlyPlaying> GetCurrentlyPlaying(AuthenticationToken token)
        {
            return await Playlist.GetCurrentlyPlaying(token);
        }


        public async Task SaveTracks(List<string> trackIds, AuthenticationToken token)
        {
            string tracksUri = CreateCommaSeperatedList(trackIds);
            var json = await HttpHelper.Put("https://api.spotify.com/v1/me/tracks?ids=" + tracksUri, token);
        }


        public async Task DeleteTracks(List<string> trackIds, AuthenticationToken token)
        {
            string tracksUri = CreateCommaSeperatedList(trackIds);
            var json = await HttpHelper.Delete("https://api.spotify.com/v1/me/tracks?ids=" + tracksUri, token);
        }

        public async Task<object> GetPlayer(AuthenticationToken token)
        {
            return await Playlist.GetPlayer(token);
        }
    }
}