using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using PublicClassLibrary;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using PublicClassLibrary.ThirdDockService;
using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ManagerSystemClassLibrary.SDECLS;
using OAModel;
using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;

namespace ManagerSystem.MVC.Controllers
{
    public class InterfaceController : BaseController
    {
        //
        // GET: /Interface/

        public ActionResult SystemRedirect()
        {
            SystemCls.ClearLoginState();
            string uid = Request.Params["uid"];
            if (string.IsNullOrEmpty(uid))
                ViewBag.logined = "自动登录失败";
            else
            {
                T_SYSSEC_IPSUSER_Model m = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = uid });

                CookieModel cookieM = new CookieModel();
                if (m != null)
                {
                    if (string.IsNullOrEmpty(m.USERID))
                        ViewBag.logined = "自动登录失败";
                    else
                    {
                        cookieM.UID = m.USERID;
                        cookieM.userName = m.LOGINUSERNAME;
                        cookieM.trueName = m.USERNAME;
                        cookieM.SaveType = "true";
                        SystemCls.SaveLoginState(cookieM);
                        ViewBag.logined = "<script language=\"javascript\">window.location.href = '" + ConfigCls.getLoginRedirectUrl(m.USERID) + "';</script>";
                    }
                }
                else
                    ViewBag.logined = "自动登录失败";
            }
            return View();
        }

    }
}
