using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ukare.Service.Dashboard
{
    public class DashboardModel
    {
        public int UserCount { get; set; }

        public IList<Message> Messages { get; set; }

        public long DailyVisitCount { get; set; }
    }


    public class Message {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MessageTypeId { get; set; }
        public int MessageFor { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}