using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ukare.Service.Message;
using Ukare.Service.User;

namespace Ukare.Areas.AdminArea.Controllers
{
    public class MessageController : Controller
    {
        IMessageService _messageService;
        IUserService _userService;
        public MessageController(IMessageService messageService, IUserService userService)
        {
            this._messageService = messageService;
            this._userService = userService;
        }
        // GET: AdminArea/Message
        public ActionResult Index()
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var model = _messageService.GetMessages(userId);
            return View(model);
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetRecipients(string Prefix)
        {

            var userlist = _userService.GetUsersWithPrefix(Prefix);
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendMessage(MessageModel message)
        {
            if (ModelState.IsValid)
            {
                int id = _messageService.SaveMessage(message);
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult MessageDetail(int id) {

            var message = _messageService.GetMessageById(id);
            return View(message);
        }

    }
}