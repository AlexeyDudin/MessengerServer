using Application.UserServices.UserService;
using Domain.Models;
using MessengerServer.Converters;
using MessengerServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace MessengerServer.Hubs
{
    public class UserHub : Hub
    {
        private readonly IUserService userService;
        public UserHub(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task UserOnline(string login)
        {
            var userInDb = userService.SetUserState(login, Context.ConnectionId, UserState.Online);
            if (userInDb != null)
            {
                await Clients.All.SendAsync("EditUser", userInDb.ToUserDto());
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = userService.GetUserByConnectionId(Context.ConnectionId);
            if (user != null)
            {
                await Task.Run(() => userService.SetUserState(user.Login, Context.ConnectionId, UserState.Offline));
                await Clients.All.SendAsync("EditUser", user.ToUserDto());
                await base.OnDisconnectedAsync(exception);
            }
        }
    }
}
