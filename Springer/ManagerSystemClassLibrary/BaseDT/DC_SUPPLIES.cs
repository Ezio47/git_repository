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
    /// 数据中心_物资表
    /// </summary>
    public class DC_SUPPLIES
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(DC_SUPPLIES_Model m)
        {


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO DC_SUPPLIES ( DCSUPPLIESID,SUPID, REPID,DCSUPCOUNT,PRICE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCSUPPLIESID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.SUPID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.REPID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCSUPCOUNT));
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.PRICE));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");

        }
        #endregion

        #region 更新
        /// <summary>
        /// 添加-更新数据库
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Mdy(DC_SUPPLIES_Model m)
        {
            if (DC_SUPPLIES.isExists(new DC_SUPPLIES_Model { DCSUPPLIESID = m.DCSUPPLIESID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" INSERT INTO DC_SUPPLIES ( DCSUPPLIESID,SUPID, REPID,DCSUPCOUNT,PRICE)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCSUPPLIESID));
                sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.SUPID));
                sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.REPID));
                sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.DCSUPCOUNT));
                sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.PRICE));
                sb.AppendFormat(")");
                bool bln = DataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功！", "");
                else
                    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE DC_SUPPLIES ");
                sb.AppendFormat("set ");
                sb.AppendFormat(" DCSUPCOUNT = '{0}'", ClsSql.EncodeSql(m.DCSUPCOUNT));
                sb.AppendFormat(" ,SUPID = '{0}'", ClsSql.EncodeSql(m.SUPID));
                sb.AppendFormat(" ,PRICE = '{0}'", ClsSql.EncodeSql(m.PRICE));
                sb.AppendFormat("where DCSUPPLIESID = '{0}'", ClsSql.EncodeSql(m.DCSUPPLIESID));
                bool bln = DataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功", "");
                else
                    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(DC_SUPPLIES_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_SUPPLIES");
            sb.AppendFormat(" where DCSUPPLIESID= '{0}'", ClsSql.EncodeSql(m.DCSUPPLIESID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", m.DCSUPPLIESID);
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        # region 判断是否存在
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExists(DC_SUPPLIES_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_SUPPLIES where 1=1");
            if (string.IsNullOrEmpty(m.DCSUPPLIESID) == false)
                sb.AppendFormat(" and DCSUPPLIESID='{0}'", ClsSql.EncodeSql(m.DCSUPPLIESID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_SUPPLIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_SUPPLIES a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DCSUPPLIESID) == false)
                sb.AppendFormat("and DCSUPPLIESID = '{0}'", ClsSql.EncodeSql(sw.DCSUPPLIESID));
            if (string.IsNullOrEmpty(sw.SUPID) == false)
                sb.AppendFormat("and SUPID = '{0}'", ClsSql.EncodeSql(sw.SUPID));
            if (string.IsNullOrEmpty(sw.REPID) == false)
                sb.AppendFormat("and REPID = '{0}'", ClsSql.EncodeSql(sw.REPID));
            if (string.IsNullOrEmpty(sw.DCSUPCOUNT) == false)
                sb.AppendFormat("and DCSUPCOUNT = '{0}'", ClsSql.EncodeSql(sw.DCSUPCOUNT));

            string sql = ("select DCSUPPLIESID,SUPID,REPID,DCSUPCOUNT,PRICE") + sb.ToString() + (" order by DCSUPPLIESID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 获取物资的数量
        /// <summary>
        /// 获取物资的数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(DC_SUPPLIES_SW sw)
        {
            DataTable dt = getDT(sw);
            string DCsupcount = "";
            if (dt.Rows.Count > 0)
                DCsupcount = dt.Rows[0]["DCSUPCOUNT"].ToString();
            dt.Clear();
            dt.Dispose();
            return DCsupcount;
        }
        #endregion

        #region 获取物资的单价
        /// <summary>
        /// 获取物资的单价
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getPrice(DC_SUPPLIES_SW sw)
        {
            DataTable dt = getDT(sw);
            string PRICE = "";
            if (dt.Rows.Count > 0)
                PRICE = dt.Rows[0]["PRICE"].ToString();
            dt.Clear();
            dt.Dispose();
            return PRICE;
        }
        #endregion

        #region 获取主键id
        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getid(DC_SUPPLIES_SW sw)
        {
            DataTable dt = getDT(sw);
            string DCSUPPLIESID = "";
            if (dt.Rows.Count > 0)
                DCSUPPLIESID = dt.Rows[0]["DCSUPPLIESID"].ToString();
            dt.Clear();
            dt.Dispose();
            return DCSUPPLIESID;
        }
        #endregion

        #region 出入库物资下拉框
        /// <summary>
        /// 出入库物资下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable getcombox(DC_SUPPLIES_SW sw,string type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" from DC_SUPPLIES b left join DC_SUPPLIESPROP a on b.SUPID =a.DC_SUPPLIESPROP_ID ");
            sb.AppendFormat(" WHERE   1=1 ");
            if (string.IsNullOrEmpty(sw.SUPID) == false)
                sb.AppendFormat("and b.SUPID = '{0}'", ClsSql.EncodeSql(sw.SUPID));
            if (string.IsNullOrEmpty(sw.REPID) == false)
                sb.AppendFormat("and b.REPID = '{0}'", ClsSql.EncodeSql(sw.REPID));
            if (type == "2")//表示出库时
            {
                sb.AppendFormat(" and b.DCSUPCOUNT>0");
            }
            string sql = "select a.*,b.*"
               + sb.ToString()
               + " order by  DC_SUPPLIESPROP_ID DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

    }
}
