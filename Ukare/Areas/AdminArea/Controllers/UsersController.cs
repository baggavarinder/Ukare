using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ukare.Filter.Authorization;
using Ukare.Service;
using Ukare.Service.Dashboard;
using Ukare.Service.User;

namespace Ukare.Areas.AdminArea.Controllers
{
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService userService) {
            this._userService = userService;
        }


        // GET: AdminArea/Users
        [UserRoleAuthorization]
        public ActionResult Users()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        [HttpPost]
        public ActionResult AddNewUser(UserModel user)
        {

            var id = _userService.UpdateUser(user);
            return RedirectToAction("Users");
        }

        [UserRoleAuthorization]
        public ActionResult AddNewUser(int id)
        {
            ViewBag.Roles = _userService.GetRoles(id);
            var user = _userService.GetUserById(id);
            return View(user);
        }

        public bool Delete(int id)
        {
            _userService.DeleteUser(id);
            return true;
        }
    }
}