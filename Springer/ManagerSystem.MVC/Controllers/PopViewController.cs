using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class PopViewController : Controller
    {

        public ActionResult Index()
        {
            string dbtype = Request.Params["dbtype"];//数据类型
            string dbid=Request.Params["dbid"];//数据主键
            return View();
        }

    }
}
