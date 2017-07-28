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
    /// 灾损_损失分类_火灾扑救费用_车马船交通费(P1)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P1Cls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_ATTACK_P1_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P1.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P1.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P1.Del(m);
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
        public static FIRELOST_LOSTTYPE_ATTACK_P1_Model getModel(FIRELOST_LOSTTYPE_ATTACK_P1_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P1.getDT(sw);//列表
            FIRELOST_LOSTTYPE_ATTACK_P1_Model m = new FIRELOST_LOSTTYPE_ATTACK_P1_Model();
            DataTable dt513 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "513" });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.P1ID = dt.Rows[i]["P1ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P1NAME = dt.Rows[i]["P1NAME"].ToString();
                m.P1CODE = dt.Rows[i]["P1CODE"].ToString();
                m.P1CODENAME = BaseDT.T_SYS_DICT.getName(dt513, m.P1CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P1COUNT = dt.Rows[i]["P1COUNT"].ToString();
                m.P1UNIT = dt.Rows[i]["P1UNIT"].ToString();
                m.P1PRICE = dt.Rows[i]["P1PRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt513.Clear();
            dt513.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_ATTACK_P1_Model> getListModel(FIRELOST_LOSTTYPE_ATTACK_P1_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_ATTACK_P1_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P1.getDT(sw);//列表
            DataTable dt513 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "513" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_ATTACK_P1_Model m = new FIRELOST_LOSTTYPE_ATTACK_P1_Model();
                m.P1ID = dt.Rows[i]["P1ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P1NAME = dt.Rows[i]["P1NAME"].ToString();
                m.P1CODE = dt.Rows[i]["P1CODE"].ToString();
                m.P1CODENAME = BaseDT.T_SYS_DICT.getName(dt513, m.P1CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P1COUNT = dt.Rows[i]["P1COUNT"].ToString();
                m.P1UNIT = dt.Rows[i]["P1UNIT"].ToString();
                m.P1PRICE = dt.Rows[i]["P1PRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt513.Clear();
            dt513.Dispose();
            return result;
        }
        #endregion
    }
}
