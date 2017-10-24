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
    /// 有害生物_属性表
    /// </summary>
    public class PEST_PESTPROP
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_PESTPROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            if (isExists(new PEST_PESTPROP_SW { BIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE }))
            {
                sb.AppendFormat("Update PEST_PESTPROP Set ");
                sb.AppendFormat(" QUARANTINE='{0}'", ClsSql.EncodeSql(m.QUARANTINE));
                sb.AppendFormat(",RISK='{0}'", ClsSql.EncodeSql(m.RISK));
                sb.AppendFormat(" WHERE BIOLOGICALTYPECODE= '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
            }
            else
            {
                sb.AppendFormat("Insert Into PEST_PESTPROP(BIOLOGICALTYPECODE, QUARANTINE, RISK)");
                sb.AppendFormat(" Values(");
                sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.QUARANTINE));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RISK));
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
        public static DataTable getDT(PEST_PESTPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_PESTPROP WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE))
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            if (!string.IsNullOrEmpty(sw.QUARANTINE))
                sb.AppendFormat(" AND QUARANTINE = '{0}'", ClsSql.EncodeSql(sw.QUARANTINE));
            if (!string.IsNullOrEmpty(sw.RISK))
                sb.AppendFormat(" AND RISK = '{0}'", ClsSql.EncodeSql(sw.RISK));
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
        public static bool isExists(PEST_PESTPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_PESTPROP where 1=1");
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" and BIOLOGICALTYPECODE='{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
