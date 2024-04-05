using Application.MessageServices;
using Application.RoleServices;
using Application.UserServices.UserService;
using MessengerServer.Converters;
using MessengerServer.Models;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace MessengerServer.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public MessageHub(IMessageService messageService, IUserService userService, IRoleService roleService) 
        {
            this.messageService = messageService;
            this.userService = userService;
            this.roleService = roleService;
        }
        public async Task SendMessage(MessageDto message)
        {
            await Task.Run(() => messageService.SendMessage(message.ToMessage(userService.GetAll(), roleService.GetRoles())));
            await Clients.All.SendAsync("RecieveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            //userService.AuthorizeUser(Context.)
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
