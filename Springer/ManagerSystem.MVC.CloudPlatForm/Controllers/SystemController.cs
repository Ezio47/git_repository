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
        

        public ActionResult NewLogin()
        {
            return View(); 
        }

        public ActionResult Desk()
        {
            return View(); 
        }

        /// <summary>
        /// 林业灾害应急平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ZHYJ()
        {
            return View();
        }

        /// <summary>
        /// 林业综合应用平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ZHYY()
        {
            return View();
        }

        /// <summary>
        /// 林业政务服务平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ZWFW()
        {
            return View();
        }

        /// <summary>
        /// 林业警务服务平台
        /// </summary>
        /// <returns></returns>
        public ActionResult JWFW()
        {
            return View();
        }

        /// <summary>
        /// 林业基础感知平台
        /// </summary>
        /// <returns></returns>
        public ActionResult JCGZ()
        {
            return View();
        }

        /// <summary>
        /// 林业基础支持平台
        /// </summary>
        /// <returns></returns>
        public ActionResult JCZC()
        {
            return View();
        }
    }
}