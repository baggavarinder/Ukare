using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public class CoreMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MessageTypeId { get; set; }
        public int? MessageFor { get; set; }

        public string SentTo { get; set; }

        public DateTime CreatedDate{get;set;}
    }
}
