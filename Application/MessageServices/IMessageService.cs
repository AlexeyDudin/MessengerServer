﻿using Domain.Models;

namespace Application.MessageServices
{
    public interface IMessageService
    {
        Message AddMessage(Message message);
        Message EditMessage(Message message);
        Message DeleteMessage(Message message);
        List<Message> GetAllMessagesBy(string userName);
    }
}
