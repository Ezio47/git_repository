using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.BaseDT;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class AncientTreesController : BaseController
    {
        #region 面积单位
        private string dic113Name = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "113" }).ToList()[0].DICTNAME;
        #endregion

        #region 古树名木采集
        /// <summary>
        /// 古树名木采集
        /// </summary>
        /// <returns></returns>
        public ActionResult TreesInfoList()
        {
            pubViewBag("023003", "023003", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        #endregion

        #region 古树群采集
        /// <summary>
        /// 古树群采集
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeGroupInfoList()
        {
            pubViewBag("023004", "023004", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        #endregion
    }
}
