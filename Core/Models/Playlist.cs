using System.Threading.Tasks;
using dashboard.Helpers;
using Newtonsoft.Json;

namespace dashboard.Core.Models
{
    public class Playlist : BaseModel
    {
       
        public bool collaborative { get; set; }

        public string description { get; set; }

        public string href { get; set; }

        public string id { get; set; }

        public string name { get; set; }

        public SpotifyUser owner { get; set; }
        public bool Public { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

        
        public static async Task<Playlist> GetUsersPlaylists(SpotifyUser user, AuthenticationToken token)
        {
            return await GetUsersPlaylists(user.id, token);
        }

      
        public static async Task<Playlist> GetUsersPlaylists(string userId, AuthenticationToken token)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/users/" + userId + "/playlists", token);
            return JsonConvert.DeserializeObject<Playlist>(json);
        }

       
        public static async Task<Playlist> GetPlaylist(SpotifyUser user, string playlistId, AuthenticationToken token)
        {
            return await GetPlaylist(user.id, playlistId, token);
        }

        
        public static async Task<Playlist> GetPlaylist(string userId, string playlistId, AuthenticationToken token)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/users/" + userId + "/playlists/" + playlistId,
                token);
            return JsonConvert.DeserializeObject<Playlist>(json);
        }


        public static async Task<CurrentlyPlaying> GetCurrentlyPlaying(AuthenticationToken token)
        {
            var httpResponse = await HttpHelper.Get("https://api.spotify.com/v1/me/player/currently-playing", token);

            return JsonConvert.DeserializeObject<CurrentlyPlaying>(httpResponse);
        }

        public static async Task UpdateUsersPlaylist(string userId, string playlistId, string name, bool isPublic,
            AuthenticationToken token)
        {
            dynamic newObject = new System.Dynamic.ExpandoObject();
            newObject.name = name;
            newObject.@public = isPublic;

            string jsonInput = JsonConvert.SerializeObject(newObject);
            var json = await HttpHelper.Put("https://api.spotify.com/v1/users/" + userId + "/playlists/" + playlistId,
                token, jsonInput);
        }

        
        public async Task UpdatePlaylist(string name, bool isPublic, AuthenticationToken token)
        {
            await UpdateUsersPlaylist(this.owner.id, this.id, name, isPublic, token);
        }

        public static async Task<Player> GetPlayer(AuthenticationToken token)
        {
            var httpResponse = await HttpHelper.Get("https://api.spotify.com/v1/me/player", token);

            return JsonConvert.DeserializeObject<Player>(httpResponse);
        }
    }
}