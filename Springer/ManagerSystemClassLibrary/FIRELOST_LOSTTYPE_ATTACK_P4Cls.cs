using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_消防器材消耗表(P4)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P4Cls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_ATTACK_P4_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P4.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P4.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P4.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static FIRELOST_LOSTTYPE_ATTACK_P4_Model getModel(FIRELOST_LOSTTYPE_ATTACK_P4_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P4.getDT(sw);//列表
            FIRELOST_LOSTTYPE_ATTACK_P4_Model m = new FIRELOST_LOSTTYPE_ATTACK_P4_Model();
            DataTable dt516 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "516" });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.P4ID = dt.Rows[i]["P4ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P4NAME = dt.Rows[i]["P4NAME"].ToString();
                m.P4CODE = dt.Rows[i]["P4CODE"].ToString();
                m.P4CODENAME = BaseDT.T_SYS_DICT.getName(dt516, m.P4CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();               
                m.NOWPRICE = dt.Rows[i]["NOWPRICE"].ToString();
                m.P4COUNT = dt.Rows[i]["P4COUNT"].ToString();
                m.P4UNIT = dt.Rows[i]["P4UNIT"].ToString();
                m.YEARAVGDEPRECIATIONRATE = dt.Rows[i]["YEARAVGDEPRECIATIONRATE"].ToString();
                m.HAVEUSEYEAR = dt.Rows[i]["HAVEUSEYEAR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt516.Clear();
            dt516.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_ATTACK_P4_Model> getListModel(FIRELOST_LOSTTYPE_ATTACK_P4_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_ATTACK_P4_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P4.getDT(sw);//列表
            DataTable dt516 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "516" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_ATTACK_P4_Model m = new FIRELOST_LOSTTYPE_ATTACK_P4_Model();
                m.P4ID = dt.Rows[i]["P4ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P4NAME = dt.Rows[i]["P4NAME"].ToString();
                m.P4CODE = dt.Rows[i]["P4CODE"].ToString();
                m.P4CODENAME = BaseDT.T_SYS_DICT.getName(dt516, m.P4CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.NOWPRICE = dt.Rows[i]["NOWPRICE"].ToString();
                m.P4COUNT = dt.Rows[i]["P4COUNT"].ToString();
                m.P4UNIT = dt.Rows[i]["P4UNIT"].ToString();
                m.YEARAVGDEPRECIATIONRATE = dt.Rows[i]["YEARAVGDEPRECIATIONRATE"].ToString();
                m.HAVEUSEYEAR = dt.Rows[i]["HAVEUSEYEAR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt516.Clear();
            dt516.Dispose();
            return result;
        }
        #endregion
    }
}
