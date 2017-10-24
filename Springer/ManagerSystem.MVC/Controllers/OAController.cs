using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using ManagerSystemModel;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystem.MVC.HelpCom;

namespace ManagerSystem.MVC.Controllers
{
    public class OAController : BaseController
    {
        #region 获取办公报件信息
        /// <summary>
        /// 获取OA办公信息
        /// </summary>
        /// <returns></returns>
        public string OAInfoStr()
        {
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            StringBuilder sb = new StringBuilder();
            string kqhref = "#", dbhref = "#", dxhref = "#", target = "_self";
            int[] nums = { 0, 0 };
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                if (HttpCommon.CheckUrlVisit(ConfigCls.getOAWebServiseAddress()))
                {
                    GetOAInfohref(cookieInfo, out kqhref, out dbhref, out dxhref, out target);
                    nums = OACls.GetOfficeNum(cookieInfo.UID);
                }
            }
            GetOAInfoStr(sb, kqhref, dbhref, dxhref, target, nums);
            return sb.ToString();
        }

        /// <summary>
        /// 前台定时获取办公信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OAInfo()
        {
            if (ConfigCls.getOAShowMethod() == "0")
                return Content(JsonConvert.SerializeObject(new Message(true, "", "")), "text/html;charset=UTF-8");
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            StringBuilder sb = new StringBuilder();
            string kqhref = "#", dbhref = "#", dxhref = "#", target = "_self";
            int[] nums = { 0, 0 };
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                if (HttpCommon.CheckUrlVisit(ConfigCls.getOAWebServiseAddress()))
                {
                    GetOAInfohref(cookieInfo, out kqhref, out dbhref, out dxhref, out target);
                    nums = OACls.GetOfficeNum(cookieInfo.UID);
                }
            }
            GetOAInfoStr(sb, kqhref, dbhref, dxhref, target, nums);
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取OA报件href
        /// </summary>
        /// <param name="cookieInfo">用户Cookie信息</param>
        /// <param name="kqhref">考勤href</param>
        /// <param name="dbhref">待办href</param>
        /// <param name="dxhref">短信href</param>
        /// <param name="target">页面跳转方式</param>
        private static void GetOAInfohref(CookieModel cookieInfo, out string kqhref, out string dbhref, out string dxhref, out string target)
        {
            kqhref = ConfigCls.getOAAddress() + "/Portal/AutoLogin/Login/login.aspx?userid=" + cookieInfo.UID + "&menuid=6685";
            dbhref = ConfigCls.getOAAddress() + "/Portal/AutoLogin/Login/login.aspx?userid=" + cookieInfo.UID + "&menuid=66666666";
            dxhref = ConfigCls.getOAAddress() + "/Portal/AutoLogin/Login/login.aspx?userid=" + cookieInfo.UID + "&menuid=21148999";
            target = "_blank";
        }

        /// <summary>
        /// 获取办公报件信息
        /// </summary>
        /// <param name="sb">拼接字符串</param>
        /// <param name="kqhref">考勤href</param>
        /// <param name="dbhref">待办href</param>
        /// <param name="dxhref">短信href</param>
        /// <param name="target">页面跳转方式</param>
        /// <param name="nums">报价数量</param>
        private static void GetOAInfoStr(StringBuilder sb, string kqhref, string dbhref, string dxhref, string target, int[] nums)
        {
            sb.AppendFormat("<div class=\"officeTools floatRight\">");
            if (ConfigCls.getOAShowMethod() == "1")
            {
                sb.AppendFormat("<dl class=\"floatLeft\">");
                sb.AppendFormat("<dt><img src=\"../../images/tools/tool_kq.png\"></dt>");
                sb.AppendFormat("<dd><a href=\"" + kqhref + "\" target=\"" + target + "\">考勤</a></dd>");
                //sb.AppendFormat("<dd><a href=\"#\" onclick='ShowMessageDialog(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")' >考勤</a></dd>", kqhref, "考勤签到签退", "1150px", "400px", true);
                //sb.AppendFormat("<dd><a href=\"#\" onclick='OpenMessageDialog(\"{0}\",\"{1}\")' >考勤</a></dd>", kqhref, "考勤签到签退");
                sb.AppendFormat("</dl>");
                sb.AppendFormat("<dl class=\"floatLeft\">");
                sb.AppendFormat("<dt><img src=\"../../images/tools/tool_db.png\"><span id=\"dbNum\">" + nums[0] + "</span></dt>");
                sb.AppendFormat("<dd><a href=\"" + dbhref + "\" target=\"" + target + "\">待办</a></dd>");
                sb.AppendFormat("</dl>");
                sb.AppendFormat("<dl class=\"floatLeft\">");
                sb.AppendFormat("<dt><img src=\"../../images/tools/tool_dx.png\"><span id=\"dxNum\">" + nums[1] + "</span></dt>");
                sb.AppendFormat("<dd><a href=\"" + dxhref + "\" target=\"" + target + "\">短信</a></dd>");
                //sb.AppendFormat("<dd><a href=\"#\" onclick='ShowMessageDialog(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")' >短信</a></dd>", dxhref, "信息专递", "1200px", "600px", true);
                //sb.AppendFormat("<dd><a href=\"#\" onclick='OpenMessageDialog(\"{0}\",\"{1}\")' >短信</a></dd>", dxhref, "信息专递");
                sb.AppendFormat("</dl>");
            }
            sb.AppendFormat("</div>");
        }
        #endregion
    }
}
