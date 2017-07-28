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
    /// 数据中心_瞭望塔人员表
    /// </summary>
     public class DC_WATCHTOWERUSER
    {
        #region  获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
         public static DataTable getDT(DC_WATCHTOWERUSER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_WATCHTOWERUSER a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DC_WATCHTOWERUSERID) == false)
                sb.AppendFormat("and DC_WATCHTOWERUSERID = '{0}'", ClsSql.EncodeSql(sw.DC_WATCHTOWERUSERID));
            if (string.IsNullOrEmpty(sw.WATCHTOWERID) == false)
                sb.AppendFormat("and WATCHTOWERID = '{0}'", ClsSql.EncodeSql(sw.WATCHTOWERID));
            if (string.IsNullOrEmpty(sw.FTNAME) == false)
                sb.AppendFormat("and FTNAME = '{0}'", ClsSql.EncodeSql(sw.FTNAME));
            if (string.IsNullOrEmpty(sw.BIRTH) == false)
                sb.AppendFormat("and BIRTH = '{0}'", ClsSql.EncodeSql(sw.BIRTH));
            if (string.IsNullOrEmpty(sw.PHOTO) == false)
                sb.AppendFormat("and PHOTO = '{0}'", ClsSql.EncodeSql(sw.PHOTO));
            if (string.IsNullOrEmpty(sw.LINKWAY) == false)
                sb.AppendFormat("and LINKWAY = '{0}'", ClsSql.EncodeSql(sw.LINKWAY));
            if (string.IsNullOrEmpty(sw.SEX) == false)
                sb.AppendFormat("and SEX= '{0}'", ClsSql.EncodeSql(sw.SEX));
            if (string.IsNullOrEmpty(sw.ORDERBY) == false)
                sb.AppendFormat("and ORDERBY = '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            if (string.IsNullOrEmpty(sw.NATION) == false)
                sb.AppendFormat("and NATION = '{0}'", ClsSql.EncodeSql(sw.NATION));
            if (string.IsNullOrEmpty(sw.NATION) == false)
                sb.AppendFormat("and USERJOB = '{0}'", ClsSql.EncodeSql(sw.USERJOB));
            string sql = ("select DC_WATCHTOWERUSERID,WATCHTOWERID,FTNAME,BIRTH,SEX,NATION,USERJOB,LINKWAY,PHOTO,ORDERBY") + sb.ToString() + ("order by DC_WATCHTOWERUSERID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
