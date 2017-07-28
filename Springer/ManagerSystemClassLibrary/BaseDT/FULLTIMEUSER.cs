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
    /// 数据中心_专职人员
    /// </summary>
    public class FULLTIMEUSER
    {
        #region  获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_FULLTIMEUSER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_FULLTIMEUSER a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DC_FULLTIMEUSERID) == false)
                sb.AppendFormat("and DC_FULLTIMEUSERID = '{0}'", ClsSql.EncodeSql(sw.DC_FULLTIMEUSERID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat("and BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.FTNAME) == false)
                sb.AppendFormat("and FTNAME = '{0}'", ClsSql.EncodeSql(sw.FTNAME));
            if (string.IsNullOrEmpty(sw.BIRTH) == false)
                sb.AppendFormat("and BIRTH = '{0}'", ClsSql.EncodeSql(sw.BIRTH));
            if (string.IsNullOrEmpty(sw.SEX) == false)
                sb.AppendFormat("and SEX = '{0}'", ClsSql.EncodeSql(sw.SEX));
            if (string.IsNullOrEmpty(sw.NATION) == false)
                sb.AppendFormat("and NATION = '{0}'", ClsSql.EncodeSql(sw.NATION));
            if (string.IsNullOrEmpty(sw.USERJOB) == false)
                sb.AppendFormat("and USERJOB = '{0}'", ClsSql.EncodeSql(sw.USERJOB));
            if (string.IsNullOrEmpty(sw.LINKWAY) == false)
                sb.AppendFormat("and LINKWAY = '{0}'", ClsSql.EncodeSql(sw.LINKWAY));
            if (string.IsNullOrEmpty(sw.PHOTO) == false)
                sb.AppendFormat("and PHOTO = '{0}'", ClsSql.EncodeSql(sw.PHOTO));
            if (string.IsNullOrEmpty(sw.ORDERBY) == false)
                sb.AppendFormat("and ORDERBY = '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            string sql = ("select DC_FULLTIMEUSERID,BYORGNO,FTNAME,BIRTH,SEX,NATION,USERJOB,LINKWAY,PHOTO,ORDERBY") + sb.ToString() + ("order by DC_FULLTIMEUSERID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
