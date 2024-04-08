using Application.MessageServices;
using MessengerServer.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessengerServer.Controllers
{
    [Route("api/messages")]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        public MessageController(IMessageService messageService) 
        {
            this.messageService = messageService;
        }

        [Authorize]
        [HttpGet, Route("{userLogin}")]
        public IResult GetUserMessages(string userLogin)
        {
            return Results.Ok(messageService.GetAllMessagesBy(userLogin).ToMessageDtoList());
        }

    }
}
