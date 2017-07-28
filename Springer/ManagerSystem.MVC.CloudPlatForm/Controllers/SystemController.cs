using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.CloudPlatForm.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.title = "登陆页";
            return View();        
        }
        
    }
}