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
    /// 灾损_损失分类_火灾扑救费用_工资伙食费(P3)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P3Cls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_ATTACK_P3_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P3.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P3.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P3.Del(m);
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
        public static FIRELOST_LOSTTYPE_ATTACK_P3_Model getModel(FIRELOST_LOSTTYPE_ATTACK_P3_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P3.getDT(sw);//列表
            FIRELOST_LOSTTYPE_ATTACK_P3_Model m = new FIRELOST_LOSTTYPE_ATTACK_P3_Model();
            DataTable dt515 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "515" });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.P3ID = dt.Rows[i]["P3ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P3NAME = dt.Rows[i]["P3NAME"].ToString();
                m.P3CODE = dt.Rows[i]["P3CODE"].ToString();
                m.P3CODENAME = BaseDT.T_SYS_DICT.getName(dt515, m.P3CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P3MONEY = dt.Rows[i]["P3MONEY"].ToString();
                m.ATTACKNUMBERS = dt.Rows[i]["ATTACKNUMBERS"].ToString();
                m.ATTACKDAYS = dt.Rows[i]["ATTACKDAYS"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt515.Clear();
            dt515.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_ATTACK_P3_Model> getListModel(FIRELOST_LOSTTYPE_ATTACK_P3_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_ATTACK_P3_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_ATTACK_P3.getDT(sw);//列表
            DataTable dt515 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "515" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_ATTACK_P3_Model m = new FIRELOST_LOSTTYPE_ATTACK_P3_Model();
                m.P3ID = dt.Rows[i]["P3ID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.P3NAME = dt.Rows[i]["P3NAME"].ToString();
                m.P3CODE = dt.Rows[i]["P3CODE"].ToString();
                m.P3CODENAME = BaseDT.T_SYS_DICT.getName(dt515, m.P3CODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.P3MONEY = dt.Rows[i]["P3MONEY"].ToString();
                m.ATTACKNUMBERS = dt.Rows[i]["ATTACKNUMBERS"].ToString();
                m.ATTACKDAYS = dt.Rows[i]["ATTACKDAYS"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt515.Clear();
            dt515.Dispose();
            return result;
        }
        #endregion
    }
}
