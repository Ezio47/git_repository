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
    /// 数据中心_专业队
    /// </summary>
    public class DC_PROTEAM
    {
        #region  获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_PROTEAM_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_PROTEAM a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DC_PROTEAMID) == false)
                sb.AppendFormat("and DC_PROTEAMID = '{0}'", ClsSql.EncodeSql(sw.DC_PROTEAMID));
            if (string.IsNullOrEmpty(sw.TYPEID) == false)
                sb.AppendFormat("and TYPEID = '{0}'", ClsSql.EncodeSql(sw.TYPEID));
            if (string.IsNullOrEmpty(sw.PROTEAMNAME) == false)
                sb.AppendFormat("and PROTEAMNAME = '{0}'", ClsSql.EncodeSql(sw.PROTEAMNAME));
            if (string.IsNullOrEmpty(sw.EQUIP) == false)
                sb.AppendFormat("and EQUIP = '{0}'", ClsSql.EncodeSql(sw.EQUIP));
            if (string.IsNullOrEmpty(sw.CAPACITY) == false)
                sb.AppendFormat("and CAPACITY = '{0}'", ClsSql.EncodeSql(sw.CAPACITY));
            if (string.IsNullOrEmpty(sw.LEADER) == false)
                sb.AppendFormat("and LEADER = '{0}'", ClsSql.EncodeSql(sw.LEADER));
            if (string.IsNullOrEmpty(sw.LINKWAY) == false)
                sb.AppendFormat("and LINKWAY = '{0}'", ClsSql.EncodeSql(sw.LINKWAY));
            if (string.IsNullOrEmpty(sw.JD) == false)
                sb.AppendFormat("and JD= '{0}'", ClsSql.EncodeSql(sw.JD));
            if (string.IsNullOrEmpty(sw.ORDERBY) == false)
                sb.AppendFormat("and ORDERBY = '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            if (string.IsNullOrEmpty(sw.WD) == false)
                sb.AppendFormat("and WD = '{0}'", ClsSql.EncodeSql(sw.WD));
            string sql = ("select DC_PROTEAMID,TYPEID,PROTEAMNAME,EQUIP,CAPACITY,LEADER,LINKWAY,JD,WD,ORDERBY") + sb.ToString() + ("order by DC_PROTEAMID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
