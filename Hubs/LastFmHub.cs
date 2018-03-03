using dashboard.Controllers.Resources;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dashboard.Hubs
{
    public class LastFmHub : Hub
    {
        public Task Send(TrackInfoResource trackInfo)
        {
            return Clients.All.InvokeAsync("UpdateTrack", trackInfo);
        }

    }
}
