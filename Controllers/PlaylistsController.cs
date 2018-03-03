using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using dashboard.Core;
using dashboard.Core.Models;
using dashboard.Helpers;
using dashboard.Hubs;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.SignalR;

namespace dashboard.Controllers
{
    [Route("/api/player")]
    public class PlaylistsController : Controller
    {
        private readonly IHubContext<DashboardHub> _dashboardHub;
        public readonly IMapper Mapper;
        private readonly ISpotifyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PlaylistsController(IHubContext<DashboardHub> dashboardHub, IMapper mapper, ISpotifyRepository repository, IUnitOfWork unitOfWork)
        {
            _dashboardHub = dashboardHub;
            Mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Player()
        {
            await _dashboardHub.Clients.All.InvokeAsync("TestSend", "Hello From The Hub Server!!");

            return View("/");
        }

        [HttpGet("auth/{code}/spotifyuser")]
        public async Task<IActionResult> GetSpotifyUser(FromBodyAttribute authCode, string code)
        {
            
            var authToken = HttpHelper.Post("https://accounts.spotify.com/api/token", code);

            var userResponse = await SpotifyUser.GetCurrentUserProfile(authToken);

            await _dashboardHub.Clients.All.InvokeAsync("TestSend", "Hello From The Hub Server!!");

            return Ok(userResponse);
        }

        [HttpGet("auth/{code}/currentlyplaying")]
        public async Task<IActionResult> GetCurrentlyPlaying(string code)
        {
           
            var authToken = HttpHelper.Post("https://accounts.spotify.com/api/token", code);

            var userResponse = await SpotifyUser.GetCurrentUserProfile(authToken);
            var playingNow = await userResponse.GetCurrentlyPlaying(authToken);

            return Ok(playingNow);
        }

        [HttpGet("auth/{code}/player")]
        public async Task<IActionResult> GetPlayer(string code)
        {
           
            var authToken = HttpHelper.Post("https://accounts.spotify.com/api/token", code);

            var userResponse = await SpotifyUser.GetCurrentUserProfile(authToken);
            var playingNow = await userResponse.GetPlayer(authToken);

            return Ok(playingNow);
        }


        private string GetUri()
        {

            //StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize/?");
            //builder.Append("client_id=" + clientId);
            //builder.Append("&response_type=token");
            //builder.Append("&redirect_uri=" + redirect_url);
            //builder.Append("&scope=" + scope);
            //return builder.ToString();
            return string.Empty;
        }
    }
}