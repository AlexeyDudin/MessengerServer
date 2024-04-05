using Domain.Models;
using MessengerServer.Models;

namespace MessengerServer.Converters
{
    public static class MessageConverter
    {
        public static MessageDto ToMessageDto(this Message message)
        {
            var messageDto = new MessageDto()
            {
                Message = message.Text,
                TimeStamp = message.TimeStamp,
                From = message.From.Login,
                UniqueId = Guid.Parse(message.UniqueId)
            };
            if (message.ToUser != null)
            {
                messageDto.ToUser = message.ToUser.Login;
            }
            if (message.ToGroup != null)
            {
                messageDto.ToGroup = message.ToGroup.Name;
            }
            return messageDto;
        }

        public static Message ToMessage(this MessageDto messageDto, List<User> userList, List<Role> roleList)
        {
            var message = new Message()
            {
                Text = messageDto.Message,
                TimeStamp = messageDto.TimeStamp,
                UniqueId = messageDto.UniqueId.ToString()
            };
            var fromUser = userList.Where(u => u.Login == messageDto.From).FirstOrDefault();
            if (fromUser != null)
            {
                message.From = fromUser;
            }
            if (!string.IsNullOrEmpty(messageDto.ToUser))
            {
                message.ToUser = userList.Where(u => u.Login == messageDto.ToUser).FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(messageDto.ToGroup))
            {
                message.ToGroup = roleList.Where(r => r.Name == messageDto.ToGroup).FirstOrDefault();
            }
            return message;
        }

        public static List<MessageDto> ToMessageDtoList(this List<Message> messages)
        {
            List<MessageDto> messageDtos = new();
            foreach (var message in messages)
            {
                messageDtos.Add(message.ToMessageDto());
            }
            return messageDtos;
        }
    }
}
