using DataBaseClassLibrary;
using ManagerSystemModel.SDEModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT.SDE
{
    /// <summary>
    /// 公益林
    /// </summary>
    public class GONGYILIN
    {
        /// <summary>
        /// 获取公益林数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable getDT(SDE_GONGYILIN_Model model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *,shape.STCentroid().STX AS STX ,shape.STCentroid().STY AS STY FROM GONGYILIN Where 1=1 ");
            if (!string.IsNullOrEmpty(model.OBJECTID))
            {
                sb.AppendFormat(" AND OBJECTID = {0}", ClsSql.EncodeSql(model.OBJECTID));
            }
            if (!string.IsNullOrEmpty(model.SqlStr))
            {
                sb.AppendFormat(" AND {0} ", model.SqlStr);
            }

            DataSet ds = SDEDataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }


        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(SDE_GONGYILIN_Model sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  from   GONGYILIN  ");
            sb.AppendFormat("where 1=1");
            if (!string.IsNullOrEmpty(sw.COUNTY))
            {
                sb.AppendFormat(" AND 县 LIKE '%{0}%'", ClsSql.EncodeSql(sw.COUNTY));
            }
            if (!string.IsNullOrEmpty(sw.COUNTRY))
            {
                sb.AppendFormat(" AND 乡 LIKE '%{0}%'", ClsSql.EncodeSql(sw.COUNTRY));
            }
            if (!string.IsNullOrEmpty(sw.VILLAGE))
            {
                sb.AppendFormat(" AND 村 LIKE '%{0}%'", ClsSql.EncodeSql(sw.VILLAGE));
            }
            if (!string.IsNullOrEmpty(sw.LINBAN))
            {
                sb.AppendFormat(" AND 林班 = '{0}'", ClsSql.EncodeSql(sw.LINBAN));
            }
            if (!string.IsNullOrEmpty(sw.XIAOBAN))
            {
                sb.AppendFormat(" AND 小班 = '{0}'", ClsSql.EncodeSql(sw.XIAOBAN));
            }
            string sql = ("select *, shape.STCentroid().STX AS STX ,shape.STCentroid().STY AS STY ") + sb.ToString() + (" order by OBJECTID desc ");
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(SDEDataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = SDEDataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion
    }
}
