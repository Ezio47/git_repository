using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
using System.Web.SessionState;
using OAModel;
namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统类
    /// </summary>
    public class SystemCls
    {
        #region 获取当前登录用户相关信息
        /// <summary>
        /// 获取当前登录用户相关信息
        /// </summary>
        /// <param name="hid">用户ID</param>
        /// <returns></returns>
        public static string getHUserOrg(string hid)
        {
            return T_IPSFR_USERCls.getModel(new T_IPSFR_USER_SW { HID = hid }).BYORGNO;
        }
        #endregion

        #region 获取当前登录用户组织机构
        /// <summary>
        /// 获取当前登录用户组织机构
        /// </summary>
        /// <returns>当前登录用户组织机构</returns>
        public static string getCurUserOrgNo()
        {
            if (string.IsNullOrEmpty(getUserID()))
                return "";
            return T_SYSSEC_IPSUSERCls.getOrgNoByUID(getUserID());
        }
        #endregion

        #region 日志保存
        /// <summary>
        /// 日志保存
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="title">标题</param>
        /// <param name="nr">内容</param>
        public static void LogSave(string type, string title, string nr)
        {
            T_SYS_LOG_Model m = new T_SYS_LOG_Model();
            m.LOGTYPE = type;
            m.OPERATION = title;
            m.OPERATIONCONTENT = nr;
            m.LOGINUSERID = getUserID();
            m.USERIP = ClsHtml.GetIP();
            m.OPERATETIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);
            m.opMethod = "Add";
            m.returnUrl = "";
            m.SYSFLAG = ConfigCls.getSystemFlag();
            BaseDT.T_SYS_LOG.Add(m);
        }
        #endregion

        #region 权限
        /// <summary>
        /// 根据 权限ID判断当前登录用户是否具有相应的权限
        /// </summary>
        /// <param name="RightID">权限编码</param>
        /// <returns>true 有权限 false 无</returns>
        public static bool isRight(string RightID)
        {
            string uid = getUserID();
            if (string.IsNullOrEmpty(uid))
                return false;
            if (string.IsNullOrEmpty(RightID))
                return false;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select RIGHTID from T_SYSSEC_ROLE_RIGHT where RIGHTID='{0}' and ROLEID in (select ROLEID from T_SYSSEC_USER_ROLE where USERID={1} )", RightID, uid);
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 用户COOKIE
        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="m">参见CookieModel</param>
        public static void SaveLoginState(CookieModel m)
        {
            ClearLoginState();
            string str = "";
            str += ClsStr.EncryptA01(m.UID, "DKIDLEKD") + ",";
            str += ClsStr.EncryptA01(m.userName, "DKIDLEKD") + ",";
            str += ClsStr.EncryptA01(m.trueName, "DKIDLEKD") + ",";
            str = ClsStr.EncryptA01(str, "LOKUDJIE");
            System.Web.HttpContext.Current.Session["SpringerSystemSession"] = str;
            if (m.SaveType == "false")
            {
            }
            else
            {
                HttpCookie _cookie = new HttpCookie("SpringerSystem");
                switch (m.SaveType)
                {
                    case "true":
                        _cookie.Expires = DateTime.Now.AddYears(1);//.AddDays(1);
                        break;
                    default:
                        _cookie.Expires = DateTime.Now.AddMinutes(10);//.AddYears(1);//.AddMinutes(10);//保存10分钟
                        break;
                }
                _cookie.Values.Add("SpringerSystemCookie", str);
                HttpContext.Current.Response.Cookies.Add(_cookie);
            }
        }

        /// <summary>
        /// 清除登录状态
        /// </summary>
        public static void ClearLoginState()
        {
            System.Web.HttpContext.Current.Session["SpringerSystemSession"] = "";
            if (HttpContext.Current.Request.Cookies["SpringerSystem"] != null)
            {
                HttpCookie _cookie = HttpContext.Current.Request.Cookies["SpringerSystem"];
                _cookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.AppendCookie(_cookie);
            }
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns>用户ID</returns>
        public static string getUserID()
        {
            CookieModel cm = getCookieInfo();
            if (ConfigCls.getIsSaveLastOpTime() != "0" && string.IsNullOrEmpty(cm.UID) == false)//需要判断用户在线状态
            {
                //更改该用户最后操作时间
                T_SYSSEC_IPSUSER_Model m = new T_SYSSEC_IPSUSER_Model { USERID = cm.UID, opMethod = "MdyLastOpTime" };
                T_SYSSEC_IPSUSERCls.Manager(m);
            }
            return cm.UID;
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns>用户登录信息 参见CookieModel</returns>
        public static CookieModel getCookieInfo()
        {
            CookieModel model = new CookieModel();
            string cookieStr = "";
            if (System.Web.HttpContext.Current.Session["SpringerSystemSession"] != null)
                cookieStr = System.Web.HttpContext.Current.Session["SpringerSystemSession"].ToString();
            HttpCookie cookies = HttpContext.Current.Request.Cookies["SpringerSystem"];
            if (string.IsNullOrEmpty(cookieStr) && cookies != null)
            {
                cookieStr = cookies["SpringerSystemCookie"];
            }
            //if (HttpContext.Current.Request.Cookies["SpringerSystem"] != null)
            //{
            //    cookieStr = HttpContext.Current.Request.Cookies["SpringerSystem"]["SpringerSystemCookie"];
            //}
            if (!string.IsNullOrEmpty(cookieStr))
            {
                string[] arr = ClsStr.DecryptA01(cookieStr, "LOKUDJIE").Split(',');
                model.UID = ClsStr.DecryptA01(arr[0], "DKIDLEKD");
                model.userName = ClsStr.DecryptA01(arr[1], "DKIDLEKD");
                model.trueName = ClsStr.DecryptA01(arr[2], "DKIDLEKD");
            }
            else
            {
                model.UID = "";
                model.userName = "";
                model.trueName = "";
            }
            return model;
        }
        #endregion

        #region 系统菜单

        #region 获取用户自定义菜单
        /// <summary>
        /// 获取用户自定义菜单
        /// </summary>
        /// <param name="sw">参见T_SYS_MENU_SW</param>
        /// <returns>html代码</returns>
        public static string getT_SYS_DEFINEMENU(T_SYS_MENU_SW sw)
        {
            if (string.IsNullOrEmpty(sw.UID))
                return "";
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendFormat(" FROM T_SYS_MENU WHERE 1=1");
            bool bln = DataBaseClass.JudgeRecordExists("SELECT MENUCODE FROM T_SYS_DEFINEMENU WHERE (UID=" + sw.UID + ") AND SYSFLAG='" + sw.SYSFLAG + "'");
            if (bln)
            {
                sb.AppendFormat(" AND MENUCODE IN (SELECT  MENUCODE FROM T_SYS_DEFINEMENU WHERE UID={0} AND SYSFLAG='{1}') ", sw.UID, sw.SYSFLAG);
                sb.AppendFormat(" AND MENURIGHTFLAG in (select RIGHTID from T_SYSSEC_ROLE_RIGHT where (ROLEID in (select ROLEID from T_SYSSEC_USER_ROLE where USERID={0} )) and roleid in(select roleid from T_SYSSEC_ROLE where SYSFLAG='{1}') )", getUserID(), ConfigCls.getSystemFlag());
            }
            else
                sb.AppendFormat(" AND MENUCODE IN(SELECT   MENUCODE FROM T_SYS_DEFINEMENU WHERE  UID=0 AND SYSFLAG='{1}') ", sw.UID, sw.SYSFLAG);
            string sql = "SELECT MENUID,MENUCODE,MENUNAME,MENUURL,MENUICO,LICLASS,ORDERBY,MENURIGHTFLAG,SYSFLAG   " + sb.ToString() + "  ";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            StringBuilder sb1 = new StringBuilder();
            DataTable dtR = BaseDT.T_IPSRPT_REPORT.getDT(new T_IPSRPT_REPORT_SW { MANSTATE = "0", orgNo = SystemCls.getCurUserOrgNo() });
            sb1.AppendFormat("<ul class=\"nav ace-nav\">");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string MENUCODE = ds.Tables[0].Rows[i]["MENUCODE"].ToString();
                string count = "";
                switch (MENUCODE)
                {
                    case "002001":
                        count = BaseDT.T_IPSRPT_REPORT.getCountByType(dtR, "1");
                        break;
                    case "002002":
                        count = BaseDT.T_IPSRPT_REPORT.getCountByType(dtR, "2");
                        break;
                    case "002003":
                        count = BaseDT.T_IPSRPT_REPORT.getCountByType(dtR, "3");
                        break;
                    case "001004":
                        count = BaseDT.T_IPS_ALARM.getDT(new T_IPS_ALARM_SW { orgNo = SystemCls.getCurUserOrgNo(), MANSTATE = "0" }).Rows.Count.ToString();
                        break;
                    case "001003":
                        count = BaseDT.T_IPS_HOTS.getDT(new T_IPS_HOTS_SW { MANSTATE = "0" }).Rows.Count.ToString();
                        break;
                    default:
                        break;
                }
                if (count == "0")
                    count = "";
                string liclass = ds.Tables[0].Rows[i]["LICLASS"].ToString();
                if (string.IsNullOrEmpty(liclass))
                    liclass = "light-blue";
                sb1.AppendFormat("<li class=\"{0}\">", liclass);
                sb1.AppendFormat("    <a class=\"dropdown-toggle\" href=\"{0}\">", ds.Tables[0].Rows[i]["MENUURL"].ToString());
                if (string.IsNullOrEmpty(count) == false)
                {
                    sb1.AppendFormat("        <i class=\"{0}   icon-spin\"></i> {1}", ds.Tables[0].Rows[i]["MENUICO"].ToString(), ds.Tables[0].Rows[i]["MENUNAME"].ToString());
                    sb1.AppendFormat("    <span class=\"badge badge-important\">{0}</span>", count);
                }
                else
                {
                    sb1.AppendFormat("        <i class=\"{0}\"></i> {1}", ds.Tables[0].Rows[i]["MENUICO"].ToString(), ds.Tables[0].Rows[i]["MENUNAME"].ToString());
                }
                sb1.AppendFormat("    </a>");
                sb1.AppendFormat("</li>");
                //T_SYS_MENU_Model m = new T_SYS_MENU_Model();
                //m.MENUID = ds.Tables[0].Rows[i]["MENUID"].ToString();
                //m.MENUCODE = ds.Tables[0].Rows[i]["MENUCODE"].ToString();
                //m.MENUNAME = ds.Tables[0].Rows[i]["MENUNAME"].ToString();
                //m.MENUURL = ds.Tables[0].Rows[i]["MENUURL"].ToString();
                //m.MENUICO = ds.Tables[0].Rows[i]["MENUICO"].ToString();
                //m.ORDERBY = ds.Tables[0].Rows[i]["ORDERBY"].ToString();
                //m.MENURIGHTFLAG = ds.Tables[0].Rows[i]["MENURIGHTFLAG"].ToString();
                //m.SYSFLAG = ds.Tables[0].Rows[i]["SYSFLAG"].ToString();
                //result.Add(m);
            }
            CookieModel cookieInfo = SystemCls.getCookieInfo();//当前登录用户姓名

            sb1.AppendFormat(" <li class=\"light-blue\">");
            sb1.AppendFormat("  <a data-toggle=\"dropdown\" href=\"#\" class=\"dropdown-toggle\">");
            sb1.AppendFormat("      <img class=\"nav-user-photo\" src=\"../Content/themes/assets/avatars/user.jpg\" alt=\"Jason's Photo\" />");
            sb1.AppendFormat("      <span class=\"user-info\">");
            sb1.AppendFormat("          <small>欢迎光临,</small>");
            sb1.AppendFormat("          <font color=\"red\">{0}</font>", cookieInfo.trueName);
            sb1.AppendFormat("      </span>");

            sb1.AppendFormat("      <i class=\"icon-caret-down\"></i>");
            sb1.AppendFormat("  </a>");
            sb1.AppendFormat("  <ul class=\"user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close\">");
            sb1.AppendFormat("      <li class=\"divider\"></li>");
            sb1.AppendFormat("      <li>");
            sb1.AppendFormat("          <a href=\"/System/LoginOut\">");
            sb1.AppendFormat("              <i class=\"icon-off\"></i>");
            sb1.AppendFormat("              退出");
            sb1.AppendFormat("          </a>");
            sb1.AppendFormat("      </li>");
            sb1.AppendFormat("  </ul>");
            sb1.AppendFormat("</li>");
            sb1.AppendFormat("</ul>");
            ds.Clear();
            dtR.Clear();
            dtR.Dispose();
            return sb1.ToString();
        }

        #endregion

        #region 获取所有系统菜单
        /// <summary>
        /// 获取所有系统菜单
        /// </summary>
        /// <param name="sw">参见T_SYS_MENU_SW</param>
        /// <returns>组合html代码</returns>
        public static string getT_SYS_MENU_Str(T_SYS_MENU_SW sw)
        {
            if (string.IsNullOrEmpty(getUserID()))
                return "";
            //var result = new List<T_SYS_MENU_Model>();
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendFormat(" FROM T_SYS_MENU WHERE 1=1");
            sb.AppendFormat(" AND  SYSFLAG='{0}' ", sw.SYSFLAG);
            sb.AppendFormat(" AND MENURIGHTFLAG in(select RIGHTID from T_SYSSEC_ROLE_RIGHT where  (ROLEID in(select ROLEID from T_SYSSEC_USER_ROLE where USERID={0} )) and roleid in(select roleid from T_SYSSEC_ROLE where SYSFLAG='{1}') )", getUserID(), ConfigCls.getSystemFlag());
            string sql = "SELECT MENUID,MENUCODE,MENUNAME,MENUURL,MENUICO,ORDERBY,MENURIGHTFLAG,SYSFLAG   " + sb.ToString() + "  ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            DataTable dt = ds.Tables[0];
            DataRow[] dr = dt.Select("len(MENUCODE)=3");
            string str = "";
            for (int i = 0; i < dr.Length; i++)
            {
                str += "\r\n";
                if (string.IsNullOrEmpty(sw.MENUCODE) == true)
                {
                    if (i == 0)
                        str += "\r\n<li class=\"active open\">";
                    else
                        str += "\r\n<li>";
                }
                else
                {
                    if (sw.Method == "collect" && dr[i]["MENUCODE"].ToString() == "003")
                    {
                        str += "\r\n<li class=\"active open\">";
                    }
                    else
                    {
                        if (sw.MENUCODE.Substring(0, 3) == dr[i]["MENUCODE"].ToString())
                        {
                            str += "\r\n<li class=\"active open\">";
                        }
                        else
                        {
                            str += "\r\n<li>";
                        }
                    }
                }
                str += "\r\n    <a href=\"#\" class=\"dropdown-toggle\">";
                str += "\r\n        <i class=\"" + dr[i]["MENUICO"].ToString() + "\"></i>";
                str += "\r\n        " + dr[i]["MENUNAME"].ToString();
                str += "\r\n          <b class=\"arrow icon-angle-down\"></b>";
                str += "\r\n    </a>";
                str += "\r\n    <ul class=\"submenu\">";
                //如果是数据采集，需要动态获取采集类别
                if (dr[i]["MENUCODE"].ToString() == "003")
                {
                    DataTable dtC = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { SYSFLAG = ConfigCls.getSystemFlag(), DICTFLAG = "数据采集" });
                    DataRow[] drC = dtC.Select("", "ORDERBY");
                    for (int j = 0; j < drC.Length; j++)
                    {
                        if (sw.TID == drC[j]["DICTVALUE"].ToString() && sw.Method == "collect")
                        {
                            str += "\r\n<li class=\"active open\">";
                        }
                        else
                        {
                            str += "\r\n<li>";
                        }
                        str += "\r\n                    <a href=\"/Home/Index?Method=collect&TID=" + drC[j]["DICTVALUE"].ToString() + "\">";
                        str += "\r\n                        <i class=\"icon-double-angle-right\"></i>";
                        str += "\r\n                        " + drC[j]["DICTNAME"].ToString() + "";
                        str += "\r\n                    </a>";
                        str += "\r\n                </li>";

                    }
                }
                DataRow[] dr1 = dt.Select("len(MENUCODE)=6 and SUBSTRING(MENUCODE,1,3)='" + dr[i]["MENUCODE"].ToString() + "'");
                for (int k = 0; k < dr1.Length; k++)
                {
                    if (string.IsNullOrEmpty(sw.MENUCODE) == true)
                    {
                        if (k == 0)
                            str += "\r\n<li class=\"active open\">";
                        else
                            str += "\r\n<li>";
                    }
                    else
                    {
                        if (sw.MENUCODE == dr1[k]["MENUCODE"].ToString())
                        {
                            str += "\r\n<li class=\"active open\">";
                        }
                        else
                        {
                            str += "\r\n<li>";
                        }
                    }
                    str += "\r\n                    <a href=\"" + dr1[k]["MENUURL"].ToString() + "\">";
                    str += "\r\n                        <i class=\"" + dr1[k]["MENUICO"].ToString() + "\"></i>";
                    str += "\r\n                        " + dr1[k]["MENUNAME"].ToString() + "";
                    str += "\r\n                    </a>";
                    str += "\r\n                </li>";
                }
                str += "\r\n    </ul>";
                str += "\r\n</li>";
            }
            ds.Clear();
            ds.Dispose();
            dt.Clear();
            dt.Dispose();

            return str;
        }

        #endregion

        #endregion
    }
}
