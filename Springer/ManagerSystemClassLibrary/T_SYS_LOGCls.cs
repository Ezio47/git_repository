using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;


namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    public class T_SYS_LOGCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_LOG_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.T_SYS_LOG.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.T_SYS_LOG.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.T_SYS_LOG.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_LOG_Model getModel(T_SYS_LOG_SW sw)
        {
            DataTable dtLog = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "日志类别" });
            DataTable dt = BaseDT.T_SYS_LOG.getDT(sw);
            T_SYS_LOG_Model m = new T_SYS_LOG_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                DataTable dtUserRole = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { USERID = dt.Rows[i]["LOGINUSERID"].ToString() });
                m.LOGTYPENAME=BaseDT.T_SYS_DICT.getName(dtLog, dt.Rows[i]["LOGTYPE"].ToString());
                m.LOGID = dt.Rows[i]["LOGID"].ToString();
                m.LOGTYPE = dt.Rows[i]["LOGTYPE"].ToString();
                m.OPERATION = dt.Rows[i]["OPERATION"].ToString();
                m.OPERATIONCONTENT = dt.Rows[i]["OPERATIONCONTENT"].ToString();
                m.LOGINUSERID = dt.Rows[i]["LOGINUSERID"].ToString();
                m.USERIP = dt.Rows[i]["USERIP"].ToString();
                m.OPERATETIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["OPERATETIME"].ToString());
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();

                m.LOGINUSERName = BaseDT.T_SYSSEC_USER.getName(dtUserRole, dt.Rows[i]["LOGINUSERID"].ToString());
                dtUserRole.Clear();
                dtUserRole.Dispose();
            }
            dtLog.Clear();
            dtLog.Dispose();
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取分页
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_LOG_Model> getListPagerModel(T_SYS_LOG_SW sw, out int total)
        {
            var result = new List<T_SYS_LOG_Model>();

            DataTable dtLog = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "日志类别" });
            DataTable dt = BaseDT.T_SYS_LOG.getDT(sw, out total);//列表

            string uidList = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(uidList))
                    uidList += dt.Rows[i]["LOGINUSERID"].ToString();
                else
                    uidList += "," + dt.Rows[i]["LOGINUSERID"].ToString();
            }
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { USERID = uidList });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_LOG_Model m=new T_SYS_LOG_Model();
             m.LOGTYPENAME=    BaseDT.T_SYS_DICT.getName(dtLog, dt.Rows[i]["LOGTYPE"].ToString());
               m.OPERATION= dt.Rows[i]["OPERATION"].ToString();
               m.LOGINUSERName = BaseDT.T_SYSSEC_USER.getName(dtUser, dt.Rows[i]["LOGINUSERID"].ToString());
                m.USERIP= dt.Rows[i]["USERIP"].ToString();
                m.OPERATETIME= PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["OPERATETIME"].ToString());
                m.LOGID = dt.Rows[i]["LOGID"].ToString();
                result.Add(m);
            }
            dtLog.Clear();
            dtLog.Dispose();
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return result;
        }
        #endregion
        
    }
}
