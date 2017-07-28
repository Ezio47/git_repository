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
    /// 数据中心_瞭望塔
    /// </summary>
    public class DC_WATCHTOWER
    {
        #region  获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_WATCHTOWER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_WATCHTOWER a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DC_WATCHTOWERID) == false)
                sb.AppendFormat("and DC_WATCHTOWERID = '{0}'", ClsSql.EncodeSql(sw.DC_WATCHTOWERID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat("and BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.WATCHNAME) == false)
                sb.AppendFormat("and WATCHNAME = '{0}'", ClsSql.EncodeSql(sw.WATCHNAME));
            if (string.IsNullOrEmpty(sw.BASICS) == false)
                sb.AppendFormat("and BASICS = '{0}'", ClsSql.EncodeSql(sw.BASICS));
            if (string.IsNullOrEmpty(sw.PHOTO) == false)
                sb.AppendFormat("and PHOTO = '{0}'", ClsSql.EncodeSql(sw.PHOTO));
            if (string.IsNullOrEmpty(sw.LINKWAY) == false)
                sb.AppendFormat("and LINKWAY = '{0}'", ClsSql.EncodeSql(sw.LINKWAY));
            if (string.IsNullOrEmpty(sw.BUILDTIME) == false)
                sb.AppendFormat("and BUILDTIME= '{0}'", ClsSql.EncodeSql(sw.BUILDTIME));
            if (string.IsNullOrEmpty(sw.WD) == false)
                sb.AppendFormat("and WD = '{0}'", ClsSql.EncodeSql(sw.WD));
            if (string.IsNullOrEmpty(sw.JD) == false)
                sb.AppendFormat("and JD = '{0}'", ClsSql.EncodeSql(sw.JD));
            if (string.IsNullOrEmpty(sw.ORDERBY) == false)
                sb.AppendFormat("and ORDERBY = '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            if (string.IsNullOrEmpty(sw.BULIDAREA) == false)
                sb.AppendFormat("and BULIDAREA = '{0}'", ClsSql.EncodeSql(sw.BULIDAREA));
            string sql = ("select DC_WATCHTOWERID,BYORGNO,WATCHNAME,BASICS,LINKWAY,PHOTO,BUILDTIME,BULIDAREA,WD,JD,USAGE,ORDERBY") + sb.ToString() + ("order by DC_WATCHTOWERID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
