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
    /// 野生动物属性表
    /// </summary>
   public class WILD_ANIMALPROPCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
       public static Message Manager(WILD_ANIMALPROP_Model m)
        {
            Message msg = BaseDT.WILD_ANIMALPROP.Save(m);
            return new Message(msg.Success, msg.Msg, msg.Url); ;
        }
        #endregion

       #region 获取单条数据
       /// <summary>
       /// 获取单条数据
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static WILD_ANIMALPROP_Model getModel(WILD_ANIMALPROP_SW sw)
       {
           DataTable dt = BaseDT.WILD_ANIMALPROP.getDT(sw);
           DataTable dt127 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "127" });//保护级别
           DataTable dt129 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "129" });//生存现状
           WILD_ANIMALPROP_Model m = new WILD_ANIMALPROP_Model();
           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.WILD_ANIMALPROPID = dt.Rows[i]["WILD_ANIMALPROPID"].ToString();
               m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
               m.LIVINGSTATUSCODE = dt.Rows[i]["LIVINGSTATUSCODE"].ToString();
               m.PROTECTIONLEVELCODE = dt.Rows[i]["PROTECTIONLEVELCODE"].ToString();
               m.BIOLOGICALTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.BIOLOGICALTYPECODE);
               m.PROTECTIONLEVELName = BaseDT.T_SYS_DICT.getName(dt127, m.PROTECTIONLEVELCODE);
               m.LIVINGSTATUSName = BaseDT.T_SYS_DICT.getName(dt129, m.LIVINGSTATUSCODE);
           }
           dt.Clear();
           dt.Dispose();
           dt127.Clear();
           dt127.Dispose();
           dt129.Clear();
           dt129.Dispose();
           return m;
       }
       #endregion

       #region 获取数据列表
       /// <summary>
       /// 获取数据列表
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static IEnumerable<WILD_ANIMALPROP_Model> getListModel(WILD_ANIMALPROP_SW sw)
       {
           var result = new List<WILD_ANIMALPROP_Model>();
           DataTable dt = BaseDT.WILD_ANIMALPROP.getDT(sw);
           DataTable dt127 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "127" });//保护级别
           DataTable dt129 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "129" });//生存现状
           for (int i = 0; i < dt.Rows.Count; i++)
           {
                WILD_ANIMALPROP_Model m = new WILD_ANIMALPROP_Model();
               m.WILD_ANIMALPROPID = dt.Rows[i]["WILD_ANIMALPROPID"].ToString();
               m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
               m.LIVINGSTATUSCODE = dt.Rows[i]["LIVINGSTATUSCODE"].ToString();
               m.PROTECTIONLEVELCODE = dt.Rows[i]["PROTECTIONLEVELCODE"].ToString();
               m.BIOLOGICALTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.BIOLOGICALTYPECODE);
               m.PROTECTIONLEVELName = BaseDT.T_SYS_DICT.getName(dt127, m.PROTECTIONLEVELCODE);
               m.LIVINGSTATUSName = BaseDT.T_SYS_DICT.getName(dt129, m.LIVINGSTATUSCODE);
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           dt127.Clear();
           dt127.Dispose();
           dt129.Clear();
           dt129.Dispose();
           return result;
       }
       #endregion
    }
}
