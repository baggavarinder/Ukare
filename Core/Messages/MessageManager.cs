using Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Messages
{
    public class MessageManager : IMessageManager
    {
        UKareEntities db = new UKareEntities();
        public int SaveMessage(CoreMessage message)
        {
            try
            {
                Message messages = new Message();
                messages.Description = message.Description;
                messages.Title = message.Title;
                messages.MessageTypeId = 4;
                messages.MessageFor = message.MessageFor;
                messages.CreatedDate = DateTime.Now;
                messages.CreatedBy = 6;   //admin user id

                db.Messages.Add(messages);
                db.SaveChanges();

                return messages.Id;
            }
            catch (DbEntityValidationException ex)
            {

                throw;
            }
            
        }

        public List<CoreMessage> GetMessages(int userid)
        {
            var roleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"]);

            List<CoreMessage> MessagesList = new List<CoreMessage>();

            if (roleId == 1)
            {
                MessagesList = db.Messages.Where(p => p.MessageTypeId == 4)
  .Select(p => new CoreMessage
  {
      Title = p.Title,
      Description = p.Description.Substring(0,20) + "...",
      SentTo = p.User.Name,
      Id = p.Id,
      CreatedDate = p.CreatedDate
  }).OrderByDescending(p=> p.CreatedDate).ToList();
            }
            else
            {
                MessagesList = db.Messages.Where(p => p.MessageTypeId == 4 && p.MessageFor == userid)
.Select(p => new CoreMessage
{
    Title = p.Title,
    Description = p.Description.Substring(0, 20) + "...",
    SentTo = p.User.Name,
    Id = p.Id,
    CreatedDate = p.CreatedDate
}).OrderByDescending(p => p.CreatedDate).ToList();
            }



            return MessagesList;
        }

        public CoreMessage GetMessageById(int id)
        {
            var message = db.Messages.Where(p => p.Id == id).FirstOrDefault();

            CoreMessage msg = new CoreMessage();
            msg.Id = message.Id;
            msg.Title = message.Title;
            msg.Description = message.Description;
            msg.MessageFor = message.MessageFor;


            return msg;
        }
    }
}
