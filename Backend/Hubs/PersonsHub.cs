using Microsoft.AspNetCore.SignalR;
using Models;
using System.Threading.Tasks;

namespace Backend.Hubs
{
    public class PersonsHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }

        [HubMethodName("PersonMoved")]
        public void PersonMoved(double latitude, double longitude)
        {
            Clients.Others.SendCoreAsync("newPersonConnected", new[] {new PersoneMovedModel
            {
                ConnectionId = Context.ConnectionId,
                Latitude = latitude,
                Longitude = longitude
            }});
        }
    }
}
