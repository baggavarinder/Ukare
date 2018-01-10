using Core.Notifications;
using Core.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ukare.Filter.Authorization;
using Ukare.Service;
using Ukare.Service.Dashboard;
using Ukare.Service.User;

namespace Ukare.Controllers
{
    public class HomeController : Controller
    {
        IUserService _userService;
        IDashboardService _dashboardService;
        public HomeController(IUserService userService, IDashboardService dashboardService)
        {
            
            this._userService = userService;
            this._dashboardService = dashboardService;
        }

        public ActionResult UnderConstruction()
        {
            return View();
        }

        public ActionResult Index()
        {
            //_dashboardService.SaveVisitorInfo();   //called to save the visitor info
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            Session["UserId"] = null;
            Session["RoleId"] = null;
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "True" ? true : false);

           if (IsCaptchaValid)
            {
                if (ModelState.IsValid)
                {
                    int id = _userService.LoginUser(model);
                    if (id > 0)
                    {
                        Session["UserId"] = id.ToString();
                        Session["RoleId"] = model.RoleId.ToString();

                        if (model.RoleId != 4)
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "AdminArea" });
                        }
                        else { return RedirectToAction("Verification"); }

                    }
                    else if (id == -1)
                    {
                        ViewBag.LoginError = "Your account is not verified!";
                    }
                    else
                    {

                        ViewBag.LoginError = "Invalid Username/password!";
                    }
                }

                return View();
            }
           else {
                ViewBag.LoginError = "Invalid captcha!";
                return View();
            }
            
        }

        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            Session["UserEmailId"] = null;
            Session["RoleId"] = null;
            return RedirectToAction("Index");
        }


        public ActionResult Registration()
        {
            ModelState.Clear();
            ViewBag.Countries = _userService.GetCountries();
            ViewBag.Specialities = _userService.GetSpecialities();
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserModel model)
        {
            Session["UserId"] = null;
            if (ModelState.IsValid)
            {

                //string status = _userService.CreateUser(model);
                var result = JsonConvert.DeserializeObject<Response>(_userService.CreateUser(model));
                if (result.Status.Equals("200", StringComparison.CurrentCultureIgnoreCase))
                {
                   // Session["UserId"] = null;                 //null, because now we are not receiving id
                    //Session["RoleId"] = null;                 //static bind
                    Session["UserId"] = result.UserId;
                    Session["RoleId"] = result.RoleId;
                    //  TempData["Message"] = "Your account has been created, please verify by clicking the link sent to your email address.";
                    // return RedirectToAction("WelcomeUser");
                    return RedirectToAction("Verification"); 
                }
                else if (result.Status.Equals("exist", StringComparison.CurrentCultureIgnoreCase))
                {
                    ViewBag.ErrorMessage = "Email id already exist.";
                }
                //else if ( status.Equals("mail failed", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    ViewBag.ErrorMessage = "Something went wrong, Please try again.";
                //}
                else
                {
                    ViewBag.ErrorMessage = "";
                }
            }
            ViewBag.Countries = _userService.GetCountries();
            ViewBag.Specialities = _userService.GetSpecialities();

            return View();
        }

        public ActionResult forgot_password()
        {
            return View();
        }

        [HttpPost]
        public ActionResult forgot_password(UserLoginModel model)
        {
            if (_userService.ForgotPassword(model))
            {
                TempData["Message"] = "Email containing password has been sent to your email Id";
                RedirectToAction("WelcomeUser");
            }
            else {
                ViewBag.LoginError = "User doesn't exist!";
            }
            return View();
        }

        public ActionResult upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult upload(FormCollection form)
        {
            return View();
        }

        public ActionResult Verification()
        {
            if (Session["UserId"] != null) { return View(); }
            else { return RedirectToAction("Index"); }
        }

        [HttpPost]
        public ActionResult Verification(FormCollection form)
        {
            return RedirectToAction("Index");
        }

        public ActionResult NoPermission()
        {
            return View();
        }

        public ActionResult ActivateUser(int id, string code) {
            if (_userService.VerifyUser(id, code))
            {
                TempData["Message"] = "Your account has been verified, login to continue.";
            }
            else {
                TempData["Message"] = "Your account is not verified";
            }
            return RedirectToAction("WelcomeUser");
        }

        public ActionResult WelcomeUser() {
            ViewBag.Message = TempData["Message"];
            return View();
        }
        
    }

    public class Response
    {
        public string Status
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }

        public string RoleId
        {
            get;
            set;
        }
    }
    public class ReCaptchaClass
    {
        public static string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = WebConfigurationManager.AppSettings["captchaSecretKey"];

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

            PrivateKey = string.Empty;
            GoogleReply = null;

            return captchaResponse.Success;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }
}
