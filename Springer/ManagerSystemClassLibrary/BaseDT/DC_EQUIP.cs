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
    /// 数据中心_装备
    /// </summary>
     public class DC_EQUIP
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
         public static DataTable getDT(DC_EQUIP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  FROM DC_EQUIP a ");
            sb.AppendFormat("WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.DC_EQUIPID) == false)
                sb.AppendFormat("AND DC_EQUIPID ='{0}'", ClsSql.EncodeSql(sw.DC_EQUIPID));
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
            string sql = "SELECT    DC_EQUIPID,TYPEID,FACINAME,JD,WD,MARK" + sb.ToString() + "order by DC_EQUIPID";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
