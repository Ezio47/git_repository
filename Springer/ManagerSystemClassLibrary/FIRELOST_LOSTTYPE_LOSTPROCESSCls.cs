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
    /// 灾损_损失分类_灾后处理费用表
    /// </summary>
    public class FIRELOST_LOSTTYPE_LOSTPROCESSCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_LOSTPROCESS_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_LOSTPROCESS.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_LOSTPROCESS.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_LOSTPROCESS.Del(m);
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
        public static FIRELOST_LOSTTYPE_LOSTPROCESS_Model getModel(FIRELOST_LOSTTYPE_LOSTPROCESS_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_LOSTPROCESS.getDT(sw);//列表
            DataTable dt511 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "511" });//灾后处理费用
            FIRELOST_LOSTTYPE_LOSTPROCESS_Model m = new FIRELOST_LOSTTYPE_LOSTPROCESS_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.LOSTPROCESSID = dt.Rows[i]["LOSTPROCESSID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.LOSTPROCESSNAME = dt.Rows[i]["LOSTPROCESSNAME"].ToString();
                m.LOSTPROCESSCODE = dt.Rows[i]["LOSTPROCESSCODE"].ToString();
                m.LOSTPROCESSCODENAME = BaseDT.T_SYS_DICT.getName(dt511, m.LOSTPROCESSCODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt511.Clear();
            dt511.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_LOSTPROCESS_Model> getListModel(FIRELOST_LOSTTYPE_LOSTPROCESS_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_LOSTPROCESS_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_LOSTPROCESS.getDT(sw);//列表
            DataTable dt511 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "511" });//灾后处理费用
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_LOSTPROCESS_Model m = new FIRELOST_LOSTTYPE_LOSTPROCESS_Model();
                m.LOSTPROCESSID = dt.Rows[i]["LOSTPROCESSID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.LOSTPROCESSNAME = dt.Rows[i]["LOSTPROCESSNAME"].ToString();
                m.LOSTPROCESSCODE = dt.Rows[i]["LOSTPROCESSCODE"].ToString();
                m.LOSTPROCESSCODENAME = BaseDT.T_SYS_DICT.getName(dt511, m.LOSTPROCESSCODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt511.Clear();
            dt511.Dispose();
            return result;
        }
        #endregion
    }
}
