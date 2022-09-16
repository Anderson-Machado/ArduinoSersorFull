using Microsoft.AspNetCore.SignalR;
using RealTimeMeteorology.Model;
using System.Threading.Tasks;

namespace RealTimeMeteorology.HubConfig
{
    public class ChartHub: Hub
    {
        //public async Task SendMessage(string user, Arduino message)
        //=> await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
