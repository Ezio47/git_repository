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
    /// 远程诊断表
    /// </summary>
    public class PEST_REMOTEDIAGN
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_REMOTEDIAGN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  PEST_REMOTEDIAGN( DIAGNTITLE, DIAGNCONTENT, DIAGNTIME, DIAGNEXPERTS, DIAGNSTATUS, DIAGNRESULT, DIAGNSPONSERUID, DIAGNSPONSERTIME)");
            sb.AppendFormat(" OUTPUT INSERTED.PEST_REMOTEDIAGNID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.DIAGNTITLE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNCONTENT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNTIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNEXPERTS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNSTATUS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNRESULT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNSPONSERUID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIAGNSPONSERTIME));
            sb.AppendFormat(")");
            try
            {
                string sId = DataBaseClass.ReturnSqlField(sb.ToString());
                if (sId != "")
                    return new Message(true, "添加成功!", sId);
                else
                    return new Message(false, "添加失败!", "");
            }
            catch
            {
                return new Message(false, "添加失败!", "");
            }
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_REMOTEDIAGN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_REMOTEDIAGN");
            sb.AppendFormat(" SET ");
            sb.AppendFormat(" DIAGNTITLE={0}", ClsSql.saveNullField(m.DIAGNTITLE));
            sb.AppendFormat(",DIAGNCONTENT={0}", ClsSql.saveNullField(m.DIAGNCONTENT));
            sb.AppendFormat(",DIAGNSTATUS={0}", ClsSql.saveNullField(m.DIAGNSTATUS));
            sb.AppendFormat(",DIAGNSPONSERTIME={0}", ClsSql.saveNullField(m.DIAGNSPONSERTIME));
            sb.AppendFormat(" WHERE PEST_REMOTEDIAGNID= '{0}'", ClsSql.EncodeSql(m.PEST_REMOTEDIAGNID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.PEST_REMOTEDIAGNID);
            else
                return new Message(false, "修改失败!", "");
        }

        /// <summary>
        /// 修改状态、发起人、发起时间
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyZT(PEST_REMOTEDIAGN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_REMOTEDIAGN");
            sb.AppendFormat(" SET ");
            sb.AppendFormat(" DIAGNRESULT={0}", ClsSql.saveNullField(m.DIAGNRESULT));
            sb.AppendFormat(",DIAGNSPONSERUID={0}", ClsSql.saveNullField(m.DIAGNSPONSERUID));
            sb.AppendFormat(",DIAGNEXPERTS={0}", ClsSql.saveNullField(m.DIAGNEXPERTS));
            sb.AppendFormat(" WHERE PEST_REMOTEDIAGNID= '{0}'", ClsSql.EncodeSql(m.PEST_REMOTEDIAGNID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.PEST_REMOTEDIAGNID);
            else
                return new Message(false, "修改失败!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_REMOTEDIAGN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete PEST_REMOTEDIAGN");
            sb.AppendFormat(" where PEST_REMOTEDIAGNID= '{0}'", ClsSql.EncodeSql(m.PEST_REMOTEDIAGNID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(PEST_REMOTEDIAGN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REMOTEDIAGN where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REMOTEDIAGNID) == false)
                sb.AppendFormat(" and PEST_REMOTEDIAGNID= '{0}'", ClsSql.EncodeSql(sw.PEST_REMOTEDIAGNID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(PEST_REMOTEDIAGN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  PEST_REMOTEDIAGN WHERE  1=1");
            if (string.IsNullOrEmpty(sw.PEST_REMOTEDIAGNID) == false)
                sb.AppendFormat(" AND PEST_REMOTEDIAGNID = '{0}'", ClsSql.EncodeSql(sw.PEST_REMOTEDIAGNID));
            if (string.IsNullOrEmpty(sw.DIAGNTITLE) == false)
                sb.AppendFormat(" AND DIAGNTITLE like '%{0}%'", ClsSql.EncodeSql(sw.DIAGNTITLE));
            if (string.IsNullOrEmpty(sw.DIAGNSTARTTIME) == false)
                sb.AppendFormat(" AND DIAGNTIME >= '{0}'", ClsSql.EncodeSql(DateTime.Parse(sw.DIAGNSTARTTIME).ToString()));
            if (string.IsNullOrEmpty(sw.DIAGNENDTIME) == false)
                sb.AppendFormat(" AND DIAGNTIME <= '{0}'", ClsSql.EncodeSql(DateTime.Parse(sw.DIAGNENDTIME).AddDays(1).AddSeconds(-1).ToString()));
            if (string.IsNullOrEmpty(sw.DIAGNSTATUS) == false)
                sb.AppendFormat(" AND DIAGNSTATUS = '{0}'", ClsSql.EncodeSql(sw.DIAGNSTATUS));
            if (string.IsNullOrEmpty(sw.DIAGNSPONSERUID) == false)
                sb.AppendFormat(" AND DIAGNSPONSERUID = '{0}'", ClsSql.EncodeSql(sw.DIAGNSPONSERUID));
            string sql = "SELECT PEST_REMOTEDIAGNID, DIAGNTITLE, DIAGNCONTENT, DIAGNTIME, DIAGNEXPERTS, DIAGNSTATUS, DIAGNRESULT, DIAGNSPONSERUID, DIAGNSPONSERTIME"
                + sb.ToString() + " ORDER BY DIAGNTIME DESC ";
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
        public static DataTable getDT(PEST_REMOTEDIAGN_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  PEST_REMOTEDIAGN WHERE  1=1");
            if (string.IsNullOrEmpty(sw.PEST_REMOTEDIAGNID) == false)
                sb.AppendFormat(" AND PEST_REMOTEDIAGNID = '{0}'", ClsSql.EncodeSql(sw.PEST_REMOTEDIAGNID));
            if (string.IsNullOrEmpty(sw.DIAGNTITLE) == false)
                sb.AppendFormat(" AND DIAGNTITLE like '%{0}%'", ClsSql.EncodeSql(sw.DIAGNTITLE));
            if (string.IsNullOrEmpty(sw.DIAGNSTARTTIME) == false)
                sb.AppendFormat(" AND DIAGNTIME >= '{0}'", ClsSql.EncodeSql(DateTime.Parse(sw.DIAGNSTARTTIME).ToString()));
            if (string.IsNullOrEmpty(sw.DIAGNENDTIME) == false)
                sb.AppendFormat(" AND DIAGNTIME <= '{0}'", ClsSql.EncodeSql(DateTime.Parse(sw.DIAGNENDTIME).AddDays(1).AddSeconds(-1).ToString()));
            if (string.IsNullOrEmpty(sw.DIAGNSTATUS) == false)
                sb.AppendFormat(" AND DIAGNSTATUS = '{0}'", ClsSql.EncodeSql(sw.DIAGNSTATUS));
            if (string.IsNullOrEmpty(sw.DIAGNSPONSERUID) == false)
                sb.AppendFormat(" AND DIAGNSPONSERUID = '{0}'", ClsSql.EncodeSql(sw.DIAGNSPONSERUID));
            string sql = "SELECT PEST_REMOTEDIAGNID, DIAGNTITLE, DIAGNCONTENT, DIAGNTIME, DIAGNEXPERTS, DIAGNSTATUS, DIAGNRESULT, DIAGNSPONSERUID, DIAGNSPONSERTIME"
                + sb.ToString() + " ORDER BY DIAGNTIME DESC ";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
            return ds.Tables[0];
        }
        #endregion
    }
}
