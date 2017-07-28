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
    /// 灾损_损失分类_火灾扑救费用_组织管理费(P5)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P5Cls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_ATTACK_P5_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P5.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P5.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P5.Del(m);
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
        public static FIRELOST_LOSTTYPE_ATTACK_P5_Model getModel(FIRELOST_LOSTTYPE_ATTACK_P5_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P5.getDT(sw);//列表
            FIRELOST_LOSTTYPE_ATTACK_P5_Model m = new FIRELOST_LOSTTYPE_ATTACK_P5_Model();
            DataTable dt517 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "517" });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.P5ID = dt.Rows[i]["P5ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P5NAME = dt.Rows[i]["P5NAME"].ToString();
                m.P5CODE = dt.Rows[i]["P5CODE"].ToString();
                m.P5CODENAME = BaseDT.T_SYS_DICT.getName(dt517, m.P5CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P5MONEY = dt.Rows[i]["P5MONEY"].ToString();
                m.ATTACKDAYS = dt.Rows[i]["ATTACKDAYS"].ToString();
                m.ELSEMONEY = dt.Rows[i]["ELSEMONEY"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt517.Clear();
            dt517.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_ATTACK_P5_Model> getListModel(FIRELOST_LOSTTYPE_ATTACK_P5_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_ATTACK_P5_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P5.getDT(sw);//列表
            DataTable dt517 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "517" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_ATTACK_P5_Model m = new FIRELOST_LOSTTYPE_ATTACK_P5_Model();
                m.P5ID = dt.Rows[i]["P5ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P5NAME = dt.Rows[i]["P5NAME"].ToString();
                m.P5CODE = dt.Rows[i]["P5CODE"].ToString();
                m.P5CODENAME = BaseDT.T_SYS_DICT.getName(dt517, m.P5CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P5MONEY = dt.Rows[i]["P5MONEY"].ToString();
                m.ATTACKDAYS = dt.Rows[i]["ATTACKDAYS"].ToString();
                m.ELSEMONEY = dt.Rows[i]["ELSEMONEY"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt517.Clear();
            dt517.Dispose();
            return result;
        }
        #endregion
    }
}
