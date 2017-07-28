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
    /// 灾损_损失分类_停减产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_STOPREDUCTIONCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_LOSTTYPE_STOPREDUCTION_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_STOPREDUCTION.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_STOPREDUCTION.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_LOSTTYPE_STOPREDUCTION.Del(m);
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
        public static FIRELOST_LOSTTYPE_STOPREDUCTION_Model getModel(FIRELOST_LOSTTYPE_STOPREDUCTION_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_STOPREDUCTION.getDT(sw);//列表
            FIRELOST_LOSTTYPE_STOPREDUCTION_Model m = new FIRELOST_LOSTTYPE_STOPREDUCTION_Model();
            DataTable dt510 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "510" });//停(减)产损失类别
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.STOPREDUCTIONID = dt.Rows[i]["STOPREDUCTIONID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.STOPREDUCTIONNAME = dt.Rows[i]["STOPREDUCTIONNAME"].ToString();
                m.STOPREDUCTIONCODE = dt.Rows[i]["STOPREDUCTIONCODE"].ToString();
                m.STOPREDUCTIONCODENAME = BaseDT.T_SYS_DICT.getName(dt510, m.STOPREDUCTIONCODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.STOPREDUCTIONCOUNT = dt.Rows[i]["STOPREDUCTIONCOUNT"].ToString();
                m.STOPREDUCTIONTIME = dt.Rows[i]["STOPREDUCTIONTIME"].ToString();
                m.STOPREDUCTIONPRICE = dt.Rows[i]["STOPREDUCTIONPRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dt510.Clear();
            dt510.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_LOSTTYPE_STOPREDUCTION_Model> getListModel(FIRELOST_LOSTTYPE_STOPREDUCTION_SW sw)
        {
            var result = new List<FIRELOST_LOSTTYPE_STOPREDUCTION_Model>();
            DataTable dt = BaseDT.FIRELOST_LOSTTYPE_STOPREDUCTION.getDT(sw);//列表
            DataTable dt510 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "510" });//停(减)产损失类别
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_LOSTTYPE_STOPREDUCTION_Model m = new FIRELOST_LOSTTYPE_STOPREDUCTION_Model();
                m.STOPREDUCTIONID = dt.Rows[i]["STOPREDUCTIONID"].ToString();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.STOPREDUCTIONNAME = dt.Rows[i]["STOPREDUCTIONNAME"].ToString();
                m.STOPREDUCTIONCODE = dt.Rows[i]["STOPREDUCTIONCODE"].ToString();
                m.STOPREDUCTIONCODENAME = BaseDT.T_SYS_DICT.getName(dt510, m.STOPREDUCTIONCODE);
                m.LOSEMONEYCOUNT = dt.Rows[i]["LOSEMONEYCOUNT"].ToString();
                m.STOPREDUCTIONCOUNT = dt.Rows[i]["STOPREDUCTIONCOUNT"].ToString();
                m.STOPREDUCTIONTIME = dt.Rows[i]["STOPREDUCTIONTIME"].ToString();
                m.STOPREDUCTIONPRICE = dt.Rows[i]["STOPREDUCTIONPRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt510.Clear();
            dt510.Dispose();
            return result;
        }
        #endregion
    }
}
