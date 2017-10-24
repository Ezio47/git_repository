using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 野生动物属性表
    /// </summary>
   public  class WILD_ANIMALPROP
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
       public static Message Save(WILD_ANIMALPROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            if (isExists(new WILD_ANIMALPROP_SW { BIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE }))
            {
                sb.AppendFormat("Update WILD_ANIMALPROP Set ");
                sb.AppendFormat(" PROTECTIONLEVELCODE='{0}'", ClsSql.EncodeSql(m.PROTECTIONLEVELCODE));
                sb.AppendFormat(",LIVINGSTATUSCODE='{0}'", ClsSql.EncodeSql(m.LIVINGSTATUSCODE));
                sb.AppendFormat(" WHERE BIOLOGICALTYPECODE= '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
            }
            else
            {
                sb.AppendFormat("Insert Into WILD_ANIMALPROP(BIOLOGICALTYPECODE, PROTECTIONLEVELCODE, LIVINGSTATUSCODE)");
                sb.AppendFormat(" Values(");
                sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PROTECTIONLEVELCODE));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LIVINGSTATUSCODE));
                sb.AppendFormat(")");
            }
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败!", "");
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
       public static DataTable getDT(WILD_ANIMALPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM WILD_ANIMALPROP WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE))
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            if (!string.IsNullOrEmpty(sw.LIVINGSTATUSCODE))
                sb.AppendFormat(" AND LIVINGSTATUSCODE = '{0}'", ClsSql.EncodeSql(sw.LIVINGSTATUSCODE));
            if (!string.IsNullOrEmpty(sw.PROTECTIONLEVELCODE))
                sb.AppendFormat(" AND PROTECTIONLEVELCODE = '{0}'", ClsSql.EncodeSql(sw.PROTECTIONLEVELCODE));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(WILD_ANIMALPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from WILD_ANIMALPROP where 1=1");
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" and BIOLOGICALTYPECODE='{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
