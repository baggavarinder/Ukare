using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ukare.Filter.Authorization;
using Ukare.Service;
using Ukare.Service.Dashboard;
using Ukare.Service.Message;
using Ukare.Service.User;

namespace Ukare.Areas.AdminArea.Controllers
{
    public class DashboardController : Controller
    {
        IDashboardService _dashboardService;
        IUserService _userService;
        IMessageService _messageService;
        public DashboardController(IDashboardService dashboardService, IUserService userService, IMessageService messageService)
        {
            this._dashboardService = dashboardService;
            this._userService = userService;
            this._messageService = messageService;
        }

        // GET: AdminArea/Dashboard
        [UserRoleAuthorization]
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View(_dashboardService.GetDashboardDetail());
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        
    }
}
