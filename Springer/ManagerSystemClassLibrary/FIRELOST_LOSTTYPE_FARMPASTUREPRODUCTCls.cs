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
    /// 灾损_损失分类_农牧产品损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT.Del(m);
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
        public static FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model getModel(FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT.getDT(sw);//列表
            FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model m = new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model();
            DataTable dt505 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "505" });//农牧产品损失类别
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID = dt.Rows[i]["FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.FARMPASTUREPRODUCNAME = dt.Rows[i]["FARMPASTUREPRODUCNAME"].ToString();
                m.FARMPASTUREPRODUCCODE = dt.Rows[i]["FARMPASTUREPRODUCCODE"].ToString();
                m.PASTUREPRODUCCODENAME = BaseDT.T_SYS_DICT.getName(dt505, m.FARMPASTUREPRODUCCODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.LOSECOUNT = dt.Rows[i]["LOSECOUNT"].ToString();
                m.BASEPRICE = dt.Rows[i]["BASEPRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt505.Clear();
            dt505.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model> getListModel(FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT.getDT(sw);//列表
            DataTable dt505 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "505" });//农牧产品损失类别
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model m = new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model();
                m.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID = dt.Rows[i]["FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.FARMPASTUREPRODUCNAME = dt.Rows[i]["FARMPASTUREPRODUCNAME"].ToString();
                m.FARMPASTUREPRODUCCODE = dt.Rows[i]["FARMPASTUREPRODUCCODE"].ToString();
                m.PASTUREPRODUCCODENAME = BaseDT.T_SYS_DICT.getName(dt505, m.FARMPASTUREPRODUCCODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.LOSECOUNT = dt.Rows[i]["LOSECOUNT"].ToString();
                m.BASEPRICE = dt.Rows[i]["BASEPRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt505.Clear();
            dt505.Dispose();
            return result;
        }
        #endregion
    }
}
