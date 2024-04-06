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
            await Task.Run(() => messageService.AddMessage(message.ToMessage(userService.GetAll(), roleService.GetRoles())));
            await Clients.All.SendAsync("RecieveMessage", message);
        }

        public async Task EditMessage(MessageDto message)
        {
            await Task.Run(() => messageService.EditMessage(message.ToMessage(userService.GetAll(), roleService.GetRoles())));
            await Clients.All.SendAsync("EditMessage", message);
        }
    }
}
