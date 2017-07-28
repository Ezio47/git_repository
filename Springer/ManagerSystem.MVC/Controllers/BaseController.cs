using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ManagerSystem.MVC.HelpCom;
using System.Net;
using System.Configuration;

namespace ManagerSystem.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        #region 页面公用 pubViewBag(string PageCode, string RightCode, string PageTitle)
        /// <summary>
        /// 页面公用ViewBag
        /// </summary>
        /// <param name="PageCode">页面编码（菜单编码）</param>
        /// <param name="RightCode">权限编码 用于页面级权限验证</param>
        /// <param name="PageTitle">页面标题 标题为空自动获取页面标题及上级页面标题</param>
        public void pubViewBag(string PageCode, string RightCode, string PageTitle)
        {
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            ViewBag.getPageMenuStr = getPageMenuStr(PageCode);
            ViewBag.PageCode = PageCode;
            string curORGNo = SystemCls.getCurUserOrgNo();
            if (PublicCls.OrgIsShi(curORGNo))
                curORGNo = ConfigCls.getConfigValue("ProvincialCapital");//州府所在地行政区划编码

            //获取火险等级
            ViewBag.fireLevel = YJ_DANGERCLASSCls.getLevelByOrgNo(new YJ_DANGERCLASS_SW { BYORGNO = curORGNo });

            //获取滚动信息
            if (T_SYS_PARAMETERCls.getValueByFlag(new T_SYS_PARAMETER_SW { PARAMFLAG = "LoginInfo" }).ToString() == "0")
            {
                ViewBag.marqueeSysInfo = T_SYS_PARAMETERCls.getValueByFlag(new T_SYS_PARAMETER_SW { PARAMFLAG = "marqueeIndexInfo" }).ToString();
            }
            else
            {
                ViewBag.marqueeSysInfo = YJ_WEATHERCls.getWeather(new YJ_WEATHER_SW { BYORGNO = curORGNo });
            }

            ViewBag.PageLeftMenu = getPageLeftMenu(PageCode);//左侧菜单
            ViewBag.isPageRight = SystemCls.isRight(RightCode);//判断页面是否有权限 
            ViewBag.SystemName = ConfigCls.getSystemName();//系统名称
            ViewBag.noticeRefreshTimeInterval = ConfigCls.noticeRefreshTimeInterval();//菜单自动刷新间隔
            ViewBag.PageTitle = PageTitle;
            //如果页面标题为空，则自动通过页面编码从系统菜单表中获取菜单名称及上级菜单名称
            if (string.IsNullOrEmpty(PageTitle))
            {
                ViewBag.PageTitle = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = PageCode, SYSFLAG = ConfigCls.getSystemFlag() }).MENUNAME;
            }
            ViewBag.Title = ConfigCls.getSystemName() + "-" + ViewBag.PageTitle; //Title名称     
            ViewBag.trueName = cookieInfo.trueName;          //当前登录用户姓名
            ViewBag.depName = StateSwitch.GetOrgNameByOrgNO(SystemCls.getCurUserOrgNo()); //部门名称
            string systemFlag = ConfigCls.getSystemFlag();   //系统标识 如Springer
            ViewBag.T_UrlReferrer = Request.UrlReferrer;
        }


        #endregion



        #region 弃用

        #region 获取系统菜单并分模块下拉显示 getHeadMenuStr1(string menuCodeList)
        public string getHeadMenuStr1(string menuCodeList)
        {
            //string str=SystemCls.getT_SYS_DEFINEMENU(new T_SYS_MENU_SW { UID =SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() });;
            string loadFunc = Request.Params["loadFunc"];//调用方法
            string Method = Request.Params["Method"];//调用方法
            string TID = Request.Params["TID"];//调用方法
            string PageCode = Request.Params["PageCode"];//调用方法
            var result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() });
            StringBuilder sb = new StringBuilder();
            bool blnMp3 = false;
            sb.AppendFormat("<ul class='nav ace-nav'>");
            sb.AppendFormat("<li class='light-blue'>");
            sb.AppendFormat("<a href='{0}' class='dropdown-toggle'>", ConfigCls.getLoginRedirectUrl());
            sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", "fa fa-wrench", "color:#ff0000;");
            sb.AppendFormat("{0}", "返回首页");
            sb.AppendFormat("</a>");
            sb.AppendFormat("</li>");
            foreach (var v in result)
            {
                if (menuCodeList.Contains("," + v.MENUCODE + ","))
                {
                    sb.AppendFormat("<li class='light-blue'>");
                    sb.AppendFormat("<a data-toggle='dropdown' href='#' class='dropdown-toggle'>");
                    sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", v.MENUICO, v.LICLASS);
                    sb.AppendFormat("{0}", v.MENUNAME);
                    sb.AppendFormat("<span class='badge badge-important'>{0}</span>", v.showCount);
                    sb.AppendFormat("<i class='icon-caret-down'></i>");
                    sb.AppendFormat("</a>");
                    sb.AppendFormat("<ul class='user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close'>");
                    var subResult = v.subMenuModel;
                    foreach (var sv in subResult)
                    {
                        sb.AppendFormat("<li>");
                        string codelist = ",001,002,003,";
                        //string a = sv.MENUCODE.Substring(0, 3);
                        if (string.IsNullOrEmpty(loadFunc) == false && codelist.Contains("," + sv.MENUCODE.Substring(0, 3) + ",") && codelist.Contains("," + PageCode.Substring(0, 3) + ","))
                        {
                            string func = "";
                            if (sv.MENUCODE == "001002")
                                func = "GetDmFun()";//点名管理
                            else if (sv.MENUCODE == "001004")
                                func = "getAlarm(\"0\")";//报警管理
                            else if (sv.MENUCODE == "001005")
                                func = "GetElecFun()";//电量管理
                            else if (sv.MENUCODE == "001003")
                                func = "getHot(\"0\")";//热点管理
                            else if (sv.MENUCODE.Substring(0, 3) == "002")//上报
                                func = "getReport(\"" + sv.TID + "\",\"0\",\"" + sv.MENUNAME + "\")";
                            else if (sv.MENUCODE.Substring(0, 3) == "003")//采集
                                func = "getCollect(\"" + sv.TID + "\",\"0\",\"" + sv.MENUNAME + "\")";
                            else
                                func = "getLonLat(\"\")";

                            sb.AppendFormat("<a href='#' onclick='({0})'>", func);
                            sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", sv.MENUICO, sv.LICLASS);
                            sb.AppendFormat("{0}", sv.MENUNAME);
                            if (string.IsNullOrEmpty(sv.showCount) == false)
                                sb.AppendFormat("<span class='badge badge-important'>{0}</span>", sv.showCount);
                            sb.AppendFormat("</a>");
                        }
                        else
                        {
                            sb.AppendFormat("<a href='{0}'>", sv.MENUURL);
                            sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", sv.MENUICO, sv.LICLASS);
                            sb.AppendFormat("{0}", sv.MENUNAME);
                            if (string.IsNullOrEmpty(sv.showCount) == false)
                                sb.AppendFormat("<span class='badge badge-important'>{0}</span>", sv.showCount);
                            sb.AppendFormat("</a>");

                        }
                        sb.AppendFormat("</li>");
                        if (string.IsNullOrEmpty(sv.showCount) == false)
                            blnMp3 = true;
                    }
                    sb.AppendFormat("      <li class='divider'></li>");
                    sb.AppendFormat("</ul>");
                    sb.AppendFormat("</li>");
                }
            }
            sb.AppendFormat("<li class='light-blue'>");
            sb.AppendFormat("<a data-toggle='dropdown' href='#' class='dropdown-toggle'>");
            sb.AppendFormat("      <img class='nav-user-photo' src='../Content/themes/assets/avatars/user.jpg' alt='Jason's Photo' />");
            sb.AppendFormat("      <span class='user-info'>");
            sb.AppendFormat("          <small>欢迎光临,</small>");
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            sb.AppendFormat("          <font color='red'>{0}</font>", cookieInfo.trueName);
            sb.AppendFormat("      </span>");
            sb.AppendFormat("<i class='icon-caret-down'></i>");
            sb.AppendFormat("</a>");
            sb.AppendFormat("<ul class='user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close'>");
            foreach (var v in result)
            {
                string codeStr = ",006,007,";
                if (codeStr.Contains("," + v.MENUCODE + ","))
                {
                    sb.AppendFormat("      <li class='divider'></li>");
                    var subResult = v.subMenuModel;
                    foreach (var sv in subResult)
                    {
                        sb.AppendFormat("<li>");
                        sb.AppendFormat("<a href='{0}'>", sv.MENUURL);
                        sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", sv.MENUICO, sv.LICLASS);
                        sb.AppendFormat("{0}", sv.MENUNAME);
                        sb.AppendFormat("<span class='badge badge-important'>{0}</span>", sv.showCount);
                        sb.AppendFormat("</a>");
                        sb.AppendFormat("</li>");
                    }

                }
            }
            sb.AppendFormat("      <li class='divider'></li>");
            sb.AppendFormat("      <li>");
            sb.AppendFormat("          <a href='/System/LoginOut'>");
            sb.AppendFormat("              <i class='fa fa-power-off'></i>");
            sb.AppendFormat("              退出");
            sb.AppendFormat("          </a>");
            sb.AppendFormat("      </li>");
            sb.AppendFormat("</ul>");
            sb.AppendFormat("</li>");
            sb.AppendFormat("</ul>");
            if (blnMp3 == true)
                sb.AppendFormat("    <audio autoplay='autoplay'>        <source src='/Content/albram.mp3' type='audio/mpeg'>    </audio>");
            string str = sb.ToString();
            str = str.Replace("<i class='", "<i class='fa-1x ");
            return str;
        }

        #endregion

        #region 获取所有有权限的页面 （不含模块名称），并平铺显示 getHeadMenuStr2()
        public string getHeadMenuStr2()
        {
            var result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul class='nav ace-nav'>");
            foreach (var v in result)
            {
                var subResult = v.subMenuModel;
                foreach (var sv in subResult)
                    if (sv.MENUCODE.Length == 6)
                    {
                        sb.AppendFormat("<li class='light-blue'>");
                        sb.AppendFormat("<a  href='{0}'>", sv.MENUURL);
                        sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", sv.MENUICO, sv.LICLASS);
                        sb.AppendFormat("{0}", sv.MENUNAME);
                        sb.AppendFormat("</a>");
                        sb.AppendFormat("</li>");
                    }
            }
            sb.AppendFormat("<li class='light-blue'>");
            sb.AppendFormat("<a data-toggle='dropdown' href='#' class='dropdown-toggle'>");
            sb.AppendFormat("      <img class='nav-user-photo' src='../Content/themes/assets/avatars/user.jpg' alt='Jason's Photo' />");
            sb.AppendFormat("      <span class='user-info'>");
            sb.AppendFormat("          <small>欢迎光临,</small>");
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            sb.AppendFormat("          <font color='red'>{0}</font>", cookieInfo.trueName);
            sb.AppendFormat("      </span>");
            sb.AppendFormat("<i class='icon-caret-down'></i>");
            sb.AppendFormat("          </a>");
            sb.AppendFormat("<ul class='user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close'>");
            sb.AppendFormat("      <li class='divider'></li>");
            sb.AppendFormat("      <li>");
            sb.AppendFormat("          <a href='/System/LoginOut'>");
            sb.AppendFormat("              <i class='fa fa-power-off'></i>");
            sb.AppendFormat("              退出");
            sb.AppendFormat("          </a>");
            sb.AppendFormat("      </li>");
            sb.AppendFormat("</ul>");
            sb.AppendFormat("</li>");
            sb.AppendFormat("</ul>");
            return sb.ToString();
        }

        #endregion

        #region 获取某些模块菜单并平铺显示 getHeadMenuStr3(string menuCodeList)
        public string getHeadMenuStr3(string menuCodeList)
        {
            string PageCode = Request.Params["PageCode"];//调用方法
            var result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul class='nav ace-nav'>");
            sb.AppendFormat("<li class='light-blue'>");
            sb.AppendFormat("<a  href='{0}' class='dropdown-toggle'>", ConfigCls.getLoginRedirectUrl());
            sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", "fa fa-wrench", "color:#ff0000;");
            sb.AppendFormat("{0}", "返回首页");
            sb.AppendFormat("</a>");
            sb.AppendFormat("</li>");
            foreach (var v in result)
            {
                if (menuCodeList.Contains("," + v.MENUCODE + ","))
                {
                    var subResult = v.subMenuModel;
                    foreach (var sv in subResult)
                    {
                        sb.AppendFormat("<li>");

                        sb.AppendFormat("<a href='{0}'>", sv.MENUURL);
                        sb.AppendFormat("<i class='{0}' style='width:25px; {1}'></i>", sv.MENUICO, sv.LICLASS);
                        sb.AppendFormat("{0}", sv.MENUNAME);
                        if (string.IsNullOrEmpty(sv.showCount) == false)
                            sb.AppendFormat("<span class='badge badge-important'>{0}</span>", sv.showCount);
                        sb.AppendFormat("</a>");
                        sb.AppendFormat("</li>");
                    }
                }
            }
            sb.AppendFormat("<li class='light-blue'>");
            sb.AppendFormat("<a data-toggle='dropdown' href='#' class='dropdown-toggle'>");
            sb.AppendFormat("      <img class='nav-user-photo' src='../Content/themes/assets/avatars/user.jpg' alt='Jason's Photo' />");
            sb.AppendFormat("      <span class='user-info'>");
            sb.AppendFormat("          <small>欢迎光临,</small>");
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            sb.AppendFormat("          <font color='red'>{0}</font>", cookieInfo.trueName);
            sb.AppendFormat("      </span>");
            sb.AppendFormat("<i class='icon-caret-down'></i>");
            sb.AppendFormat("          </a>");
            sb.AppendFormat("<ul class='user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close'>");
            sb.AppendFormat("      <li class='divider'></li>");
            sb.AppendFormat("      <li>");
            sb.AppendFormat("          <a href='/System/LoginOut'>");
            sb.AppendFormat("              <i class='fa fa-power-off'></i>");
            sb.AppendFormat("              退出");
            sb.AppendFormat("          </a>");
            sb.AppendFormat("      </li>");
            sb.AppendFormat("</ul>");
            sb.AppendFormat("</li>");
            sb.AppendFormat("</ul>");
            return sb.ToString();
        }

        #endregion

        #region 获取头部菜单JSON字符串 getHeadMenuJson1111111111111111111111111()
        /// <summary>
        /// 获取头部菜单JSON字符串
        /// </summary>
        /// <returns>JSON字符串</returns>
        public ActionResult getHeadMenuJson1111111111111111111111111()
        {
            if (ConfigCls.getMenuShowMode() == "2")
                return Content(JsonConvert.SerializeObject(new Message(true, getHeadMenuStr2(), "")), "text/html;charset=UTF-8");
            else
            {
                string PageCode = Request.Params["PageCode"];
                if (PageCode.Substring(0, 3) == "008")
                    return Content(JsonConvert.SerializeObject(new Message(true, getHeadMenuStr3(",008,"), "")), "text/html;charset=UTF-8");
                if (PageCode.Substring(0, 3) == "009")
                    return Content(JsonConvert.SerializeObject(new Message(true, getHeadMenuStr3(",009,"), "")), "text/html;charset=UTF-8");
                if (PageCode.Substring(0, 3) == "010")
                    return Content(JsonConvert.SerializeObject(new Message(true, getHeadMenuStr3(",010,"), "")), "text/html;charset=UTF-8");
                if (PageCode.Substring(0, 3) == "006" || PageCode.Substring(0, 3) == "007")
                    return Content(JsonConvert.SerializeObject(new Message(true, getHeadMenuStr1(",006,007,"), "")), "text/html;charset=UTF-8");
                return Content(JsonConvert.SerializeObject(new Message(true, getHeadMenuStr1(",001,002,003,004,005,"), "")), "text/html;charset=UTF-8");//008,009,010,011,
            }
        }

        #endregion

        #endregion



        #region 获取各模块顶部菜单（防火系列）
        public string getPageMenuStr(string PageCode)
        {
            //获取有权限的所有菜单
            List<SystemMenu_Model> result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() }).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<dl>");
            List<SystemMenu_Model> topMenuResult = result.Where(p => p.ISTOPMENU == "1" && p.MENUCODE.Length == 3).ToList();//获取顶部菜单
            foreach (var v in topMenuResult)
            {
                string liClass = v.LICLASS;//判断样式
                if (string.IsNullOrEmpty(liClass))
                {
                    if (v.MENULINKMODE.Contains(PageCode.Substring(0, 3)))
                        liClass = " LiCur";
                    else
                        liClass = " LiDefault";
                }
                //判断跳转地址
                string url = "";
                string[] arr = v.MENULINKMODE.Split(',');
                for (int kkk = 0; kkk < arr.Length; kkk++)
                {
                    foreach (var linkV in result.Where(p => p.MENUCODE == arr[kkk]))
                    {
                        foreach (var vv in linkV.subMenuModel)
                        {
                            if (string.IsNullOrEmpty(url))
                            {
                                if (string.IsNullOrEmpty(vv.MENUURL) == false)
                                    url = vv.MENUURL;
                            }
                        }
                    }
                }
                if (v.MENUDROWMTHOD == "1")//有下拉框
                    sb.AppendFormat("<dd id='menuTop_{2}' class='{0}'>{1}", liClass, v.MENUNAME, v.MENUCODE);
                else
                {
                    if (v.MENUOPENMETHOD == "1")//弹出
                        sb.AppendFormat("<dd  id='menuTop_{3}' class='{0}' onclick=\"window.open('{1}');\">{2}", liClass, url, v.MENUNAME, v.MENUCODE);
                    else //本页面跳转
                        sb.AppendFormat("<dd id='menuTop_{3}' class='{0}' ondblclick=\"window.open('{1}');\"  onclick=\"window.location.href='{1}';\">{2}", liClass, url, v.MENUNAME, v.MENUCODE);
                }
                if (v.MENUDROWMTHOD == "1")
                {
                    sb.AppendFormat(" <ul>");
                    foreach (var vv in v.subMenuModel)
                    {
                        sb.AppendFormat(" <li ondblclick=\"window.open('{0}');\" onclick=\"window.location.href='{0}';\"><a>{1}</a></li>", vv.MENUURL, vv.MENUNAME);
                    }
                    sb.AppendFormat(" </ul>");
                }
                sb.AppendFormat("</dd>");
            }
            sb.AppendFormat("</dl>");
            return sb.ToString();
        }

        #endregion

        #region 获取各模块左侧菜单（防火系列）
        public string getPageLeftMenu(string PageCode)
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() });
            var m = result.Where(p => p.MENUCODE == PageCode.Substring(0, 3)).FirstOrDefault();//获取该页面对应的菜单信息
            sb.AppendFormat("<ul class=\"page-menu\">\r\n");
            int indexI = -1;
            int indexII = 0;
            if (m != null)
            {
                string tmp = m.MENULINKMODE;
                if (string.IsNullOrEmpty(tmp))
                    tmp = m.MENUCODE;
                string[] arr = tmp.Split(',');//循环数组，用于判断如果是多个模块组合成左侧菜单
                for (int i = 0; i < arr.Length; i++)
                {
                    var mModel = result.Where(p => p.MENUCODE == arr[i]).FirstOrDefault();
                    string[] arrHQJC = new string[55];
                    if (arr[i] == "040")//火情监控统计个数
                    {
                        var list = new List<JC_FIRE_Model>();
                        string orgno = "";
                        bool bo = PublicCls.OrgIsShi(SystemCls.getCurUserOrgNo());//市机构
                        if (!bo)
                        {
                            bool bc = PublicCls.OrgIsZhen(SystemCls.getCurUserOrgNo());//乡镇机构
                            if (bc)
                            {
                                orgno = SystemCls.getCurUserOrgNo();//
                            }
                            else
                            {
                                orgno = SystemCls.getCurUserOrgNo().Substring(0, 6);//
                            }
                        }
                        if (string.IsNullOrEmpty(orgno))
                        {
                            list = JC_FIRECls.GetListModel(new JC_FIRE_SW { }).Where(p => p.ISOUTFIRE.Trim() != "1" && p.MANSTATE != "19" && p.MANSTATE != "18").ToList();//不筛选出火已灭的记录并且已经上报（非火情）19为市（州）已经上报  // && p.MANSTATE != "19"
                        }
                        else
                        {
                            list = JC_FIRECls.GetListModel(new JC_FIRE_SW { }).Where(p => p.BYORGNO.StartsWith(orgno.ToString()) && p.ISOUTFIRE.Trim() != "1" && p.MANSTATE != "19" && p.MANSTATE != "18").ToList();//不筛选出火已灭的记录
                        }

                       
                        arrHQJC[2] = list.Where(p => p.FIREFROM == "2").Count().ToString();
                        arrHQJC[5] = list.Where(p => p.FIREFROM == "5").Count().ToString();
                        arrHQJC[4] = list.Where(p => p.FIREFROM == "4").Count().ToString();
                        arrHQJC[6] = list.Where(p => p.FIREFROM == "6").Count().ToString();
                        arrHQJC[3] = list.Where(p => p.FIREFROM == "3").Count().ToString();
                    }
                    if (mModel != null)
                    {
                        indexI++;
                        sb.AppendFormat("<li><span><h1><span>{0}</span></h1></span>\r\n", mModel.MENUNAME);
                        sb.AppendFormat("<ul>\r\n");
                        foreach (var sv in mModel.subMenuModel)
                        {
                            var menu = false;
                            var num = "";
                            if (arr[i] == "040")//火情监控
                            {
                                if (sv.MENUCODE == "040002")
                                {
                                    menu = true;
                                    num = arrHQJC[2];
                                }
                                if (sv.MENUCODE == "040003")
                                {
                                    menu = true;
                                    num = arrHQJC[3];
                                }
                                if (sv.MENUCODE == "040004")
                                {
                                    menu = true;
                                    num = arrHQJC[4];
                                }
                                if (sv.MENUCODE == "040005")
                                {
                                    menu = true;
                                    num = arrHQJC[5];
                                }
                                if (sv.MENUCODE == "040006")
                                {
                                    menu = true;
                                    num = arrHQJC[6];
                                }
                            }
                            if (sv.MENUCODE == "009005")
                            {
                                menu = true;
                                num = E_RECEIVE_Cls.getCount();
                            }
                            if (sv.MENUCODE == "011002")
                            {
                                menu = true;
                                num = DC_ARMYCls.getCount();
                            }
                            if (sv.MENUCODE == "011003")
                            {
                                menu = true;
                                num = DC_RESOURCE_NEWCls.getCount();
                            }
                            if (sv.MENUCODE == "011005")
                            {
                                menu = true;
                                num = DC_EQUIP_NEWCls.getCount();
                            }
                            if (sv.MENUCODE == "011006")
                            {
                                menu = true;
                                num = JC_FIRECls.getCount();
                            }
                            if (sv.MENUCODE == "011008")
                            {
                                menu = true;
                                num = DC_REPOSITORYCls.getCount();
                            }
                            if (sv.MENUCODE == "011009")
                            {
                                menu = true;
                                num = DC_CARCls.getCount();
                            }
                            if (sv.MENUCODE == "011011")
                            {
                                menu = true;
                                num = TD_MOUNTAINCls.getCount();
                            }
                            if (sv.MENUCODE == "016001")
                            {
                                menu = true;
                                num = DC_UTILITY_CAMPCls.getCount();
                            }
                            if (sv.MENUCODE == "016002")
                            {
                                menu = true;
                                num = DC_UTILITY_OVERWATCHCls.getCount();
                            }
                            if (sv.MENUCODE == "016003")
                            {
                                menu = true;
                                num = DC_UTILITY_ISOLATIONSTRIPCls.getCount();
                            }
                            if (sv.MENUCODE == "016004")
                            {
                                menu = true;
                                num = DC_UTILITY_PROPAGANDASTELECls.getCount();
                            }
                            if (sv.MENUCODE == "016005")
                            {
                                menu = true;
                                num = DC_UTILITY_RELAYCls.getCount();
                            }
                            if (sv.MENUCODE == "016006")
                            {
                                menu = true;
                                num = DC_UTILITY_MONITORINGSTATIONCls.getCount();
                            }
                            if (sv.MENUCODE == "016007")
                            {
                                menu = true;
                                num = DC_UTILITY_FIRECHANNELCls.getCount();
                            }
                            if (sv.MENUCODE == "016008")
                            {
                                menu = true;
                                num = DC_UTILITY_FACTORCOLLECTSTATIONCls.getCount();
                            }
                            if (PageCode == sv.MENUCODE)
                            {
                                if (menu == true)
                                    sb.AppendFormat("<li class=\"title liCur\" onclick=\"window.location.href='{0}';\"><label class=\" {2}\"></label>{1}<font color =\"red\">[{3}]</font></li>\r\n", sv.MENUURL, sv.MENUNAME, sv.LICLASS, num);
                                else
                                    sb.AppendFormat("<li class=\"title liCur\" onclick=\"window.location.href='{0}';\"><label class=\" {2}\"></label>{1}</li>\r\n", sv.MENUURL, sv.MENUNAME, sv.LICLASS);
                                indexII = indexI;
                            }
                            else
                            {
                                if (menu == true)
                                {
                                    sb.AppendFormat("<li class=\"title {2}\" onclick=\"window.location.href='{0}';\"><label class=\" {2}\"></label>{1}<font color =\"red\">[{3}]</font></li>\r\n", sv.MENUURL, sv.MENUNAME, sv.LICLASS, num);
                                }
                                else
                                {
                                    sb.AppendFormat("<li class=\"title {2}\" onclick=\"window.location.href='{0}';\"><label class=\" {2}\"></label>{1}</li>\r\n", sv.MENUURL, sv.MENUNAME, sv.LICLASS);
                                }
                            }
                        }
                        sb.AppendFormat("</ul>\r\n");
                        sb.AppendFormat("</li>\r\n");
                    }
                }
            }
            sb.AppendFormat("</ul>\r\n");
            string str = "";
            str += "        <script type=\"text/javascript\">\r\n";
            str += "            function menuEvent() {";
            str += "                var $parent_li, $ul, $span;\r\n";
            str += "                $(\".page-menu li>span\").click(function () {\r\n";
            str += "                    $span = $(this);\r\n";
            str += "                    $parent_li = $span.parent();\r\n";
            str += "                    $parent_li.siblings().children(\"ul\").slideUp();\r\n";
            str += "                    $ul = $span.next(\"ul\");\r\n";
            str += "                    $ul.slideToggle();\r\n";
            str += "                });\r\n";
            str += "                $(\".page-menu>li:eq(" + indexII + ")>span\").click();\r\n";
            str += "            }\r\n";
            str += "            $(function () {\r\n";
            str += "                menuEvent();\r\n";
            str += "            })\r\n";
            str += "        </script>\r\n";
            return sb.ToString() + str;//008,009,010,011,
        }

        #endregion

        #region Npoi导出Excel样式
        /// <summary>
        /// 普通单元格样式 居左
        /// </summary>
        /// <param name="book"></param>
        /// <returns>参见模型</returns>
        public ICellStyle getCellStyleLeft(NPOI.HSSF.UserModel.HSSFWorkbook book)
        {
            ICellStyle cellstyle = book.CreateCellStyle();
            cellstyle.Alignment = HorizontalAlignment.Left; //水平居中
            cellstyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            cellstyle.WrapText = true; //自动换行
            IFont cellfont = book.CreateFont();
            cellfont.FontHeightInPoints = 11; //11号字体
            cellstyle.SetFont(cellfont);
            return cellstyle;
        }

        /// <summary>
        /// 普通单元格样式 居中
        /// </summary>
        /// <param name="book"></param>
        /// <returns>参见模型</returns>
        public ICellStyle getCellStyleCenter(NPOI.HSSF.UserModel.HSSFWorkbook book)
        {
            ICellStyle cellstyle = book.CreateCellStyle();
            cellstyle.Alignment = HorizontalAlignment.Center; //水平居中
            cellstyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            cellstyle.WrapText = true; //自动换行
            IFont cellfont = book.CreateFont();
            cellfont.FontHeightInPoints = 11; //11号字体
            cellstyle.SetFont(cellfont);
            return cellstyle;
        }

        /// <summary>
        /// 头部单元格样式
        /// </summary>
        /// <param name="book"></param>
        /// <returns>参见模型</returns>
        public ICellStyle getCellStyleHead(NPOI.HSSF.UserModel.HSSFWorkbook book)
        {
            ICellStyle cellstyle = book.CreateCellStyle();
            cellstyle.Alignment = HorizontalAlignment.Center;
            cellstyle.VerticalAlignment = VerticalAlignment.Center;
            cellstyle.WrapText = true;
            IFont cellheadfont = book.CreateFont();
            cellheadfont.FontHeightInPoints = 11;
            cellheadfont.Boldweight = (short)FontBoldWeight.Bold; //字体加粗
            cellstyle.SetFont(cellheadfont);
            return cellstyle;
        }

        /// <summary>
        /// 标题单元格样式
        /// </summary>
        /// <param name="book"></param>
        /// <returns>参见模型</returns>
        public ICellStyle getCellStyleTitle(NPOI.HSSF.UserModel.HSSFWorkbook book)
        {
            ICellStyle cellstyle = book.CreateCellStyle();
            cellstyle.Alignment = HorizontalAlignment.Center;
            cellstyle.VerticalAlignment = VerticalAlignment.Center;
            IFont titlefont = book.CreateFont();
            titlefont.FontHeightInPoints = 14;
            titlefont.Boldweight = (short)FontBoldWeight.Bold; //字体加粗
            cellstyle.SetFont(titlefont);
            return cellstyle;
        }

        #endregion
    }
}
