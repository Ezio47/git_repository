using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class UavController : BaseController
    {
 
        public ActionResult Index()
        {
            pubViewBag("028", "028", "无人机");
            return View();
        }

    }
}
