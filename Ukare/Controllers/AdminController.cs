﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ukare.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }
        public ActionResult GroupUsers()
        {
            return View();
        }
        public ActionResult AddNewUser()
        {
            return View();
        }

    }
}
