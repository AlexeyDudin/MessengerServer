using Application.RoleServices;
using Application.UserServices.UserService;
using Domain.Foundation;
using Domain.Models;

namespace Application.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        public MessageService(IUnitOfWork unitOfWork, IUserService userService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }
        public Message DeleteMessage(Message message)
        {
            var messageInDb = unitOfWork.MessageRepository.Where(m => m.UniqueId == message.UniqueId).FirstOrDefault();
            if (messageInDb != null)
            {
                unitOfWork.MessageRepository.Delete(messageInDb);
                unitOfWork.Commit();
            }
            return message;
        }

        public Message EditMessage(Message message)
        {
            var messageInDb = unitOfWork.MessageRepository.Where(m => m.UniqueId == message.UniqueId).FirstOrDefault();
            if (messageInDb != null) 
            {
                messageInDb.ChangeValues(message);
                unitOfWork.Commit();
            }
            return message;
        }

        public List<Message> GetAllMessagesBy(string userLogin)
        {
            var roles = userService.GetRolesBy(userLogin);
            var result = unitOfWork.MessageRepository.Where(m => m.From.Login == userLogin ||
                                                                 m.ToUser.Login == userLogin ||
                                                                 roles.Contains(m.ToGroup));
            return result;
        }

        public Message AddMessage(Message message)
        {
            unitOfWork.MessageRepository.Add(message);
            unitOfWork.Commit();
            return message;
        }

    }
}
