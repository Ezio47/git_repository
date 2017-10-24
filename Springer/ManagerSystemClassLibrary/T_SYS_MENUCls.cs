using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统菜单管理公共类
    /// </summary>
    public class T_SYS_MENUCls
    {
        #region 获取系统菜单单条记录
        /// <summary>
        /// 获取系统菜单单条记录
        /// </summary>
        /// <param name="sw">参见模型 sw.SYSFLAG系统标识符 sw.MENUCODE 菜单编码</param>
        /// <returns>参见模型</returns>
        public static T_SYS_MENU_Model getModel(T_SYS_MENU_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_MENU.getModel(sw);
            T_SYS_MENU_Model m = new T_SYS_MENU_Model();
            if (dt == null)
                return m;
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.MENUCODE = dt.Rows[i]["MENUCODE"].ToString();
                m.MENUNAME = dt.Rows[i]["MENUNAME"].ToString();
                m.MENUURL = dt.Rows[i]["MENUURL"].ToString();
                m.MENUICO = dt.Rows[i]["MENUICO"].ToString();
                m.LICLASS = dt.Rows[i]["LICLASS"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.MENURIGHTFLAG = dt.Rows[i]["MENURIGHTFLAG"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.MENUOPENMETHOD = dt.Rows[i]["MENUOPENMETHOD"].ToString();
                m.MENULINKMODE = dt.Rows[i]["MENULINKMODE"].ToString();
                m.MENUDROWMTHOD = dt.Rows[i]["MENUDROWMTHOD"].ToString();
                m.ISTOPMENU = dt.Rows[i]["ISTOPMENU"].ToString();

            }
            dt.Clear();
            dt.Dispose();
            return m;
        } 
        #endregion

        #region 获取所有有权限的系统菜单
        /// <summary>
        /// 获取所有系统菜单
        /// </summary>
        /// <param name="sw">参见T_SYS_MENU_SW</param>
        /// <returns>参见SystemMenu_Model</returns>
        public static IEnumerable<SystemMenu_Model> getT_SYS_MENUModel(T_SYS_MENU_SW sw)
        {
            var result = new List<SystemMenu_Model>();
            if (string.IsNullOrEmpty(SystemCls.getUserID()))
                return result;
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendFormat(" FROM T_SYS_MENU WHERE 1=1");
            sb.AppendFormat(" AND  SYSFLAG='{0}' ", sw.SYSFLAG);
            sb.AppendFormat(" AND MENURIGHTFLAG in (select RIGHTID from T_SYSSEC_ROLE_RIGHT where (ROLEID in (select ROLEID from T_SYSSEC_USER_ROLE where USERID={0} ) and RIGHTID in(select RIGHTID from T_SYSSEC_RIGHT where SYSFLAG='{1}')) and roleid in (select roleid from T_SYSSEC_ROLE where SYSFLAG='{1}') )", SystemCls.getUserID(), ConfigCls.getSystemFlag());
            if (string.IsNullOrEmpty(sw.MENUCODE) == false)
                sb.AppendFormat(" AND  left(MENUCODE,3) in({0}) ", sw.MENUCODE);
            if (string.IsNullOrEmpty(sw.MenuCodeList) == false)
                sb.AppendFormat(" AND  left(MENUCODE,3) in({0}) ", sw.MenuCodeList);
            string sql = "SELECT MENUID,MENUCODE,MENUNAME,MENUURL,MENUICO,LICLASS,ORDERBY,MENURIGHTFLAG,SYSFLAG,MENUOPENMETHOD, MENULINKMODE, MENUDROWMTHOD, ISTOPMENU   " + sb.ToString() + " ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            DataTable dt = ds.Tables[0];
            DataRow[] dr = dt.Select("len(MENUCODE)=3", "ORDERBY");
            //获取上报数据
            DataTable dtR = BaseDT.T_IPSRPT_REPORT.getDT(new T_IPSRPT_REPORT_SW { MANSTATE = "0", orgNo = SystemCls.getCurUserOrgNo() });
            CookieModel cookieInfo = SystemCls.getCookieInfo();//登录用户信息            
            for (int i = 0; i < dr.Length; i++)
            {
                SystemMenu_Model m = new SystemMenu_Model();
                m.MENUCODE = dr[i]["MENUCODE"].ToString();
                m.MENUICO = dr[i]["MENUICO"].ToString();
                m.MENUNAME = dr[i]["MENUNAME"].ToString();
                m.LICLASS = dr[i]["LICLASS"].ToString();
                m.MENUOPENMETHOD = dr[i]["MENUOPENMETHOD"].ToString();
                m.MENULINKMODE = dr[i]["MENULINKMODE"].ToString();
                m.MENUDROWMTHOD = dr[i]["MENUDROWMTHOD"].ToString();
                m.ISTOPMENU = dr[i]["ISTOPMENU"].ToString();
                long mShowCount = 0;//一级菜单显示数量
                var SubM = new List<T_SYS_MENU_Model>();
                DataRow[] dr1 = dt.Select("len(MENUCODE)=6 and SUBSTRING(MENUCODE,1,3)='" + dr[i]["MENUCODE"].ToString() + "'", "ORDERBY");
                for (int k = 0; k < dr1.Length; k++)
                {
                    T_SYS_MENU_Model mm = new T_SYS_MENU_Model();
                    mm.MENUCODE = dr1[k]["MENUCODE"].ToString();
                    mm.MENUURL = dr1[k]["MENUURL"].ToString().Replace("[USERID]", cookieInfo.UID).Replace("[LOGINUSERNAME]", cookieInfo.userName);
                    mm.MENUNAME = dr1[k]["MENUNAME"].ToString();
                    mm.MENUICO = dr1[k]["MENUICO"].ToString();
                    mm.LICLASS = dr1[k]["LICLASS"].ToString();
                    //获取需要显示提醒数量的菜单
                    string MENUCODE = dr1[k]["MENUCODE"].ToString();
                    string count = "0";
                    if (MENUCODE == "001004")
                        count = BaseDT.T_IPS_ALARM.getDT(new T_IPS_ALARM_SW { orgNo = SystemCls.getCurUserOrgNo(), MANSTATE = "0" }).Rows.Count.ToString();
                    if (MENUCODE == "001003")
                        count = BaseDT.T_IPS_HOTS.getDT(new T_IPS_HOTS_SW { MANSTATE = "0" }).Rows.Count.ToString();
                    if (MENUCODE.Substring(0, 3) == "002")
                    {
                        string tmpCode = MENUCODE.Substring(3, 3);
                        if (tmpCode.Substring(0, 2) == "00")
                            tmpCode = tmpCode.Substring(2, 1);
                        else if (tmpCode.Substring(0, 1) == "0")
                            tmpCode = tmpCode.Substring(1, 2);
                        else
                            tmpCode = tmpCode.Substring(2, 1);
                        count = BaseDT.T_IPSRPT_REPORT.getCountByType(dtR, tmpCode);
                        mm.TID = tmpCode;//类别ＩＤ 上报序号
                    }
                    if (MENUCODE.Substring(0, 3) == "003")
                    {
                        string tmpCode = MENUCODE.Substring(3, 3);
                        if (tmpCode.Substring(0, 2) == "00")
                            tmpCode = tmpCode.Substring(2, 1);
                        else if (tmpCode.Substring(0, 1) == "0")
                            tmpCode = tmpCode.Substring(1, 2);
                        else
                            tmpCode = tmpCode.Substring(2, 1);
                        //count = BaseDT.T_IPSRPT_REPORT.getCountByType(dtR, tmpCode);
                        mm.TID = tmpCode; //类别ＩＤ 上报序号
                    }
                    mShowCount += Convert.ToInt64(count);
                    if (count == "0") count = "";
                    mm.showCount = count;
                    SubM.Add(mm);
                }
                m.showCount = (mShowCount == 0) ? "" : mShowCount.ToString();
                m.subMenuModel = SubM;
                result.Add(m);
            }
            ds.Clear();
            ds.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型T_SYS_MENU_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_SYS_MENU_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgMENU = BaseDT.T_SYS_MENU.Add(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgMENU = BaseDT.T_SYS_MENU.Mdy(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgMENU = BaseDT.T_SYS_MENU.Del(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型T_SYS_MENU_Model</param>
        /// <returns>参见模型T_SYS_MENU_Model</returns>
        public static IEnumerable<T_SYS_MENU_Model> getListModel(T_SYS_MENU_SW sw)
        {
            DataTable dt201 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID  = "201" });//菜单打开方式
            DataTable dt202 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "202" });//菜单下拉方式
            DataTable dt203 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "203" });//是否为顶级菜单
            var result = new List<T_SYS_MENU_Model>();
            DataTable dt = BaseDT.T_SYS_MENU.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_MENU_Model m = new T_SYS_MENU_Model();
                m.MENUCODE = dt.Rows[i]["MENUCODE"].ToString();
                m.MENUNAME = dt.Rows[i]["MENUNAME"].ToString();
                m.MENUURL = dt.Rows[i]["MENUURL"].ToString();
                m.MENUICO = dt.Rows[i]["MENUICO"].ToString();
                m.LICLASS = dt.Rows[i]["LICLASS"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.MENURIGHTFLAG = dt.Rows[i]["MENURIGHTFLAG"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.MENUOPENMETHOD = dt.Rows[i]["MENUOPENMETHOD"].ToString();
                m.MENUOPENMETHODName = BaseDT.T_SYS_DICT.getName(dt201, m.MENUOPENMETHOD);
                m.MENULINKMODE = dt.Rows[i]["MENULINKMODE"].ToString();
                m.MENUDROWMTHOD = dt.Rows[i]["MENUDROWMTHOD"].ToString();
                m.MENUDROWMTHODName = BaseDT.T_SYS_DICT.getName(dt202, m.MENUDROWMTHOD);
                m.ISTOPMENU = dt.Rows[i]["ISTOPMENU"].ToString();
                m.ISTOPMENUName = BaseDT.T_SYS_DICT.getName(dt203, m.ISTOPMENU);
                result.Add(m);
            }
            dt201.Clear();
            dt201.Dispose();
            dt202.Clear();
            dt202.Dispose();
            dt203.Clear();
            dt203.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 判断是否有下级
        /// <summary>
        /// 判断是否有下级
        /// </summary>
        /// <param name="sw">参见模型T_SYS_MENU_Model</param>
        /// <returns>true:存在 false：不存在</returns>
        public static bool isExistsChild(T_SYS_MENU_SW sw)
        {
            return BaseDT.T_SYS_MENU.isExistsChild(sw);
        }
        #endregion

        #region 根据菜单编码查询菜单名称
        /// <summary>
        /// 根据菜单编码查询菜单名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getMenuNameByCode(T_SYS_MENU_SW sw)
        {
            return BaseDT.T_SYS_MENU.getMenuNameByCode(sw);
        } 
        #endregion
    }
}
