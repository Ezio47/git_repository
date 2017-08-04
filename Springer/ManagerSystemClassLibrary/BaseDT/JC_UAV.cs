using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 无人机表
    /// </summary>
    public class JC_UAV
    {
        #region 无人机增删改
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_UAV_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_UAV(BYORGNO, UAVNAME,UAVEQUIPNAME, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.UAVNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.UAVEQUIPNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            var bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(JC_UAV_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from  JC_UAV");
            sb.AppendFormat(" where 1=1 ");
            if (string.IsNullOrEmpty(m.UAVID) == false)
                sb.AppendFormat(" AND UAVID = '{0}'", ClsSql.EncodeSql(m.UAVID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败！", "");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Update(JC_UAV_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update  JC_UAV set ");
            if (string.IsNullOrEmpty(m.BYORGNO) == false)
                sb.AppendFormat(" BYORGNO= '{0}',", ClsSql.EncodeSql(m.BYORGNO));
            if (string.IsNullOrEmpty(m.UAVNAME) == false)
                sb.AppendFormat(" UAVNAME= '{0}',", ClsSql.EncodeSql(m.UAVNAME));
            if (string.IsNullOrEmpty(m.UAVEQUIPNAME) == false)
                sb.AppendFormat(" UAVEQUIPNAME= '{0}'", ClsSql.EncodeSql(m.UAVEQUIPNAME));
            sb.AppendFormat(" where UAVID = '{0}'", ClsSql.EncodeSql(m.UAVID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败！", "");
        }
        #endregion

        #region 获取一条信息
        /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_UAV_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM    JC_UAV");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.UAVID) == false)
                sb.AppendFormat(" AND UAVID = '{0}'", ClsSql.EncodeSql(sw.UAVID));
            if (string.IsNullOrEmpty(sw.UAVNAME) == false)
                sb.AppendFormat(" AND UAVNAME like '%{0}%'", ClsSql.EncodeSql(sw.UAVNAME));
            if (string.IsNullOrEmpty(sw.UAVEQUIPNAME) == false)
                sb.AppendFormat(" AND UAVEQUIPNAME like '%{0}%'", ClsSql.EncodeSql(sw.UAVEQUIPNAME));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND(SUBSTRING(BYORGNO,1,9) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(12, 3) == "000")//获取所有村的
                    sb.AppendFormat(" AND(SUBSTRING(BYORGNO,1,12) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT UAVID,BYORGNO,UAVNAME,UAVEQUIPNAME,ORDERBY"
                + sb.ToString()
                + " order by ORDERBY desc";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_UAV_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      JC_UAV");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.UAVID) == false)
                sb.AppendFormat(" AND UAVID = '{0}'", ClsSql.EncodeSql(sw.UAVID));
            if (string.IsNullOrEmpty(sw.UAVNAME) == false)
                sb.AppendFormat(" AND UAVNAME like '%{0}%'", ClsSql.EncodeSql(sw.UAVNAME));
            if (string.IsNullOrEmpty(sw.UAVEQUIPNAME) == false)
                sb.AppendFormat(" AND UAVEQUIPNAME like '%{0}%'", ClsSql.EncodeSql(sw.UAVEQUIPNAME));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
               if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND BYORGNO = '{0}'or BYORGNO is null or BYORGNO=''", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(12, 3) == "000")//获取所有村的
                    sb.AppendFormat(" AND BYORGNO = '{0}'or BYORGNO is null or BYORGNO=''", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT UAVID,BYORGNO, UAVNAME, UAVEQUIPNAME,ORDERBY"
                + sb.ToString()
                + " order by ORDERBY desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
