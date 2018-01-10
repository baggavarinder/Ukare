using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ukare.Service.Dashboard
{
    public interface IDashboardService
    {
        DashboardModel GetDashboardDetail();

        //bool SaveVisitorInfo();
    }
}