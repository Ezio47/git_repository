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
    /// 有害生物与树种对应表
    /// </summary>
    public class PEST_TREESPECIES_PEST
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_TREESPECIES_PEST_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrPESTBYCODE = m.PESTBYCODE.Split(',');
            StringBuilder sbInsert = new StringBuilder();
            if (arrPESTBYCODE.Length > 0)
            {
                sbInsert.AppendFormat("INSERT INTO PEST_TREESPECIES_PEST(TREESPECIESCODE, PESTBYCODE) ");
                for (int i = 0; i < arrPESTBYCODE.Length; i++)
                {
                    if (!isExists(new PEST_TREESPECIES_PEST_SW { TREESPECIESCODE = m.TREESPECIESCODE, PESTBYCODE = arrPESTBYCODE[i] }))
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.TREESPECIESCODE));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrPESTBYCODE[i]));
                        sbInsert.AppendFormat(" UNION ALL ");
                    }
                }
                string insertStr = sbInsert.ToString();
                if (insertStr.Contains(" UNION ALL "))
                {
                    insertStr = insertStr.Substring(0, insertStr.Length - 10);
                    sqllist.Add(insertStr);
                }
            }
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "添加成功!", "");
            else if (y == 0)
                return new Message(true, "该有害生物已关联!", "");
            else
                return new Message(false, "添加失败,事物回滚!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_TREESPECIES_PEST_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Delete From  PEST_TREESPECIES_PEST  WHERE 1=1 ");
            sb.AppendFormat(" AND TREESPECIESCODE = '{0}'", ClsSql.EncodeSql(m.TREESPECIESCODE));
            sb.AppendFormat(" AND PESTBYCODE  IN ({0})", ClsSql.SwitchStrToSqlIn(m.PESTBYCODE));
            sqllist.Add(sb.ToString());
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败,事物回滚!", "");
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_TREESPECIES_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_TREESPECIES_PEST WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.TREESPECIESCODE))
                sb.AppendFormat(" AND TREESPECIESCODE = '{0}'", ClsSql.EncodeSql(sw.TREESPECIESCODE));
            if (!string.IsNullOrEmpty(sw.PESTBYCODE))
                sb.AppendFormat(" AND PESTBYCODE = '{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            string sql = " SELECT PEST_TREESPECIES_PESTID, TREESPECIESCODE, PESTBYCODE " + sb.ToString() + " ORDER BY TREESPECIESCODE,PESTBYCODE ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_TREESPECIES_PEST_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_TREESPECIES_PEST WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.TREESPECIESCODE))
                sb.AppendFormat(" AND TREESPECIESCODE = '{0}'", ClsSql.EncodeSql(sw.TREESPECIESCODE));
            if (!string.IsNullOrEmpty(sw.PESTBYCODE))
                sb.AppendFormat(" AND PESTBYCODE = '{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            string sql = " SELECT PEST_TREESPECIES_PESTID, TREESPECIESCODE, PESTBYCODE " + sb.ToString() + " ORDER BY  TREESPECIESCODE,PESTBYCODE ";
            string sqlC = "SELECT Count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_TREESPECIES_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_TREESPECIES_PEST where 1=1");
            if (string.IsNullOrEmpty(sw.TREESPECIESCODE) == false)
                sb.AppendFormat(" and TREESPECIESCODE='{0}'", ClsSql.EncodeSql(sw.TREESPECIESCODE));
            if (string.IsNullOrEmpty(sw.PESTBYCODE) == false)
                sb.AppendFormat(" and PESTBYCODE='{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
