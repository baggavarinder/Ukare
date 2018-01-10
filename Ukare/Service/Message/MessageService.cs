using Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ukare.Service.Message
{
    public class MessageService : IMessageService
    {
        private IMessageManager _messageManager;

        public MessageService(IMessageManager messageManager)
        {
            this._messageManager = messageManager;
        }

        public MessageService() { }

        public int SaveMessage(MessageModel message) {

            CoreMessage msg = new CoreMessage();
            msg.Title = message.Title;
            msg.Description = message.Description;
            msg.MessageFor = message.MessageFor;

            int id = _messageManager.SaveMessage(msg);
            return id;
        }

        public List<MessageModel> GetMessages(int id) {

            var messageList = _messageManager.GetMessages(id);

            List<MessageModel> messages = messageList.Select(p => new MessageModel {
                Title = p.Title,
                Description = p.Description,
                SentTo = p.SentTo,
                Id = p.Id,
                CreatedDate = p.CreatedDate
            }).ToList();

            return messages;
        }

        public MessageModel GetMessageById(int id) {
            var message = _messageManager.GetMessageById(id);

            MessageModel msg = new MessageModel();
            msg.Id = message.Id;
            msg.Title = message.Title;
            msg.Description = message.Description;
            msg.MessageFor = message.MessageFor;
            msg.CreatedDate = message.CreatedDate;

            return msg;
        }
       
    }
}