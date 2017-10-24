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
    /// 用户_通讯录表
    /// </summary>
    public class T_SYS_ADDREDDBOOK
    {
        #region 增加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_ADDREDDBOOK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_ADDREDDBOOK(ATID, ADNAME, USERJOB, PHONE, Tell, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.ATID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ADNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.USERJOB));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.Tell));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败,请检查各输入框是否正确!", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_ADDREDDBOOK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_ADDREDDBOOK");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" ORGNO='{0}'", ClsSql.EncodeSql(m.ORGNO));
            sb.AppendFormat(",ATID='{0}'", ClsSql.EncodeSql(m.ATID));
            sb.AppendFormat(",ADNAME='{0}'", ClsSql.EncodeSql(m.ADNAME));
            sb.AppendFormat(",USERJOB='{0}'", ClsSql.EncodeSql(m.USERJOB));
            sb.AppendFormat(",PHONE='{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",Tell='{0}'", ClsSql.EncodeSql(m.Tell));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" where ADID= '{0}'", ClsSql.EncodeSql(m.ADID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败,请检查各输入框是否正确!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_ADDREDDBOOK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_ADDREDDBOOK");
            sb.AppendFormat(" where ADID= '{0}'", ClsSql.EncodeSql(m.ADID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败,请检查各输入框是否正确!", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_ADDREDDBOOK_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      T_SYS_ADDREDDBOOK a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ADID) == false)
                sb.AppendFormat(" AND ADID = '{0}'", ClsSql.EncodeSql(sw.ADID));
            if (string.IsNullOrEmpty(sw.ATID) == false)
                sb.AppendFormat(" AND ATID = '{0}'", ClsSql.EncodeSql(sw.ATID));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" AND PHONE like '%{0}%'", ClsSql.EncodeSql(sw.PHONE));
            if (string.IsNullOrEmpty(sw.ADNAME) == false)
                sb.AppendFormat(" AND ADNAME like '%{0}%'", ClsSql.EncodeSql(sw.ADNAME));

            string sql = "SELECT ADID, ATID,ORGNO, ADNAME, USERJOB, PHONE, Tell, ORDERBY " + sb.ToString() + " order by ORDERBY";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_ADDREDDBOOK_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      T_SYS_ADDREDDBOOK a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ADID) == false)
                sb.AppendFormat(" AND ADID = '{0}'", ClsSql.EncodeSql(sw.ADID));
            if (string.IsNullOrEmpty(sw.ATID) == false)
                sb.AppendFormat(" AND ATID = '{0}'", ClsSql.EncodeSql(sw.ATID));
            //if (string.IsNullOrEmpty(sw.ORGNO) == false)
            //    sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" AND PHONE like '%{0}%'", ClsSql.EncodeSql(sw.PHONE));
            if (string.IsNullOrEmpty(sw.ADNAME) == false)
                sb.AppendFormat(" AND ADNAME like '%{0}%'", ClsSql.EncodeSql(sw.ADNAME));
            //if (string.IsNullOrEmpty(sw.TopORGNO) == false)
            //{
            //    if (PublicCls.OrgIsShi(sw.TopORGNO))
            //    {
            //        sb.AppendFormat(" and ORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.TopORGNO));
            //    }
            //    else if (PublicCls.OrgIsXian(sw.TopORGNO))
            //    {
            //        sb.AppendFormat(" and ORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.TopORGNO));
            //    }
            //    else
            //    {
            //        sb.AppendFormat(" and ORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.TopORGNO));
            //    }
            //}
            string sql = "SELECT ADID,ATID, ORGNO, ADNAME, USERJOB, PHONE, Tell, ORDERBY " + sb.ToString() + " order by ORDERBY";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
