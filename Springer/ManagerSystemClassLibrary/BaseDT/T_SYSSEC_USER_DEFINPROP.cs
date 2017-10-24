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
    /// 系统用户_自定义属性表
    /// </summary>
   public class T_SYSSEC_USER_DEFINPROP
   {
       /// <summary>
       /// 判断记录是否存在 
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>true存在 false不存在 </returns>
       public static bool isExists(T_SYSSEC_USER_DEFINPROP_SW sw)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("select 1 from T_SYSSEC_USER_DEFINPROP where 1=1");
           if (string.IsNullOrEmpty(sw.UID) == false)
               sb.AppendFormat(" and UID='{0}'", ClsSql.EncodeSql(sw.UID));
           if (string.IsNullOrEmpty(sw.DICTVALUE) == false)
               sb.AppendFormat(" and DICTVALUE='{0}'", ClsSql.EncodeSql(sw.DICTVALUE));
           return DataBaseClass.JudgeRecordExists(sb.ToString());
       }

       /// <summary>
       /// 获取数据,获取所有
       /// </summary>
       /// <returns>参见模型</returns>
       public static DataTable getDT(T_SYSSEC_USER_DEFINPROP_SW sw)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("SELECT   T_SYSSEC_USER_DEFINPROPID, UID, DICTVALUE, PROPVALUE");
           sb.AppendFormat(" FROM      T_SYSSEC_USER_DEFINPROP");
           sb.AppendFormat(" WHERE   1=1");
           if (string.IsNullOrEmpty(sw.UID) == false)
               sb.AppendFormat(" and UID='{0}'", ClsSql.EncodeSql(sw.UID));
           if (string.IsNullOrEmpty(sw.DICTVALUE) == false)
               sb.AppendFormat(" and DICTVALUE='{0}'", ClsSql.EncodeSql(sw.DICTVALUE));
           sb.AppendFormat(" ORDER BY UID,DICTVALUE ");
           DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
           return ds.Tables[0];
       }

       /// <summary>
       /// 根据编号查询属性值
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static string getPROPVALUEByUIDDICTVALUE(T_SYSSEC_USER_DEFINPROP_SW sw)
       {
           string sql = "SELECT PROPVALUE FROM T_SYSSEC_USER_DEFINPROP WHERE UID='" + sw.UID + "' and DICTVALUE='" + sw.DICTVALUE + "'";
           return DataBaseClass.ReturnSqlField(sql);
       }
    }
}
