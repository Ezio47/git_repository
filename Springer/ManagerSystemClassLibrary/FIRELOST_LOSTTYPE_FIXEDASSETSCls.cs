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
    /// 灾损_损失分类_固定资产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_FIXEDASSETSCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_FIXEDASSETS_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_FIXEDASSETS.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_FIXEDASSETS.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_FIXEDASSETS.Del(m);
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
        public static FIRELOST_LOSTTYPE_FIXEDASSETS_Model getModel(FIRELOST_LOSTTYPE_FIXEDASSETS_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_FIXEDASSETS.getDT(sw);//列表
            FIRELOST_LOSTTYPE_FIXEDASSETS_Model m = new FIRELOST_LOSTTYPE_FIXEDASSETS_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.FIRELOST_LOSTTYPE_FIXEDASSETSID = dt.Rows[i]["FIRELOST_LOSTTYPE_FIXEDASSETSID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.FIXEDASSETSNAME = dt.Rows[i]["FIXEDASSETSNAME"].ToString();
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.RESETVALUE = dt.Rows[i]["RESETVALUE"].ToString();
                m.YEARAVGDEPRECIATIONRATE = dt.Rows[i]["YEARAVGDEPRECIATIONRATE"].ToString();
                m.HAVEUSEYEAR = dt.Rows[i]["HAVEUSEYEAR"].ToString();
                m.BURNRATE = dt.Rows[i]["BURNRATE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_FIXEDASSETS_Model> getListModel(FIRELOST_LOSTTYPE_FIXEDASSETS_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_FIXEDASSETS_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_FIXEDASSETS.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_FIXEDASSETS_Model m = new FIRELOST_LOSTTYPE_FIXEDASSETS_Model();
                m.FIRELOST_LOSTTYPE_FIXEDASSETSID = dt.Rows[i]["FIRELOST_LOSTTYPE_FIXEDASSETSID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.FIXEDASSETSNAME = dt.Rows[i]["FIXEDASSETSNAME"].ToString();
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.RESETVALUE = dt.Rows[i]["RESETVALUE"].ToString();
                m.YEARAVGDEPRECIATIONRATE = dt.Rows[i]["YEARAVGDEPRECIATIONRATE"].ToString();
                m.HAVEUSEYEAR = dt.Rows[i]["HAVEUSEYEAR"].ToString();
                m.BURNRATE = dt.Rows[i]["BURNRATE"].ToString();
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
