using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 监测_电子监控表
    /// </summary>
    public class JC_MONITORCls
    {
        #region 基本信息增、删、改

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_MONITOR_BASICINFO_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "电子监控基本信息:" + m.EMNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_MONITOR_BASICINFO.Add(m);

                if (msgUser.Success == true)
                {
                    Message msgUser1 = BaseDT.JC_MONITOR_BASICINFO.AddSHIPINGJIANKONG(m,msgUser.Url);//添加三维库
                    return new Message(msgUser1.Success, msgUser1.Msg, m.returnUrl);
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "电子监控基本信息:" + m.EMNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_MONITOR_BASICINFO.Mdy(m);
                if (msgUser.Success == true)
                {
                    Message msgUser1 = BaseDT.JC_MONITOR_BASICINFO.MdySHIPINGJIANKONG(m);//添加三维库
                    return new Message(msgUser1.Success, msgUser1.Msg, m.returnUrl);
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "电子监控基本信息:" + m.EMNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_MONITOR_BASICINFO.Del(m);
                if (msgUser.Success == true)
                {
                    Message msgUser1 = BaseDT.JC_MONITOR_BASICINFO.DelSHIPINGJIANKONG(m);//添加三维库
                    return new Message(msgUser1.Success, msgUser1.Msg, m.returnUrl);
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }
        #endregion

        #region  根据查询条件获取某一条信息记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_MONITOR_BASICINFO_Model getModel(JC_MONITOR_BASICINFO_SW sw)
        {
            DataTable dt = BaseDT.JC_MONITOR_BASICINFO.getDT(sw);
            JC_MONITOR_BASICINFO_Model m = new JC_MONITOR_BASICINFO_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            if (dt.Rows.Count > 0)
            {//, XH, PP, GD, JCJL
                int i = 0;
                m.EMID = dt.Rows[i]["EMID"].ToString();
                m.TTBH = dt.Rows[i]["TTBH"].ToString();
                m.EMNAME = dt.Rows[i]["EMNAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.GC = dt.Rows[i]["GC"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.IP = dt.Rows[i]["IP"].ToString();
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();
                m.USERPWD = dt.Rows[i]["USERPWD"].ToString();
                m.XH = dt.Rows[i]["XH"].ToString();
                m.PP = dt.Rows[i]["PP"].ToString();
                m.GD = dt.Rows[i]["GD"].ToString();
                m.JCJL = dt.Rows[i]["JCJL"].ToString();
                m.OBJID = dt.Rows[i]["OBJID"].ToString();
                m.TEMPLATEDID = dt.Rows[i]["TEMPLATEDID"].ToString();
                m.PORT = dt.Rows[i]["PORT"].ToString();
                m.TYPE = dt.Rows[i]["TYPE"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
            }

            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_MONITOR_BASICINFO_Model> getListModel(JC_MONITOR_BASICINFO_SW sw)
        {
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt = BaseDT.JC_MONITOR_BASICINFO.getDT(sw);//列表
            var result = new List<JC_MONITOR_BASICINFO_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_MONITOR_BASICINFO_Model m = new JC_MONITOR_BASICINFO_Model();
                m.EMID = dt.Rows[i]["EMID"].ToString();
                m.TTBH = dt.Rows[i]["TTBH"].ToString();
                m.EMNAME = dt.Rows[i]["EMNAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.GC = dt.Rows[i]["GC"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.IP = dt.Rows[i]["IP"].ToString();
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();
                m.USERPWD = dt.Rows[i]["USERPWD"].ToString();
                m.XH = dt.Rows[i]["XH"].ToString();
                m.PP = dt.Rows[i]["PP"].ToString();
                m.GD = dt.Rows[i]["GD"].ToString();
                m.JCJL = dt.Rows[i]["JCJL"].ToString();
                m.OBJID = dt.Rows[i]["OBJID"].ToString();
                m.TEMPLATEDID = dt.Rows[i]["TEMPLATEDID"].ToString();
                m.PORT = dt.Rows[i]["PORT"].ToString();
                m.TYPE = dt.Rows[i]["TYPE"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion



        #region 电子监控传递火情增、删、管理

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message ManagerMonitor(JC_MONITOR_Model m)
        {

            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "电子监控火情信息图片:" + m.IMBID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_MONITOR.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "电子监控火情信息图片:" + m.IMBID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_MONITOR.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }

            if (m.opMethod == "Man")
            {
                SystemCls.LogSave("5", "电子监控火情信息图片:" + m.IMBID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_MONITOR.Man(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");

        }
        #endregion

        #region  根据查询条件获取某一条火情信息记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_MONITOR_Model getModelMonitor(JC_MONITOR_SW sw)
        {
            DataTable dt = BaseDT.JC_MONITOR.getDT(sw);
            JC_MONITOR_Model m = new JC_MONITOR_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;

                m.IMBID = dt.Rows[i]["IMBID"].ToString();
                m.TTBH = dt.Rows[i]["TTBH"].ToString();
                m.IMBTIME = ClsSwitch.SwitTM(dt.Rows[i]["IMBTIME"].ToString());
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.SPJ = dt.Rows[i]["SPJ"].ToString();
                m.FYJ = dt.Rows[i]["FYJ"].ToString();
                m.IMBIMGURL = dt.Rows[i]["IMBIMGURL"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();


                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { USERID = m.MANUSERID });
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                    dtUser.Clear();
                    dtUser.Dispose();

                }
                m.BasicInfoModel = getModel(new JC_MONITOR_BASICINFO_SW { TTBH = m.TTBH });
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取火情列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_MONITOR_Model> getListModelMonitor(JC_MONITOR_SW sw)
        {
            DataTable dt = BaseDT.JC_MONITOR.getDT(sw);//列表
            var result = new List<JC_MONITOR_Model>();

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_MONITOR_Model m = new JC_MONITOR_Model();
                m.IMBID = dt.Rows[i]["IMBID"].ToString();
                m.TTBH = dt.Rows[i]["TTBH"].ToString();
                m.IMBTIME = ClsSwitch.SwitTM(dt.Rows[i]["IMBTIME"].ToString());
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.SPJ = dt.Rows[i]["SPJ"].ToString();
                m.FYJ = dt.Rows[i]["FYJ"].ToString();
                m.IMBIMGURL = dt.Rows[i]["IMBIMGURL"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }

                }
                m.BasicInfoModel = getModel(new JC_MONITOR_BASICINFO_SW { TTBH = m.TTBH });
                result.Add(m);
            }
            dtUser.Clear();
            dtUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion
    }
}
