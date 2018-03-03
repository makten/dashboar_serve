using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Controllers.Resources;
using Microsoft.AspNetCore.SignalR;

namespace dashboard.Hubs
{
    public class TeamEnvironmentHub : Hub
    {
        public Task Send(string environment)
        {
            return Clients.All.InvokeAsync("UpdateTeamEnvironment", environment);
        }
    }
}
