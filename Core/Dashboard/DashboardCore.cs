using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dashboard
{
    public class DashboardCore
    {
        public int UserCount { get; set; }

        public IList<MessageCore> Messages { get; set; }

        public long DailyVisitCount { get; set; }
    }

    public class MessageCore
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MessageTypeId { get; set; }
        public int MessageFor { get; set; }

        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
