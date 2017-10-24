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
    /// 群组
    /// </summary>
    public class E_GROUP
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(E_GROUP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  E_GROUP(EGROUPUSERID, EGROUPNAME, EGROUPMEMBERLIST, EGROUPTYPE,EGROUPPHONELIST)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.EGROUPUSERID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EGROUPNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EGROUPMEMBERLIST));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EGROUPTYPE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EGROUPPHONELIST));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");

            
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(E_GROUP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE E_GROUP");
            sb.AppendFormat(" set ");
            sb.AppendFormat("EGROUPUSERID={0}", ClsSql.saveNullField(m.EGROUPUSERID));
            sb.AppendFormat(",EGROUPNAME={0}", ClsSql.saveNullField(m.EGROUPNAME));
            sb.AppendFormat(",EGROUPMEMBERLIST={0}", ClsSql.saveNullField(m.EGROUPMEMBERLIST));
            sb.AppendFormat(",EGROUPTYPE={0}", ClsSql.saveNullField(m.EGROUPTYPE));
            sb.AppendFormat(",EGROUPPHONELIST={0}", ClsSql.saveNullField(m.EGROUPPHONELIST));
            sb.AppendFormat(" where EGROUPID= '{0}'", ClsSql.EncodeSql(m.EGROUPID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.EGROUPID);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(E_GROUP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete E_GROUP");
            sb.AppendFormat(" where EGROUPID= '{0}'", ClsSql.EncodeSql(m.EGROUPID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(E_GROUP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      E_GROUP");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.EGROUPID) == false)
                sb.AppendFormat(" AND EGROUPID = '{0}'", ClsSql.EncodeSql(sw.EGROUPID));
            if (string.IsNullOrEmpty(sw.EGROUPUSERID) == false)
                sb.AppendFormat(" AND EGROUPUSERID = '{0}'", ClsSql.EncodeSql(sw.EGROUPUSERID));
            if (string.IsNullOrEmpty(sw.EGROUPNAME) == false)
                sb.AppendFormat(" AND EGROUPNAME = '{0}'", ClsSql.EncodeSql(sw.EGROUPNAME));
            if (string.IsNullOrEmpty(sw.EGROUPTYPE) == false)
                sb.AppendFormat(" AND EGROUPTYPE = '{0}'", ClsSql.EncodeSql(sw.EGROUPTYPE));
            string sql = "SELECT EGROUPID, EGROUPUSERID, EGROUPNAME, EGROUPMEMBERLIST, EGROUPTYPE,EGROUPPHONELIST"
                + sb.ToString()
                + " order by EGROUPID";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取记录涉及分页
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        ///  <param name="total">总共</param>
        /// <returns></returns>
        public static DataTable getDT(E_GROUP_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      E_GROUP");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.EGROUPID) == false)
                sb.AppendFormat(" AND EGROUPID = '{0}'", ClsSql.EncodeSql(sw.EGROUPID));
            if (string.IsNullOrEmpty(sw.EGROUPUSERID) == false)
                sb.AppendFormat(" AND EGROUPUSERID = '{0}'", ClsSql.EncodeSql(sw.EGROUPUSERID));
            if (string.IsNullOrEmpty(sw.EGROUPNAME) == false)
                sb.AppendFormat(" AND EGROUPNAME = '{0}'", ClsSql.EncodeSql(sw.EGROUPNAME));
            if (string.IsNullOrEmpty(sw.EGROUPTYPE) == false)
                sb.AppendFormat(" AND EGROUPTYPE = '{0}'", ClsSql.EncodeSql(sw.EGROUPTYPE));
            string sql = "SELECT EGROUPID, EGROUPUSERID, EGROUPNAME, EGROUPMEMBERLIST, EGROUPTYPE,EGROUPPHONELIST"
                + sb.ToString() + " order by EGROUPID";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            //DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
