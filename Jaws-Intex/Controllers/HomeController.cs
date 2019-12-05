using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Jaws_Intex.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Quote()
        {
            return View();
        }

        public ActionResult QuoteSuccess()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.LoginMessage = "";

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();

            if (string.Equals(username, "admin") && (string.Equals(password, "admin")))
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe);

                ViewBag.LoginBanner = "test";
                //ViewBag.LoginBanner = "<div class='alert alert-success' role='alert'>" +
                //    "<button type='button' class='close' dat-dismiss='alert'>" +
                //    "x" +
                //    "</button>" +
                //    "You have successfully logged in!" +
                //    "</div>";

                return RedirectToAction("LoggedInIndex", "Home");

            }
            else
            {
                ViewBag.LoginMessage = "Incorrect username or password";
                return View();
            }
        }

        [Authorize]
        public ActionResult LoggedInIndex()
        {
            return View();
        }
    }
}