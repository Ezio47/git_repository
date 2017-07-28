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
    /// 热点公共类
    /// </summary>
    public class T_IPS_HOTSCls
    {

        #region 删除、处理

        /// <summary>
        /// 删除、处理
        /// </summary>
        /// <param name="m">参见模型T_IPS_HOTS_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_IPS_HOTS_Model m)
        {
            if (m.opMethod == "Del")
            {
                T_IPS_HOTS_Model m1 = getModel(new T_IPS_HOTS_SW { HOTSID = m.HOTSID });
                SystemCls.LogSave("5", "热点处理:" + m1.FXSJ, ClsStr.getModelContent(m1));
                Message msgUser = BaseDT.T_IPS_HOTS.Del(m);

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "Man")
            {
                T_IPS_HOTS_Model m1 = getModel(new T_IPS_HOTS_SW { HOTSID = m.HOTSID });
                SystemCls.LogSave("4", "热点处理:" + m1.FXSJ, ClsStr.getModelContent(m1));
                Message msgUser = BaseDT.T_IPS_HOTS.Man(m);

                return new Message(msgUser.Success, msgUser.Msg, "");
            }

            return new Message(false, "无效操作", "");


        }
        #endregion

        #region 获取一条数据
        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <example>
        /// sw.ALARMID       热点ID
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_HOTS_SW</param>
        /// <returns>参见模型T_IPS_HOTS_Model</returns>
        public static T_IPS_HOTS_Model getModel(T_IPS_HOTS_SW sw)
        {
            DataTable dt = BaseDT.T_IPS_HOTS.getDT(sw);
            T_IPS_HOTS_Model m = new T_IPS_HOTS_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.HOTSID = dt.Rows[i]["HOTSID"].ToString();
                m.BH = dt.Rows[i]["BH"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.HLY = dt.Rows[i]["HLY"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                //******************计算坐标偏移量
                string[] arr = PublicCls.switJWD(m.WD, m.JD);
                m.WD = arr[0];
                m.JD = arr[1];
                //******************计算坐标偏移量 End
                m.XS = dt.Rows[i]["XS"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.FXSJ = dt.Rows[i]["FXSJ"].ToString();
                if (string.IsNullOrEmpty(m.FXSJ) == false)
                    m.FXSJ = PublicClassLibrary.ClsSwitch.SwitTM(m.FXSJ);
                m.SBSJ = dt.Rows[i]["SBSJ"].ToString();
                if (string.IsNullOrEmpty(m.SBSJ) == false)
                    m.SBSJ = PublicClassLibrary.ClsSwitch.SwitTM(m.SBSJ); 
                m.BZW = dt.Rows[i]["BZW"].ToString();
                m.FJBH = dt.Rows[i]["FJBH"].ToString();
                m.WLBH = dt.Rows[i]["WLBH"].ToString();
                m.XZQH = dt.Rows[i]["XZQH"].ToString();
                m.CZW = dt.Rows[i]["CZW"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();

                if (!string.IsNullOrEmpty( m.MANUSERID))
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
                //权限获取
                //var userid = SystemCls.getUserID();
                //var rightsw = new T_SYSSEC_IPSUSER_SW();
                //rightsw.USERID = userid;
                //m.Rights = T_SYSSEC_RIGHTCls.getRightStrByUID(rightsw);

            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取多条数据
        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <example>
        /// sw.orgNo            机构编码，用于获取该机构编码下所有的热点信息
        /// sw.ALARMSTATE       处理状态 0未处理1已处理
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_HOTS_SW</param>
        /// <returns>参见模型T_IPS_HOTS_Model</returns>
        public static IEnumerable<T_IPS_HOTS_Model> getModelList(T_IPS_HOTS_SW sw)
        {
            //var userid = SystemCls.getUserID();
            //var rightsw = new T_SYSSEC_IPSUSER_SW();
            //rightsw.USERID = userid;

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            var result = new List<T_IPS_HOTS_Model>();
            DataTable dt = BaseDT.T_IPS_HOTS.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPS_HOTS_Model m = new T_IPS_HOTS_Model();
                m.HOTSID = dt.Rows[i]["HOTSID"].ToString();
                m.BH = dt.Rows[i]["BH"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.HLY = dt.Rows[i]["HLY"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.XS = dt.Rows[i]["XS"].ToString();
                //******************计算坐标偏移量
                string[] arr = PublicCls.switJWD(m.WD, m.JD);
                m.WD = arr[0];
                m.JD = arr[1];
                //******************计算坐标偏移量 End
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.FXSJ = dt.Rows[i]["FXSJ"].ToString();
                if (string.IsNullOrEmpty(m.FXSJ) == false)
                    m.FXSJ = PublicClassLibrary.ClsSwitch.SwitTM(m.FXSJ);
                m.SBSJ = dt.Rows[i]["SBSJ"].ToString();
                if (string.IsNullOrEmpty(m.SBSJ) == false)
                    m.SBSJ = PublicClassLibrary.ClsSwitch.SwitTM(m.SBSJ); 
                m.BZW = dt.Rows[i]["BZW"].ToString();
                m.FJBH = dt.Rows[i]["FJBH"].ToString();
                m.WLBH = dt.Rows[i]["WLBH"].ToString();
                m.XZQH = dt.Rows[i]["XZQH"].ToString();
                m.CZW = dt.Rows[i]["CZW"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();

                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }

                }

                //权限获取
                //m.Rights = T_SYSSEC_RIGHTCls.getRightStrByUID(rightsw);
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
