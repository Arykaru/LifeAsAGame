using Models;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Old.Hubs
{
    public class PersonsHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
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
