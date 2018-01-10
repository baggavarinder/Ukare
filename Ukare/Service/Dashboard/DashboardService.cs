using Core.Dashboard;
using Core.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ukare.Service.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private IDashboardManager _dashboardManager;
        public DashboardService(IDashboardManager dashboardManager)
        {
            this._dashboardManager = dashboardManager;
        }

        public DashboardModel GetDashboardDetail()
        {
            var dashboardData = _dashboardManager.GetDashboardDetail();
            DashboardModel dashboardCount = new DashboardModel();
            dashboardCount.UserCount = dashboardData.UserCount;
            dashboardCount.DailyVisitCount = dashboardData.DailyVisitCount;
            dashboardCount.Messages = dashboardData.Messages.Select(p => new Message
            {
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                MessageTypeId = p.MessageTypeId

            }).ToList();

            return dashboardCount;
        }

        //public static bool SaveVisitorInfo()
        //{
        //    bool status = _dashboardManager.SaveVisitorInfo();
        //    return status;
        //}
    }
}