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
    /// 数据中心-设施
    /// </summary>
     public class DC_FACILITY
    {
        #region 获取数据
         /// <summary>
        /// 获取数据
         /// </summary>
         /// <param name="sw"></param>
         /// <returns></returns>
        public static DataTable getDT(DC_FACILITY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  FROM DC_FACILITY a ");
            sb.AppendFormat("WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.DC_FACILITYID) == false)
                sb.AppendFormat("AND DC_FACILITYID ='{0}'",ClsSql.EncodeSql(sw.DC_FACILITYID));
            if (string.IsNullOrEmpty(sw.TYPEID) == false)
                sb.AppendFormat("AND TYPEID ='{0}'", ClsSql.EncodeSql(sw.TYPEID));
            if (string.IsNullOrEmpty(sw.FACINAME) == false)
                sb.AppendFormat("AND FACINAME='{0}'", ClsSql.EncodeSql(sw.FACINAME));
            if (string.IsNullOrEmpty(sw.JD) == false)
                sb.AppendFormat("AND JD='{0}'", ClsSql.EncodeSql(sw.JD));
            if (string.IsNullOrEmpty(sw.WD) == false)
                sb.AppendFormat("AND WD='{0}'", ClsSql.EncodeSql(sw.WD));
            if (string.IsNullOrEmpty(sw.MARK) == false)
                sb.AppendFormat("AND MARK='{0}'", ClsSql.EncodeSql(sw.MARK));
            string sql = "SELECT    DC_FACILITYID,TYPEID,FACINAME,JD,WD,MARK" + sb.ToString() + "order by DC_FACILITYID";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
