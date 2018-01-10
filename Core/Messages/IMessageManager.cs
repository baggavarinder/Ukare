using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public interface IMessageManager
    {
        int SaveMessage(CoreMessage message);

        List<CoreMessage> GetMessages(int userid);

        CoreMessage GetMessageById(int id);
    }
}
