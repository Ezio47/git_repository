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
    /// 数据中心_出入库明细表
    /// </summary>
    public class DC_DETAILS
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(DC_DETAILS_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO DC_DETAILS ( SUPID, REPID, DCREPTIME, DCREPFLAG, DCREPSUPCOUNT,DCENTYMANID,DCUSERID,DCCUSTODIANID,DCUSERORG,PRICE,MARK,REPERTORYCOUNT,DCZHIBIAOREN,DCFAFANGREN,NUMBER)");
            sb.AppendFormat("VALUES(");
           // sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCDETAILSID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.SUPID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.REPID));
            //sb.AppendFormat("'{0}',", PublicClassLibrary.ClsSwitch.SwitDate(m.DCREPTIME));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCREPTIME));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCREPFLAG));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCREPSUPCOUNT));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCENTYMANID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCUSERID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCCUSTODIANID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCUSERORG));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.PRICE));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.MARK));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.REPERTORYCOUNT));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCZHIBIAOREN));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCFAFANGREN));
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.NUMBER));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 更新物资剩余数量
        /// <summary>
        /// 更新物资剩余数量
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Mdy(DC_DETAILS_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE DC_DETAILS");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" REPERTORYCOUNT={0}", ClsSql.saveNullField(m.REPERTORYCOUNT));
            sb.AppendFormat(" where DCDETAILSID= '{0}'", ClsSql.EncodeSql(m.DCDETAILSID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_DETAILS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_DETAILS a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DCDETAILSID) == false)
                sb.AppendFormat("and DCDETAILSID = '{0}'", ClsSql.EncodeSql(sw.DCDETAILSID));
            //if (string.IsNullOrEmpty(sw.SUPID) == false)
            //    sb.AppendFormat("and SUPID in (select SUPID from DC_SUPPLIESPROP where DCSUPPROPNAME like '%{0}%')", ClsSql.EncodeSql(sw.SUPNAME));
            //if (string.IsNullOrEmpty(sw.REPID) == false)
            //    sb.AppendFormat("and REPID = (select DCREPOSITORYID from DC_REPOSITORY where NAME ='{0}')", ClsSql.EncodeSql(sw.DPNAME));
            if (string.IsNullOrEmpty(sw.SUPID) == false)
                sb.AppendFormat("and SUPID = '{0}'", ClsSql.EncodeSql(sw.SUPID));
            if (string.IsNullOrEmpty(sw.REPID) == false)
                sb.AppendFormat("and REPID = '{0}'", ClsSql.EncodeSql(sw.REPID));
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" and DCREPTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" and DCREPTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.DCREPFLAG) == false)
                sb.AppendFormat("and DCREPFLAG = '{0}'", ClsSql.EncodeSql(sw.DCREPFLAG));
            //if (string.IsNullOrEmpty(sw.DCREPTIME) == false)
            //    sb.AppendFormat("and DCREPTIME = '{0}'", ClsSql.EncodeSql(sw.DCREPTIME));
            if (string.IsNullOrEmpty(sw.DCENTYMANID) == false)
                sb.AppendFormat("and DCENTYMANID = '{0}'", ClsSql.EncodeSql(sw.DCENTYMANID));
            if (string.IsNullOrEmpty(sw.DCUSERID) == false)
                sb.AppendFormat("and DCUSERID = '{0}'", ClsSql.EncodeSql(sw.DCUSERID));
            if (string.IsNullOrEmpty(sw.DCCUSTODIANID) == false)
                sb.AppendFormat("and DCCUSTODIANID = '{0}'", ClsSql.EncodeSql(sw.DCCUSTODIANID));
            if (string.IsNullOrEmpty(sw.REPERTORYCOUNT) == false)
                sb.AppendFormat("and REPERTORYCOUNT = '{0}'", ClsSql.EncodeSql(sw.REPERTORYCOUNT));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat("and NUMBER = '{0}'", ClsSql.EncodeSql(sw.NUMBER));
            string sql = ("select DCDETAILSID,SUPID,REPID,DCREPTIME,DCREPFLAG,DCREPSUPCOUNT,DCENTYMANID,DCUSERID,DCCUSTODIANID,DCUSERORG,PRICE,MARK,REPERTORYCOUNT,DCZHIBIAOREN,DCFAFANGREN,NUMBER") + sb.ToString() + (" order by DCREPTIME desc ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        /// <summary>
        ///combox根据日期和物资名称排列
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getcombox(DC_DETAILS_SW sw) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" from DC_DETAILS b left join DC_SUPPLIESPROP a on b.SUPID =a.DC_SUPPLIESPROP_ID ");
            sb.AppendFormat(" WHERE   1=1 ");
            if (string.IsNullOrEmpty(sw.DCDETAILSID) == false)
                sb.AppendFormat("and b.DCDETAILSID = '{0}'", ClsSql.EncodeSql(sw.DCDETAILSID));
            if (string.IsNullOrEmpty(sw.SUPID) == false)
                sb.AppendFormat("and b.SUPID = '{0}'", ClsSql.EncodeSql(sw.SUPID));
            if (string.IsNullOrEmpty(sw.REPID) == false)
                sb.AppendFormat("and b.REPID = '{0}'", ClsSql.EncodeSql(sw.REPID));
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" and b.DCREPTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" and b.DCREPTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.DCREPFLAG) == false)
                sb.AppendFormat("and b.DCREPFLAG = '{0}'", ClsSql.EncodeSql(sw.DCREPFLAG));
            if (string.IsNullOrEmpty(sw.DCENTYMANID) == false)
                sb.AppendFormat("and b.DCENTYMANID = '{0}'", ClsSql.EncodeSql(sw.DCENTYMANID));
            if (string.IsNullOrEmpty(sw.DCUSERID) == false)
                sb.AppendFormat("and b.DCUSERID = '{0}'", ClsSql.EncodeSql(sw.DCUSERID));
            if (string.IsNullOrEmpty(sw.DCCUSTODIANID) == false)
                sb.AppendFormat("and b.DCCUSTODIANID = '{0}'", ClsSql.EncodeSql(sw.DCCUSTODIANID));
            if (string.IsNullOrEmpty(sw.REPERTORYCOUNT) == false)
                sb.AppendFormat("and b.REPERTORYCOUNT = '{0}'", ClsSql.EncodeSql(sw.REPERTORYCOUNT));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat("and b.NUMBER = '{0}'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.SUPNAME) == false)
                sb.AppendFormat("and a.DCSUPPROPNAME = '{0}'", ClsSql.EncodeSql(sw.SUPNAME));
            string sql = "select a.*,b.*"
               + sb.ToString()
               + " order by b.DCREPTIME,a.DCSUPPROPNAME DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql); 
            return ds.Tables[0];
        }
        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_DETAILS_SW sw, out int total) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_DETAILS a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DCDETAILSID) == false)
                sb.AppendFormat("and DCDETAILSID = '{0}'", ClsSql.EncodeSql(sw.DCDETAILSID));
            if (string.IsNullOrEmpty(sw.SUPID) == false)
                sb.AppendFormat("and SUPID = '{0}'", ClsSql.EncodeSql(sw.SUPID));
            if (string.IsNullOrEmpty(sw.REPID) == false)
                sb.AppendFormat("and REPID = '{0}'", ClsSql.EncodeSql(sw.REPID));
            //if (string.IsNullOrEmpty(sw.SUPNAME) == false)
            //    sb.AppendFormat("and SUPID in (select SUPID from DC_SUPPLIESPROP where DCSUPPROPNAME ='{0}')", ClsSql.EncodeSql(sw.SUPNAME));
            //if (string.IsNullOrEmpty(sw.REPID) == false)
            //    sb.AppendFormat("and REPID = (select DCREPOSITORYID from DC_REPOSITORY where NAME like '%{0}%)", ClsSql.EncodeSql(sw.DPNAME));
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" and DCREPTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" and DCREPTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.DCREPFLAG) == false)
                sb.AppendFormat("and DCREPFLAG = '{0}'", ClsSql.EncodeSql(sw.DCREPFLAG));
            if (string.IsNullOrEmpty(sw.DCREPSUPCOUNT) == false)
                sb.AppendFormat("and DCREPSUPCOUNT = '{0}'", ClsSql.EncodeSql(sw.DCREPSUPCOUNT));
            if (string.IsNullOrEmpty(sw.DCENTYMANID) == false)
                sb.AppendFormat("and DCENTYMANID = '{0}'", ClsSql.EncodeSql(sw.DCENTYMANID));
            if (string.IsNullOrEmpty(sw.DCUSERID) == false)
                sb.AppendFormat("and DCUSERID = '{0}'", ClsSql.EncodeSql(sw.DCUSERID));
            if (string.IsNullOrEmpty(sw.DCCUSTODIANID) == false)
                sb.AppendFormat("and DCCUSTODIANID = '{0}'", ClsSql.EncodeSql(sw.DCCUSTODIANID));
            if (string.IsNullOrEmpty(sw.REPERTORYCOUNT) == false)
                sb.AppendFormat("and REPERTORYCOUNT = '{0}'", ClsSql.EncodeSql(sw.REPERTORYCOUNT));
            string sql = ("select DCDETAILSID,SUPID,REPID,DCREPTIME,DCREPFLAG,DCREPSUPCOUNT,DCENTYMANID,DCUSERID,DCCUSTODIANID,DCUSERORG,PRICE,MARK,REPERTORYCOUNT,DCZHIBIAOREN,DCFAFANGREN,NUMBER") + sb.ToString() + (" order by DCREPTIME desc ");
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 获取编号
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNUMBER(DC_DETAILS_SW sw) 
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_DETAILS a ");
            sb.AppendFormat("where ");
            sb.AppendFormat("NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            string sqlC = "select count(distinct NUMBER) " + sb.ToString();
            //total = DataBaseClass.ReturnSqlField(sqlC);
            total = (int.Parse(DataBaseClass.ReturnSqlField(sqlC)) + 1).ToString();
            return total;
        }
        #endregion

        #region 通过序号获取物资supid
        /// <summary>
        /// 通过序号获取物资supid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getsupid(string id) 
        {
            string supid = "";
            DataTable dt = getDT(new DC_DETAILS_SW { DCDETAILSID = id });
            if(dt.Rows.Count>0)
            {
                supid = dt.Rows[0]["SUPID"].ToString();
            }
            return supid;
        }
        #endregion

        #region 通过序号获取剩余数量
        /// <summary>
        /// 通过序号获取物资剩余数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getsum(string id)
        {
            string REPERTORYCOUNT = "";
            DataTable dt = getDT(new DC_DETAILS_SW { DCDETAILSID = id });
            if (dt.Rows.Count > 0)
            {
                REPERTORYCOUNT = dt.Rows[0]["REPERTORYCOUNT"].ToString();
            }
            return REPERTORYCOUNT;
        }
        #endregion

        #region 通过序号获取物资单价
        /// <summary>
        /// 通过序号获取物资物资单价
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getprice(string id)
        {
            string price = "";
            DataTable dt = getDT(new DC_DETAILS_SW { DCDETAILSID = id });
            if (dt.Rows.Count > 0)
            {
                price = dt.Rows[0]["PRICE"].ToString();
            }
            return price;
        }
        #endregion
    }
}
