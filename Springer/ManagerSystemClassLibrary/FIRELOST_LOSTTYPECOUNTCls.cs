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
    /// 灾损_损失分类统计表
    /// </summary>
    public class FIRELOST_LOSTTYPECOUNTCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPECOUNT_Model m)
        {
            Message msg = BaseDT.FIRELOST_LOSTTYPECOUNT.Save(m);
            return new Message(msg.Success, msg.Msg, msg.Url);
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static FIRELOST_LOSTTYPECOUNT_Model getModel(FIRELOST_LOSTTYPECOUNT_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPECOUNT.getDT(sw);//列表
            FIRELOST_LOSTTYPECOUNT_Model m = new FIRELOST_LOSTTYPECOUNT_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.FIRELOST_LOSTTYPECOUNTID = dt.Rows[i]["FIRELOST_LOSTTYPECOUNTID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.FIRELOSETYPECODE = dt.Rows[i]["FIRELOSETYPECODE"].ToString();
                m.LOSEMONEY = dt.Rows[i]["LOSEMONEY"].ToString();
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
        public static IEnumerable<FIRELOST_LOSTTYPECOUNT_Model> getListModel(FIRELOST_LOSTTYPECOUNT_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPECOUNT_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPECOUNT.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPECOUNT_Model m = new FIRELOST_LOSTTYPECOUNT_Model();
                m.FIRELOST_LOSTTYPECOUNTID = dt.Rows[i]["FIRELOST_LOSTTYPECOUNTID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.FIRELOSETYPECODE = dt.Rows[i]["FIRELOSETYPECODE"].ToString();
                m.LOSEMONEY = dt.Rows[i]["LOSEMONEY"].ToString();
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
