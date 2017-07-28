using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLW.AH.Application.Interfance;

namespace TLW.Project.ThirdVideoServiceWebApi.Controllers
{
    public class HomeController : Controller
    {
        #region Identity
        private IJCFireApplicationService iJCFireApplicationService = null;
        public HomeController(IJCFireApplicationService jcfireService)
        {
            this.iJCFireApplicationService = jcfireService;
        }
        // [Dependency]
        //  public IJCFireApplicationService iJCFireApplicationService { get; set; } 
        #endregion Identity
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //var ss = iJCFireApplicationService.GetStr("测试数据");
            // var user = iJCFireApplicationService.GetFireInfoByJCFID("93");
          //  var ss = iJCFireApplicationService.GetSysOrgByOrgNOData("532503000");
            return View();
        }

        public ActionResult TestIndex()
        {
            return View();
        }
    }
}
