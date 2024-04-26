using MyChurch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyChurch.Controllers
{
    public class AuthController : Controller
    {
        DBContext db = new DBContext();

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user) 
        {
            var loggedUser = db.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if (loggedUser == null)
            {
                TempData["error"] = true;
                return RedirectToAction("Login");
            }

            FormsAuthentication.SetAuthCookie(loggedUser.IDUser.ToString(), true);
            Session["UserID"] = loggedUser.IDUser;
            Session["UserN"] = loggedUser.UserName;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            if (Session["UserN"] != null)
            {
                Session.Remove("UserN");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}