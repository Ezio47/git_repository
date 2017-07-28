using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using PublicClassLibrary;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            pubViewBag("020002", "020002", "二维护林员");
            var CenterX = Request.Params["CenterX"];
            var CenterY = Request.Params["CenterY"];
            var CenterZ = Request.Params["CenterZ"];
            if (string.IsNullOrEmpty(CenterX) == false && string.IsNullOrEmpty(CenterY) == false)
            {
                double[] arr = ClsPositionTrans.GpsTransform(double.Parse(CenterY), double.Parse(CenterX), "1");
                CenterX = arr[1].ToString();
                CenterY = arr[0].ToString();
            }
            else
            {
                CenterX = "103.354065";
                CenterY = "23.367718";
            }
            if (string.IsNullOrEmpty(CenterZ) == false)
            {
                if (Convert.ToDouble(CenterZ) >= 571000)
                {
                    CenterZ = "8";
                }
                else if (Convert.ToDouble(CenterZ) >= 321000 && Convert.ToDouble(CenterZ) < 571000)
                {
                    CenterZ = "9";
                }
                else if (Convert.ToDouble(CenterZ) >= 138000 && Convert.ToDouble(CenterZ) < 321000)
                {
                    CenterZ = "10";
                }
                else if (Convert.ToDouble(CenterZ) >= 65000 && Convert.ToDouble(CenterZ) < 138000)
                {
                    CenterZ = "11";
                }
                else if (Convert.ToDouble(CenterZ) >= 37000 && Convert.ToDouble(CenterZ) < 65000)
                {
                    CenterZ = "12";
                }
                else if (Convert.ToDouble(CenterZ) >= 12600 && Convert.ToDouble(CenterZ) < 37000)
                {
                    CenterZ = "13";
                }
                else if (Convert.ToDouble(CenterZ) >= 5980 && Convert.ToDouble(CenterZ) < 12600)
                {
                    CenterZ = "14";
                }
                else if (Convert.ToDouble(CenterZ) >= 3700 && Convert.ToDouble(CenterZ) < 5980)
                {
                    CenterZ = "15";
                }
                else if (Convert.ToDouble(CenterZ) >= 2390 && Convert.ToDouble(CenterZ) < 3700)
                {
                    CenterZ = "16";
                }
                else if (Convert.ToDouble(CenterZ) >= 1096 && Convert.ToDouble(CenterZ) < 2390)
                {
                    CenterZ = "17";
                }
                else if (Convert.ToDouble(CenterZ) >= 635 && Convert.ToDouble(CenterZ) < 1096)
                {
                    CenterZ = "18";
                }
                else if (Convert.ToDouble(CenterZ) < 635)
                {
                    CenterZ = "19";
                }
            }
            else
            {
                CenterZ = "10";
            }
            ViewBag.CenterX = CenterX;
            ViewBag.CenterY = CenterY;
            ViewBag.CenterZ = CenterZ;
            ViewBag.rights = T_SYSSEC_RIGHTCls.getRightStrByUID(new T_SYSSEC_IPSUSER_SW { USERID = SystemCls.getUserID() });
            string method = Request.Params["Method"];
            string str = ViewBag.getPageMenuStr;
            ViewBag.getPageMenuStr = str.Replace("window.location.href='/Home/Index?Method=report&TID=1';", "getReport(&quot;1&quot;,&quot;0&quot;,&quot;火情&quot;)")
                .Replace("window.location.href='/Home/Index?Method=report&TID=2';", "getReport(&quot;2&quot;,&quot;0&quot;,&quot;病虫害&quot;)")
                .Replace("window.location.href='/Home/Index?Method=report&TID=3';", "getReport(&quot;3&quot;,&quot;0&quot;,&quot;盗砍盗伐&quot;)")
                .Replace("window.location.href='/Home/Index?Method=report&TID=4';", "getReport(&quot;4&quot;,&quot;0&quot;,&quot;安全隐患&quot;)")
                .Replace("window.location.href='/Home/Index?Method=collect&TID=1';", "getCollect(&quot;1&quot;,&quot;0&quot;,&quot;建筑物&quot;)")
                .Replace("window.location.href='/Home/Index?Method=collect&TID=2';", "getCollect(&quot;2&quot;,&quot;0&quot;,&quot;消防设施&quot;)")
                .Replace("window.location.href='/Home/Index?Method=collect&TID=3';", "getCollect(&quot;3&quot;,&quot;0&quot;,&quot;道路&quot;)")
                .Replace("window.location.href='/Home/Index?Method=collect&TID=4';", "getCollect(&quot;4&quot;,&quot;0&quot;,&quot;可燃物载量&quot;)")
                .Replace("window.location.href='/Home/Index';", "(getLonLat(&quot;&quot;))")
                .Replace("window.location.href='/Home/Index?Method=dm';", "(GetDmFun())")
                .Replace("window.location.href='/Home/Index?Method=hot';", "(getHot(&quot;0&quot;))")
                .Replace("window.location.href='/Home/Index?Method=alarm';", "(getAlarm(&quot;0&quot;))")
                .Replace("window.location.href='/Home/Index?Method=dl';", "(GetElecFun())");

            ViewBag.getPageMenuStr = ViewBag.getPageMenuStr;
            switch (method)
            {
                case "alarm":
                    ViewBag.loadFunc = "getAlarm(\"0\")";//一键报警
                    break;
                case "hot":
                    ViewBag.loadFunc = "getHot(\"0\")";//热点
                    break;
                case "report"://数据上报
                    var reportsw = new T_SYS_DICTSW();
                    reportsw.DICTTYPEID = "5";
                    reportsw.DICTVALUE = Request.Params["TID"];
                    var reportmodel = T_SYS_DICTCls.getModel(reportsw);
                    ViewBag.loadFunc = "getReport(\"" + Request.Params["TID"] + "\",\"0\",\"" + reportmodel.DICTNAME + "\")";//1 火情 2 病虫害 3 盗砍盗伐  //第一个参数是数据类型 第二个参数是处理状态
                    break;
                case "collect"://数据采集
                    var sw = new T_SYS_DICTSW();
                    sw.DICTTYPEID = "4";
                    sw.DICTVALUE = Request.Params["TID"];
                    var model = T_SYS_DICTCls.getModel(sw);
                    ViewBag.loadFunc = "getCollect(\"" + Request.Params["TID"] + "\",\"0\",\"" + model.DICTNAME + "\")";//第一个参数是数据类型 第二个参数是处理状态 第三个参数名字
                    break;

                default:
                    ViewBag.loadFunc = "getLonLat(\"\")";
                    break;
            }
            string TID = Request.Params["TID"];
            if (method == "report")//上报
            {
                var sw = new T_SYS_DICTSW();
                sw.DICTTYPEID = "5";
                sw.DICTVALUE = TID;
                var model = T_SYS_DICTCls.getModel(sw);
                if (TID.Length == 1)
                    pubViewBag("00200" + TID, "00200" + TID, model.DICTNAME);
                else if (TID.Length == 2)
                    pubViewBag("0020" + TID, "0020" + TID, model.DICTNAME);
                else
                    pubViewBag("002" + TID, "002" + TID, model.DICTNAME);
            }
            else if (method == "collect")//采集
            {
                var sw = new T_SYS_DICTSW();
                sw.DICTTYPEID = "4";
                sw.DICTVALUE = TID;
                var model = T_SYS_DICTCls.getModel(sw);
                if (TID.Length == 1)
                    pubViewBag("00300" + TID, "00300" + TID, model.DICTNAME);
                else if (TID.Length == 2)
                    pubViewBag("0030" + TID, "0030" + TID, model.DICTNAME);
                else
                    pubViewBag("003" + TID, "003" + TID, model.DICTNAME);
            }
            else if (method == "dm")
                pubViewBag("001002", "001002", "点名管理");
            else if (method == "dl")
                pubViewBag("001005", "001005", "电量查询");
            else if (method == "hot")
                pubViewBag("001003", "001003", "热点追踪");
            else if (method == "alarm")
                pubViewBag("001004", "001004", "报警管理");
            else
                pubViewBag("001001", "001001", "巡查监控");
            ViewBag.Method = Request.Params["Method"];       //类别方法 用于GIS页面
            ViewBag.TID = Request.Params["TID"];             //类别ID
            return View();
        }

        /// <summary>
        /// 视频监控页面
        /// </summary>
        /// <returns></returns>
        public ActionResult playvideo()
        {
            pubViewBag("020002", "020002", "");
            return View();
        }

        /// <summary>
        /// 二维整合页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Total2DIndex()
        {
            pubViewBag("030", "030", "二维护林员");
            var CenterX = Request.Params["CenterX"];
            var CenterY = Request.Params["CenterY"];
            var CenterZ = Request.Params["CenterZ"];
            if (string.IsNullOrEmpty(CenterX) == false && string.IsNullOrEmpty(CenterY) == false)
            {
                double[] arr = ClsPositionTrans.GpsTransform(double.Parse(CenterY), double.Parse(CenterX), "1");
                CenterX = arr[1].ToString();
                CenterY = arr[0].ToString();
            }
            else
            {
                //CenterX = "103.354065";
                //CenterY = "23.367718";
                CenterX = ConfigCls.getConfigValue("Longitude");
                CenterY = ConfigCls.getConfigValue("Latitude");
            }
            if (string.IsNullOrEmpty(CenterZ) == false)
            {
                if (Convert.ToDouble(CenterZ) >= 571000)
                {
                    CenterZ = "8";
                }
                else if (Convert.ToDouble(CenterZ) >= 321000 && Convert.ToDouble(CenterZ) < 571000)
                {
                    CenterZ = "9";
                }
                else if (Convert.ToDouble(CenterZ) >= 138000 && Convert.ToDouble(CenterZ) < 321000)
                {
                    CenterZ = "10";
                }
                else if (Convert.ToDouble(CenterZ) >= 65000 && Convert.ToDouble(CenterZ) < 138000)
                {
                    CenterZ = "11";
                }
                else if (Convert.ToDouble(CenterZ) >= 37000 && Convert.ToDouble(CenterZ) < 65000)
                {
                    CenterZ = "12";
                }
                else if (Convert.ToDouble(CenterZ) >= 12600 && Convert.ToDouble(CenterZ) < 37000)
                {
                    CenterZ = "13";
                }
                else if (Convert.ToDouble(CenterZ) >= 5980 && Convert.ToDouble(CenterZ) < 12600)
                {
                    CenterZ = "14";
                }
                else if (Convert.ToDouble(CenterZ) >= 3700 && Convert.ToDouble(CenterZ) < 5980)
                {
                    CenterZ = "15";
                }
                else if (Convert.ToDouble(CenterZ) >= 2390 && Convert.ToDouble(CenterZ) < 3700)
                {
                    CenterZ = "16";
                }
                else if (Convert.ToDouble(CenterZ) >= 1096 && Convert.ToDouble(CenterZ) < 2390)
                {
                    CenterZ = "17";
                }
                else if (Convert.ToDouble(CenterZ) >= 635 && Convert.ToDouble(CenterZ) < 1096)
                {
                    CenterZ = "18";
                }
                else if (Convert.ToDouble(CenterZ) < 635)
                {
                    CenterZ = "19";
                }
            }
            else
            {
                CenterZ = "10";
            }
            ViewBag.CenterX = CenterX;
            ViewBag.CenterY = CenterY;
            ViewBag.CenterZ = CenterZ;
            ViewBag.rights = T_SYSSEC_RIGHTCls.getRightStrByUID(new T_SYSSEC_IPSUSER_SW { USERID = SystemCls.getUserID() });
            return View();
        }

        /// <summary>
        /// 获取Tree树
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeGet()
        {
            string str = Request.Params["phonehname"];
            string ID = Request.Params["id"];
            string result = T_IPSFR_USERCls.getTree(new T_IPSFR_USER_SW { PhoneHname = str }, ID);
            return Content(result, "application/json");
        }

        /// <summary>
        /// 获取车辆Tree树
        /// </summary>
        /// <returns></returns>
        public ActionResult CarTreeGet()
        {
            string idorgno = Request.Params["id"];
            string orgno = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            string result = DC_CARCls.GetJsonStrCar(orgno, idorgno);
            return Content(result, "application/json");
        }

        /// <summary>
        /// 三维整合页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Total3DIndex()
        {
            var type = Request.Params["type"];
            if (type == "0")
            {
                pubViewBag("020003", "020003", "三维护林员");
            }
            else if (type == "1")
            {
                pubViewBag("012006", "012006", "应急处置");
            }
            else if (type == "2")
            {
                pubViewBag("018003", "018003", "三维首页");
            }
            else if (type == "3")
            {
                pubViewBag("017001", "017001", "");
            }
            
            CookieModel cookieInfo1 = SystemCls.getCookieInfo();
            ViewBag.LAYERNAME = T_SYS_LAYERCls.getLayerNameStr(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.DEFAULTCH = T_SYS_LAYERCls.getLayerDEFAULTCHStr(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.LAYERID = T_SYS_LAYERCls.getLayerFireLAYERID(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.Fire = T_SYS_LAYERCls.getTreeFireQuery(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.AllNAME = T_SYS_LAYERCls.getLayerAllNAME();
            var xcenter = Request.Params["xcenter"];
            var ycenter = Request.Params["ycenter"];
            var scale = Request.Params["scale"];
            if (string.IsNullOrEmpty(xcenter) == false && string.IsNullOrEmpty(ycenter) == false)
            {
                double[] arr = ClsPositionTrans.GpsTransform(double.Parse(ycenter), double.Parse(xcenter), "2");
                xcenter = arr[1].ToString();
                ycenter = arr[0].ToString();
            }
            if (string.IsNullOrEmpty(scale) == false)
            {
                if (Convert.ToDouble(scale) >= 2311162)
                {
                    scale = "571830";
                }
                else if (Convert.ToDouble(scale) >= 1155581 && Convert.ToDouble(scale) < 2311162)
                {
                    scale = "321000";
                }
                else if (Convert.ToDouble(scale) >= 557790 && Convert.ToDouble(scale) < 1155581)
                {
                    scale = "138440";
                }
                else if (Convert.ToDouble(scale) >= 288895 && Convert.ToDouble(scale) < 557790)
                {
                    scale = "65890";
                }
                else if (Convert.ToDouble(scale) >= 144447 && Convert.ToDouble(scale) < 288895)
                {
                    scale = "37210";
                }
                else if (Convert.ToDouble(scale) >= 72223 && Convert.ToDouble(scale) < 144447)
                {
                    scale = "12660";
                }
                else if (Convert.ToDouble(scale) >= 36111 && Convert.ToDouble(scale) < 72223)
                {
                    scale = "5981";
                }
                else if (Convert.ToDouble(scale) >= 18055 && Convert.ToDouble(scale) < 36111)
                {
                    scale = "3789";
                }
                else if (Convert.ToDouble(scale) >= 9027 && Convert.ToDouble(scale) < 18055)
                {
                    scale = "2390";
                }
                else if (Convert.ToDouble(scale) >= 4513 && Convert.ToDouble(scale) < 9027)
                {
                    scale = "1096";
                }
                else if (Convert.ToDouble(scale) >= 2256 && Convert.ToDouble(scale) < 4513)
                {
                    scale = "635";
                }
                else if (Convert.ToDouble(scale) < 1130)
                {
                    scale = "300";
                }
            }
            //从数据库里获取三维图层树
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            string result = T_SYS_LAYERCls.getTree(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });
            ViewBag.TreeData = result;
            ViewBag.xcenter = xcenter;
            ViewBag.ycenter = ycenter;
            ViewBag.scale = scale;
            string strmenue = ViewBag.getPageMenuStr;
            ViewBag.getPageMenuStr = strmenue.Replace("window.location.href='/Home/Total3Dindex?type=0'", "showHlyFun()").Replace("window.location.href='/Home/Total3Dindex?type=1'", "showYjczFun()").Replace("window.location.href='/Home/Total3Dindex?type=2';", "showSyzrFun()").Replace("window.location.href='/Home/Total3Dindex?type=3';", "showGylFun()");
            var reportMenuList = new List<MenuTypeModel>();//数据上报菜单
            var reportList = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { MENUCODE = "002", SYSFLAG = ConfigCls.getSystemFlag() }).FirstOrDefault();//数据上报
            if (reportList != null)
            {
                foreach (var item in reportList.subMenuModel)
                {
                    var reportmodel = new MenuTypeModel();
                    reportmodel.DICTTYPEID = "5";
                    reportmodel.LICLASS = item.LICLASS;
                    reportmodel.MENUNAME = item.MENUNAME;
                    reportmodel.DICTVALUE = item.MENUURL.Substring(item.MENUURL.Length - 1, 1);
                    reportMenuList.Add(reportmodel);
                }
            }
            ViewBag.reportlist = reportMenuList;//数据上报项目获取
            var collectList = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { MENUCODE = "003", SYSFLAG = ConfigCls.getSystemFlag() }).FirstOrDefault();//数据采集
            var collectMenuList = new List<MenuTypeModel>();//数据采集
            if (collectList != null)
            {
                foreach (var item in collectList.subMenuModel)
                {
                    var collectmodel = new MenuTypeModel();
                    collectmodel.DICTTYPEID = "4";
                    collectmodel.LICLASS = item.LICLASS;
                    collectmodel.MENUNAME = item.MENUNAME;
                    collectmodel.DICTVALUE = item.MENUURL.Substring(item.MENUURL.Length - 1, 1);
                    collectMenuList.Add(collectmodel);
                }
            }
            ViewBag.collectList = collectMenuList;//数据采集项目获取
            //var modelfirelist = GetCUrFireList();//当前火情
            return View();
        }

        #region Ajax
        /// <summary>
        /// 选择地图
        /// </summary>
        /// <returns></returns>
        public JsonResult SwitchMapAjax()
        {
            Message ms = null;
            string value = Request.Params["value"];
            if (string.IsNullOrEmpty(value))
            {
                ms = new Message(false, "选择地图数据缺失", "");
            }
            //<!--系统调用地图类型 0 系统自建ArcGis 1 Google地图 2 天地图 3Baidu地图-->
            //<add key="mapType" value="1" />
            //<!--是否经纬度偏移量转换 0 表示不需要 1表示需要-->
            //<add key="lonLatChange" value="1" />
            if (value == "1")//google
            {
                ConfigCom.UpdateAppSetting("mapType", value);
                ConfigCom.UpdateAppSetting("lonLatChange", "1");
            }
            else
            {
                ConfigCom.UpdateAppSetting("mapType", value);
                ConfigCom.UpdateAppSetting("lonLatChange", "0");
            }
            ms = new Message(true, "选择地图成功", "");
            return Json(ms);
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取当前火情
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EHCurFireMode> GetCUrFireList()
        {
            var result = new List<EHCurFireMode>();
            var jcfirelist = JC_FIRECls.GetListModel(new JC_FIRE_SW { BYORGNO = SystemCls.getCurUserOrgNo() }).Where(p => (p.ISOUTFIRE.Trim() != "1"));//监测火情信息
            if (jcfirelist.Any())
            {
                foreach (var item in jcfirelist)
                {
                    var firefkmodel = JC_FIRETICKLINGCls.GetModelList(new JC_FIRETICKLING_SW() { JCFID = item.JCFID, ISOUTFIRE = "0" }).Where(p => p.HOTTYPE == "1" || p.HOTTYPE == "6" || p.HOTTYPE == "10").FirstOrDefault();
                    if (firefkmodel != null)
                    {
                        var model = new EHCurFireMode();
                        model.ADDRESSS = item.ZQWZ;
                        model.FIRENAME = item.FIRENAME;
                        model.JCFID = item.JCFID;
                        model.JD = item.JD;
                        model.WD = item.WD;
                        model.ORGNO = item.BYORGNO;
                        var record = JC_FIRE_PROPCls.getModel(new JC_FIRE_PROP_SW { JCFID = item.JCFID });
                        if (record != null)
                        {
                            model.FIRELEVEL = record.FIRELEVEL;
                        }
                        result.Add(model);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取当前火情——优化后
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EHCurFireMode> GetCUrFireListYH()
        {
            var result = new List<EHCurFireMode>();
            var jcfirelist = JC_FIRECls.GetListModelYH(new JC_FIRE_SW { BYORGNO = SystemCls.getCurUserOrgNo() });//监测火情信息
            foreach (var item in jcfirelist)
            {
                var model = new EHCurFireMode();
                model.ADDRESSS = item.ZQWZ;
                model.FIRENAME = item.FIRENAME;
                model.JCFID = item.JCFID;
                model.JD = item.JD;
                model.WD = item.WD;
                model.ORGNO = item.BYORGNO;
                model.FIRELEVEL = item.FIRELEVEL;
                result.Add(model);
            }
            return result;
        }
        #endregion
        /// <summary>
        /// Total3D页面分开-护林员页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HLY3D()
        {
            var reportMenuList = new List<MenuTypeModel>();//数据上报菜单
            var reportList = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { MENUCODE = "002", SYSFLAG = ConfigCls.getSystemFlag() }).FirstOrDefault();//数据上报
            if (reportList != null)
            {
                foreach (var item in reportList.subMenuModel)
                {
                    var reportmodel = new MenuTypeModel();
                    reportmodel.DICTTYPEID = "5";
                    reportmodel.LICLASS = item.LICLASS;
                    reportmodel.MENUNAME = item.MENUNAME;
                    reportmodel.DICTVALUE = item.MENUURL.Substring(item.MENUURL.Length - 1, 1);
                    reportMenuList.Add(reportmodel);
                }
            }
            ViewBag.reportlist = reportMenuList;//数据上报项目获取
            var collectList = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { MENUCODE = "003", SYSFLAG = ConfigCls.getSystemFlag() }).FirstOrDefault();//数据采集
            var collectMenuList = new List<MenuTypeModel>();//数据采集
            if (collectList != null)
            {
                foreach (var item in collectList.subMenuModel)
                {
                    var collectmodel = new MenuTypeModel();
                    collectmodel.DICTTYPEID = "4";
                    collectmodel.LICLASS = item.LICLASS;
                    collectmodel.MENUNAME = item.MENUNAME;
                    collectmodel.DICTVALUE = item.MENUURL.Substring(item.MENUURL.Length - 1, 1);
                    collectMenuList.Add(collectmodel);
                }
            }
            ViewBag.collectList = collectMenuList;//数据采集项目获取
            return View();
        }
        /// <summary>
        /// Total3D页面分开-预警监测页面
        /// </summary>
        /// <returns></returns>
        public ActionResult YJJC3D()
        {
            return View();
        }
        /// <summary>
        /// Total3D页面分开-应急指挥页面
        /// </summary>
        /// <returns></returns>
        public ActionResult YJZH3D()
        {
            string userid = SystemCls.getUserID();
            ViewBag.Fire = T_SYS_LAYERCls.getTreeFireQuery(new T_SYS_LAYER_SW { USERID = userid });
            ViewBag.LAYERID = T_SYS_LAYERCls.getLayerFireLAYERID(new T_SYS_LAYER_SW { USERID = userid });
            var modelfirelist = GetCUrFireListYH();//当前火情
            return View(modelfirelist);
        }
        /// <summary>
        /// Total3D页面分开-公益林页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GYL3D()
        {
            return View();
        }
    }
}
