using MyChurch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyChurch.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            var membri = db.Members.ToList();
            ViewBag.Title = "Home Page";

            return View(membri);
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        public ActionResult Visit()
        {
            ViewBag.Title = "Visit";
            return View();
        }
    }
}
