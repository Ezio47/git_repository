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
    /// 灾损_损失分类_人员伤亡损失明细表
    /// </summary>
    public class FIRELOST_LOSTTYPE_CASUALTYDETAILCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m)
        {
            if (m.opMethod == "Add" || m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_CASUALTYDETAIL.Save(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_CASUALTYDETAIL.Del(m);
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
        public static FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model getModel(FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_CASUALTYDETAIL.getDT(sw);//列表
            FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m = new FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.CASUALTYDETAILID = dt.Rows[i]["CASUALTYDETAILID"].ToString();
                m.FIRELOST_LOSTTYPE_CASUALTYID = dt.Rows[i]["FIRELOST_LOSTTYPE_CASUALTYID"].ToString();
                m.CASUALTYDETAILCODE = dt.Rows[i]["CASUALTYDETAILCODE"].ToString();
                m.CASUALTYDETAIMONEY = dt.Rows[i]["CASUALTYDETAIMONEY"].ToString();
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
        public static IEnumerable<FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model> getListModel(FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_CASUALTYDETAIL.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m = new FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model();
                m.CASUALTYDETAILID = dt.Rows[i]["CASUALTYDETAILID"].ToString();
                m.FIRELOST_LOSTTYPE_CASUALTYID = dt.Rows[i]["FIRELOST_LOSTTYPE_CASUALTYID"].ToString();
                m.CASUALTYDETAILCODE = dt.Rows[i]["CASUALTYDETAILCODE"].ToString();
                m.CASUALTYDETAIMONEY = dt.Rows[i]["CASUALTYDETAIMONEY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
