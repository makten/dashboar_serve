using System;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.Extensions.Options;

namespace dashboard.Persistence
{
    public class SpotifyRepository : ISpotifyRepository
    {
        private readonly SpotifyAuthFactory _spotifyAuth;
        private readonly SpotifySettings _settings;
        
    }
}
