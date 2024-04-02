using Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace MessengerServer.Hubs
{
    public class UsersHub : Hub
    {
        public async Task Send(User user)
        {
            await this.Clients.All.SendAsync("UserAdded", user);
        }
    }
}
