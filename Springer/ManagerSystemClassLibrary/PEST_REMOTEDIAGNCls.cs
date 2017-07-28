using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 远程诊断表
    /// </summary>
    public class PEST_REMOTEDIAGNCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REMOTEDIAGN_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_REMOTEDIAGN.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_REMOTEDIAGN.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "MdyZT")
            {
                Message msg = BaseDT.PEST_REMOTEDIAGN.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_REMOTEDIAGN.Del(m);
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
        public static PEST_REMOTEDIAGN_Model getModel(PEST_REMOTEDIAGN_SW sw)
        {
            DataTable dt = BaseDT.PEST_REMOTEDIAGN.getDT(sw);//列表
            DataTable dt122 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "122" });//诊断状态
            PEST_REMOTEDIAGN_Model m = new PEST_REMOTEDIAGN_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_REMOTEDIAGNID = dt.Rows[i]["PEST_REMOTEDIAGNID"].ToString();
                m.DIAGNTITLE = dt.Rows[i]["DIAGNTITLE"].ToString();
                m.DIAGNCONTENT = dt.Rows[i]["DIAGNCONTENT"].ToString();
                m.DIAGNTIME = ClsSwitch.SwitMN(dt.Rows[i]["DIAGNTIME"].ToString());
                m.DIAGNEXPERTS = dt.Rows[i]["DIAGNEXPERTS"].ToString();
                m.DIAGNSTATUS = dt.Rows[i]["DIAGNSTATUS"].ToString();
                m.DIAGNSTATUSName = BaseDT.T_SYS_DICT.getName(dt122, m.DIAGNSTATUS);
                m.DIAGNRESULT = dt.Rows[i]["DIAGNRESULT"].ToString();
                m.DIAGNSPONSERUID = dt.Rows[i]["DIAGNSPONSERUID"].ToString();
                if (!string.IsNullOrEmpty(m.DIAGNSPONSERUID))
                    m.DIAGNSPONSERNAME = T_SYSSEC_IPSUSERCls.getname(m.DIAGNSPONSERUID);
                m.DIAGNSPONSERTIME = ClsSwitch.SwitMN(dt.Rows[i]["DIAGNSPONSERTIME"].ToString());
            }
            dt.Clear();
            dt.Dispose();
            dt122.Clear();
            dt122.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<PEST_REMOTEDIAGN_Model> getListModel(PEST_REMOTEDIAGN_SW sw, out int total)
        {
            var result = new List<PEST_REMOTEDIAGN_Model>();
            DataTable dt = BaseDT.PEST_REMOTEDIAGN.getDT(sw, out total);//列表
            DataTable dt122 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "122" });//诊断状态
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REMOTEDIAGN_Model m = new PEST_REMOTEDIAGN_Model();
                m.PEST_REMOTEDIAGNID = dt.Rows[i]["PEST_REMOTEDIAGNID"].ToString();
                m.DIAGNTITLE = dt.Rows[i]["DIAGNTITLE"].ToString();
                m.DIAGNCONTENT = dt.Rows[i]["DIAGNCONTENT"].ToString();
                m.DIAGNTIME = ClsSwitch.SwitMN(dt.Rows[i]["DIAGNTIME"].ToString());
                m.DIAGNEXPERTS = dt.Rows[i]["DIAGNEXPERTS"].ToString();
                m.DIAGNSTATUS = dt.Rows[i]["DIAGNSTATUS"].ToString();
                m.DIAGNSTATUSName = BaseDT.T_SYS_DICT.getName(dt122, m.DIAGNSTATUS);
                m.DIAGNRESULT = dt.Rows[i]["DIAGNRESULT"].ToString();
                m.DIAGNSPONSERUID = dt.Rows[i]["DIAGNSPONSERUID"].ToString();
                if (!string.IsNullOrEmpty(m.DIAGNSPONSERUID))
                    m.DIAGNSPONSERNAME = T_SYSSEC_IPSUSERCls.getname(m.DIAGNSPONSERUID);
                m.DIAGNSPONSERTIME = ClsSwitch.SwitMN(dt.Rows[i]["DIAGNSPONSERTIME"].ToString());
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt122.Clear();
            dt122.Dispose();
            return result;
        }
        #endregion
    }
}
