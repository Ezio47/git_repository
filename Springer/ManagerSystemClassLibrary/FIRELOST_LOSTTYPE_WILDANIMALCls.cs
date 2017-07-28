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
    /// 灾损_损失分类_野生动物损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_WILDANIMALCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_WILDANIMAL_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_WILDANIMAL.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_WILDANIMAL.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_WILDANIMAL.Del(m);
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
        public static FIRELOST_LOSTTYPE_WILDANIMAL_Model getModel(FIRELOST_LOSTTYPE_WILDANIMAL_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_WILDANIMAL.getDT(sw);//列表
            FIRELOST_LOSTTYPE_WILDANIMAL_Model m = new FIRELOST_LOSTTYPE_WILDANIMAL_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.WILDANIMALID = dt.Rows[i]["WILDANIMALID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.WILDANIMALNAME = dt.Rows[i]["WILDANIMALNAME"].ToString();
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.WILDANIMALCOUNT = dt.Rows[i]["WILDANIMALCOUNT"].ToString();
                m.WILDANIMALPRICE = dt.Rows[i]["WILDANIMALPRICE"].ToString();
                m.RESIDUALVALUE = dt.Rows[i]["RESIDUALVALUE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_WILDANIMAL_Model> getListModel(FIRELOST_LOSTTYPE_WILDANIMAL_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_WILDANIMAL_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_WILDANIMAL.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_WILDANIMAL_Model m = new FIRELOST_LOSTTYPE_WILDANIMAL_Model();
                m.WILDANIMALID = dt.Rows[i]["WILDANIMALID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.WILDANIMALNAME = dt.Rows[i]["WILDANIMALNAME"].ToString();
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.WILDANIMALCOUNT = dt.Rows[i]["WILDANIMALCOUNT"].ToString();
                m.WILDANIMALPRICE = dt.Rows[i]["WILDANIMALPRICE"].ToString();
                m.RESIDUALVALUE = dt.Rows[i]["RESIDUALVALUE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
