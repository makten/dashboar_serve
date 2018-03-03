using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core.Models;
using dashboard.Hubs;
using last_fm_now_playing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace dashboard_app.Controllers
{
    [Produces("application/json")]
    [Route("api/FetchCurrentTrack")]
    public class FetchCurrentTrackController : Controller
    {
        private readonly LastFmSettings option;
        private readonly IMapper _mapper;
        private readonly IHubContext<LastFmHub> _lastFmHub;

        public FetchCurrentTrackController(IMapper mapper, IOptionsSnapshot<LastFmSettings> option, IHubContext<LastFmHub> lastFmHub)
        {
            this.option = option.Value;
            _mapper = mapper;
            _lastFmHub = lastFmHub;
        }

        public IActionResult Get()
        {
            var lastFm = new NowPlaying(option.ApiKey);

            foreach (var user in option.Users)
            {
                var currentTrack = lastFm.GetTrackInfo(user);

                if (currentTrack != null)
                    InvokeHub(_mapper.Map<TrackInfo, TrackInfoResource>(currentTrack));
                return Ok(currentTrack);
            }

            InvokeHub(_mapper.Map<TrackInfo, TrackInfoResource>(new TrackInfo()));

            return Ok();
        }

        private async void InvokeHub(TrackInfoResource currentTrack)

        {
            await _lastFmHub.Clients.All.InvokeAsync("UpdateTrack", currentTrack);
        }
    }
}