using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.CloudPlatForm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.title = "智慧林业云平台";
            return View();
        }

        public ActionResult NewIndex()
        {
            ViewBag.title = "智慧林业云平台";
            return View();
        }
    }
}