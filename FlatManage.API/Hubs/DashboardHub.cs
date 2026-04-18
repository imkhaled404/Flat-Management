using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FlatManage.API.Hubs
{
    public class DashboardHub : Hub
    {
        public async Task SendDashboardUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveDashboardUpdate", message);
        }
    }
}
