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
    /// 监测_群众报警表
    /// </summary>
    public class JC_PERALARMCls
    {
        #region 增、删、改、管理

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_PERALARM_Model m)
        {
            if (m.opMethod == "Add")
            {
                    string[] arr = m.BYORGNOLIST.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        m.BYORGNO = arr[i];
                       BaseDT.JC_PERALARM.Add(m);
                    }                  
                SystemCls.LogSave("3", "群众报警:" + m.PERALARMID, ClsStr.getModelContent(m));
                return new Message(true, "添加成功", m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "群众报警:" + m.PERALARMID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_PERALARM.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "群众报警:" + m.PERALARMID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_PERALARM.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Man")
            {
                SystemCls.LogSave("5", "群众报警:" + m.PERALARMID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_PERALARM.Man(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }
        #endregion

  
        #region  根据查询条件获取某一条记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_PERALARM_Model getModel(JC_PERALARM_SW sw)
        {
            DataTable dt = BaseDT.JC_PERALARM.getDT(sw);
            JC_PERALARM_Model m = new JC_PERALARM_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PERALARMID = dt.Rows[i]["PERALARMID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.PERALARMPHONE = dt.Rows[i]["PERALARMPHONE"].ToString();
                m.PERALARMNAME = dt.Rows[i]["PERALARMNAME"].ToString();
                m.PERALARMTIME = ClsSwitch.SwitTM(dt.Rows[i]["PERALARMTIME"].ToString());
                m.PERALARMADDRESS = dt.Rows[i]["PERALARMADDRESS"].ToString();
                m.PERALARMCONTENT = dt.Rows[i]["PERALARMCONTENT"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.PEARLARMPRE = dt.Rows[i]["PEARLARMPRE"].ToString();
                m.PEARLARMISSUED = dt.Rows[i]["PEARLARMISSUED"].ToString();
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
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
            }

            dtORG.Clear();
            dtORG.Dispose();
            dt.Clear();
            dt.Dispose(); 
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_PERALARM_Model> getListModel(JC_PERALARM_SW sw)
        {
            DataTable dt = BaseDT.JC_PERALARM.getDT(sw);//列表
            var result = new List<JC_PERALARM_Model>();

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_PERALARM_Model m = new JC_PERALARM_Model();
                m.PERALARMID = dt.Rows[i]["PERALARMID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.PERALARMPHONE = dt.Rows[i]["PERALARMPHONE"].ToString();
                m.PERALARMNAME = dt.Rows[i]["PERALARMNAME"].ToString();
                m.PERALARMTIME = ClsSwitch.SwitTM(dt.Rows[i]["PERALARMTIME"].ToString());
                m.PERALARMADDRESS = dt.Rows[i]["PERALARMADDRESS"].ToString();
                m.PERALARMCONTENT = dt.Rows[i]["PERALARMCONTENT"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.PEARLARMPRE = dt.Rows[i]["PEARLARMPRE"].ToString();
                m.PEARLARMISSUED = dt.Rows[i]["PEARLARMISSUED"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                }
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取列表（分页）
        /// <summary>
        /// 获取用户列表（分页）
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<JC_PERALARM_Model> getListModelPager(JC_PERALARM_SW sw, out int total)
        {
            var result = new List<JC_PERALARM_Model>();

            DataTable dt = BaseDT.JC_PERALARM.getDT(sw, out total);//用户列表

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_PERALARM_Model m = new JC_PERALARM_Model();
                m.PERALARMID = dt.Rows[i]["PERALARMID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.PERALARMPHONE = dt.Rows[i]["PERALARMPHONE"].ToString();
                m.PERALARMNAME = dt.Rows[i]["PERALARMNAME"].ToString();
                m.PERALARMTIME = ClsSwitch.SwitTM(dt.Rows[i]["PERALARMTIME"].ToString());
                m.PERALARMADDRESS = dt.Rows[i]["PERALARMADDRESS"].ToString();
                m.PERALARMCONTENT = dt.Rows[i]["PERALARMCONTENT"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.PEARLARMPRE = dt.Rows[i]["PEARLARMPRE"].ToString();
                m.PEARLARMISSUED = dt.Rows[i]["PEARLARMISSUED"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                }
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
