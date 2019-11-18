using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class HomeViewController : Controller
    {
        // GET: HomeView
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Calendor()
        {
            return View();
        }
    }
}