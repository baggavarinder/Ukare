using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ukare.Service.Message
{
    public interface IMessageService
    {
        int SaveMessage(MessageModel message);

        List<MessageModel> GetMessages(int id);

        MessageModel GetMessageById(int id);
    }
}