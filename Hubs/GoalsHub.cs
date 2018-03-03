using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Controllers.Resources;
using Microsoft.AspNetCore.SignalR;

namespace dashboard.Hubs
{
    public class GoalsHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("UpdateGoals", message);
        }
    }
}
