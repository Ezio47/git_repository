using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataBaseClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 获取T_SYS_ORG_EXTENDID
    /// </summary>
    public class T_SYS_ORG_EXTENDID
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        public static DataTable getDT(T_SYS_ORG_EXTENDID_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      T_SYS_ORG_EXTEND  WHERE 1=1");
            sb.AppendFormat(" AND ORGNO='{0}'", ClsSql.EncodeSql(sw.ORGNO));
            string sql = " SELECT GYLLAYERNAME, DGXLAYERNAME" + sb.ToString();
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
    }
}
