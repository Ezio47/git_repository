using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 文档类别
    /// </summary>
   public class ART_TYPE
   {
       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static DataTable getDT(ART_TYPE_SW sw)
       {

           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("SELECT   ARTTYPEID, ARTTYPENAME, ARTTYPERID, ISCHECKED, ORDERBY");
           sb.AppendFormat(" FROM      ART_TYPE");
           sb.AppendFormat(" WHERE   1=1");
           if (string.IsNullOrEmpty(sw.ARTTYPEID) == false)
               sb.AppendFormat(" AND ARTTYPEID = '{0}'", ClsSql.EncodeSql(sw.ARTTYPEID));
           if (string.IsNullOrEmpty(sw.ARTTYPERID) == false)
               sb.AppendFormat(" AND ARTTYPERID = '{0}'", ClsSql.EncodeSql(sw.ARTTYPERID));
           //if (string.IsNullOrEmpty(sw.USERID) == false)
           //    sb.AppendFormat(" AND USERID = '{0}'", ClsSql.EncodeSql(sw.USERID));

           sb.AppendFormat(" ORDER BY ORDERBY ");

           DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
           return ds.Tables[0];
       }

       /// <summary>
       /// 根据编号获取用户登录名
       /// </summary>
       /// <param name="dt">用户DataTable</param>
       /// <param name="value">编号</param>
       /// <returns>用户登录名</returns>
       public static string getName(DataTable dt, string value)
       {
           if (dt == null)
               return "";
           if (string.IsNullOrEmpty(value))
               return "";
           string str = "";
           DataRow[] dr = dt.Select("ARTTYPEID='" + value + "'");
           if (dr.Length > 0)
               str = dr[0]["ARTTYPENAME"].ToString();
           return str;
       }
    }
}
