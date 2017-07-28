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
    /// 灾损_损失分类_火灾扑救费用_燃料材料费(P2)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P2Cls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_ATTACK_P2_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P2.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P2.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P2.Del(m);
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
        public static FIRELOST_LOSTTYPE_ATTACK_P2_Model getModel(FIRELOST_LOSTTYPE_ATTACK_P2_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P2.getDT(sw);//列表
            FIRELOST_LOSTTYPE_ATTACK_P2_Model m = new FIRELOST_LOSTTYPE_ATTACK_P2_Model();
            DataTable dt514 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "514" });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.P2ID = dt.Rows[i]["P2ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P2NAME = dt.Rows[i]["P2NAME"].ToString();
                m.P2CODE = dt.Rows[i]["P2CODE"].ToString();
                m.P2CODENAME = BaseDT.T_SYS_DICT.getName(dt514, m.P2CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P2COUNT = dt.Rows[i]["P2COUNT"].ToString();
                m.P2UNIT = dt.Rows[i]["P2UNIT"].ToString();
                m.NOWPRICE = dt.Rows[i]["NOWPRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt514.Clear();
            dt514.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_ATTACK_P2_Model> getListModel(FIRELOST_LOSTTYPE_ATTACK_P2_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_ATTACK_P2_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P2.getDT(sw);//列表
            DataTable dt514 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "514" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_ATTACK_P2_Model m = new FIRELOST_LOSTTYPE_ATTACK_P2_Model();
                m.P2ID = dt.Rows[i]["P2ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P2NAME = dt.Rows[i]["P2NAME"].ToString();
                m.P2CODE = dt.Rows[i]["P2CODE"].ToString();
                m.P2CODENAME = BaseDT.T_SYS_DICT.getName(dt514, m.P2CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P2COUNT = dt.Rows[i]["P2COUNT"].ToString();
                m.P2UNIT = dt.Rows[i]["P2UNIT"].ToString();
                m.NOWPRICE = dt.Rows[i]["NOWPRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt514.Clear();
            dt514.Dispose();
            return result;
        }
        #endregion
    }
}
