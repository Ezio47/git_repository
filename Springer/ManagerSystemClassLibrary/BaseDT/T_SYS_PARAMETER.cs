using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;


namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 系统参数管理
    /// </summary>
    public class T_SYS_PARAMETER
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_PARAMETER_SW SW)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT   PARAMID,PARAMFLAG,PARAMNAME,PARAMVALUE,PARAMMARK,SYSFLAG");
            sb.AppendFormat(" FROM T_SYS_PARAMETER");
            sb.AppendFormat(" WHERE 1=1");
            if (string.IsNullOrEmpty(SW.PARAMID) == false)
                sb.AppendFormat(" AND PARAMID = '{0}'", ClsSql.EncodeSql(SW.PARAMID));
            if (string.IsNullOrEmpty(SW.PARAMFLAG) == false)
                sb.AppendFormat(" AND PARAMFLAG = '{0}'", ClsSql.EncodeSql(SW.PARAMFLAG));
            if (string.IsNullOrEmpty(SW.PARAMNAME) == false)
                sb.AppendFormat(" AND PARAMNAME = '{0}'", ClsSql.EncodeSql(SW.PARAMNAME));
            if (string.IsNullOrEmpty(SW.PARAMVALUE) == false)
                sb.AppendFormat(" AND PARAMVALUE = '{0}'", ClsSql.EncodeSql(SW.PARAMVALUE));
            if (string.IsNullOrEmpty(SW.PARAMMARK) == false)
                sb.AppendFormat(" AND PARAMMARK = '{0}'", ClsSql.EncodeSql(SW.PARAMMARK));
            if (string.IsNullOrEmpty(SW.SYSFLAG) == false)
                sb.AppendFormat(" AND SYSFLAG = '{0}'", ClsSql.EncodeSql(SW.SYSFLAG));
            sb.AppendFormat(" ORDER BY ORDERBY ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_PARAMETER_Model  m) 
        {
           StringBuilder sb=new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_PARAMETER");
            sb.AppendFormat(" SET ");
            if (string.IsNullOrEmpty(ClsSql.EncodeSql(m.PARAMVALUE)) == false)
            sb.AppendFormat(" PARAMVALUE='{0}'",ClsSql.EncodeSql(m.PARAMVALUE));
            sb.AppendFormat(" WHERE PARAMID='{0}'", ClsSql.EncodeSql(m.PARAMID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
             if (bln == true)
                 return new Message(true, "修改成功！", m.returnUrl);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", m.returnUrl);
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="SW">参见模型</param>
        /// <returns>参见模型</returns>
        public static bool isExists(T_SYS_PARAMETER_SW SW)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_PARAMETER where 1=1");
            if (string.IsNullOrEmpty(SW.PARAMID) == false)
                sb.AppendFormat(" and PARAMID='{0}'", ClsSql.EncodeSql(SW.PARAMID));
            if (string.IsNullOrEmpty(SW.PARAMVALUE) == false)
                sb.AppendFormat(" and PARAMVALUE='{0}'", ClsSql.EncodeSql(SW.PARAMVALUE));
            if (string.IsNullOrEmpty(SW.PARAMNAME) == false)
                sb.AppendFormat(" and PARAMNAME='{0}'", ClsSql.EncodeSql(SW.PARAMNAME));
            if (string.IsNullOrEmpty(SW.PARAMFLAG) == false)
                sb.AppendFormat(" and PARAMFLAG='{0}'", ClsSql.EncodeSql(SW.PARAMFLAG));
            if (string.IsNullOrEmpty(SW.PARAMMARK) == false)
                sb.AppendFormat(" and PARAMMARK='{0}'", ClsSql.EncodeSql(SW.PARAMMARK));
            if (string.IsNullOrEmpty(SW.SYSFLAG) == false)
                sb.AppendFormat(" and SYSFLAG='{0}'", ClsSql.EncodeSql(SW.SYSFLAG));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion
    }
}
