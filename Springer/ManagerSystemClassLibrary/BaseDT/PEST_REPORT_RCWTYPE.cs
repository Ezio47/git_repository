using DataBaseClassLibrary;
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
    /// 有害生物_报表_人财物类别表
    /// </summary>
    public class PEST_REPORT_RCWTYPE
    {
        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_REPORT_RCWTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_RCWTYPE WHERE 1=1");
            if (sw.ISGetOnlyTypeTitle)
                sb.AppendFormat(" AND len(RCWCODE) = '2'");
            if (!string.IsNullOrEmpty(sw.RCWCODE))
            {
                sb.AppendFormat(" AND  len(RCWCODE) > '{0}'", ClsSql.EncodeSql(sw.RCWCODE).Length);
                sb.AppendFormat(" AND  substring(RCWCODE,1,{0}) = '{1}'", ClsSql.EncodeSql(sw.RCWCODE).Length, ClsSql.EncodeSql(sw.RCWCODE));
            }
            sb.AppendFormat(" ORDER BY RCWCODE, ORDERBY");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
