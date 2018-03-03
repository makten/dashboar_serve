using System.Threading.Tasks;
using dashboard.Controllers.Resources;
using Microsoft.AspNetCore.SignalR;

namespace dashboard.Hubs
{
    public class DashboardHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("PlayerUpdate", message);
        }
    }
}
