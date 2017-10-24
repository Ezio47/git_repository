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
    /// 野生植物属性
    /// </summary>
   public class WILD_BOTANYPROPCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
       public static Message Manager(WILD_BOTANYPROP_Model m)
        {
            Message msg = BaseDT.WILD_BOTANYPROP.Save(m);
            return new Message(msg.Success, msg.Msg, msg.Url); ;
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
       public static WILD_BOTANYPROP_Model getModel(WILD_BOTANYPROP_SW sw)
        {
            DataTable dt = BaseDT.WILD_BOTANYPROP.getDT(sw);
            DataTable dt127 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "127" });//保护级别
            DataTable dt129 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "129" });//生存现状
            WILD_BOTANYPROP_Model m = new WILD_BOTANYPROP_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.WILD_BOTANYPROPID = dt.Rows[i]["WILD_BOTANYPROPID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.LIVINGSTATUSCODE = dt.Rows[i]["LIVINGSTATUSCODE"].ToString();
                m.PROTECTIONLEVELCODE = dt.Rows[i]["PROTECTIONLEVELCODE"].ToString();
                m.BIOLOGICALTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.BIOLOGICALTYPECODE);
                m.PROTECTIONLEVELName = BaseDT.T_SYS_DICT.getName(dt127, m.PROTECTIONLEVELCODE);
                m.LIVINGSTATUSName = BaseDT.T_SYS_DICT.getName(dt129, m.LIVINGSTATUSCODE);
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
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
       public static IEnumerable<WILD_BOTANYPROP_Model> getListModel(WILD_BOTANYPROP_SW sw)
        {
            var result = new List<WILD_BOTANYPROP_Model>();
            DataTable dt127 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "127" });//保护级别
            DataTable dt129 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "129" });//生存现状
            DataTable dt = BaseDT.WILD_BOTANYPROP.getDT(sw);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WILD_BOTANYPROP_Model m = new WILD_BOTANYPROP_Model();
                m.WILD_BOTANYPROPID = dt.Rows[i]["WILD_BOTANYPROPID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.LIVINGSTATUSCODE = dt.Rows[i]["LIVINGSTATUSCODE"].ToString();
                m.PROTECTIONLEVELCODE = dt.Rows[i]["PROTECTIONLEVELCODE"].ToString();
                m.PROTECTIONLEVELName = BaseDT.T_SYS_DICT.getName(dt127, m.PROTECTIONLEVELCODE);
                m.LIVINGSTATUSName = BaseDT.T_SYS_DICT.getName(dt129, m.LIVINGSTATUSCODE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
