using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class MonitorListController : BaseController
    {
        //
        // GET: /MonitorList/

        public ActionResult Index()
        {
            pubViewBag("027", "027", "");
            return View();
        }


    }
}
