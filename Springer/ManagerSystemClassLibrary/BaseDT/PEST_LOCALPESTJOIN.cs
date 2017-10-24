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
    /// 有害生物_本地化生物关联表
    /// </summary>
    public class PEST_LOCALPESTJOIN
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_LOCALPESTJOIN_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrPESTBYCODE = m.PESTBYCODE.Split(',');
            StringBuilder sbInsert = new StringBuilder();
            if (arrPESTBYCODE.Length > 0)
            {
                sbInsert.AppendFormat("INSERT INTO PEST_LOCALPESTJOIN(BYORGNO, PESTBYCODE, SEARCHTYPE) ");
                for (int i = 0; i < arrPESTBYCODE.Length; i++)
                {
                    if (!isExists(new PEST_LOCALPESTJOIN_SW { BYORGNO = m.BYORGNO, SEARCHTYPE = m.SEARCHTYPE, PESTBYCODE = arrPESTBYCODE[i] }))
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrPESTBYCODE[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SEARCHTYPE));
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
                return new Message(true, "该生物已关联!", "");
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
        public static Message Del(PEST_LOCALPESTJOIN_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Delete From  PEST_LOCALPESTJOIN WHERE 1=1 ");
            sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(" AND SEARCHTYPE = '{0}'", ClsSql.EncodeSql(m.SEARCHTYPE));
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
        public static DataTable getDT(PEST_LOCALPESTJOIN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_LOCALPESTJOIN WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.IsOnlyGetORGNO)
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                else
                {
                    if (sw.BYORGNO.Substring(4, 11) == "00000000000")  //获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(6, 9) == "000000000" && sw.BYORGNO.Substring(4, 11) != "00000000000") //获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else if (sw.BYORGNO.Substring(9, 6) == "000000" && sw.BYORGNO.Substring(6, 9) != "000000000")   //获取所有乡镇的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                    else if (sw.BYORGNO.Substring(9, 6) != "000000")   //获取所有村的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
            }
            if (!string.IsNullOrEmpty(sw.SEARCHTYPE))
                sb.AppendFormat(" AND SEARCHTYPE = '{0}'", ClsSql.EncodeSql(sw.SEARCHTYPE));
            if (!string.IsNullOrEmpty(sw.PESTBYCODE))
                sb.AppendFormat(" AND PESTBYCODE = '{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            string sql = "";
            if (sw.IsDistinctByPestCode)
            {
                sb.AppendFormat(" AND PESTBYCODE NOT IN ({0})", " SELECT TSPCODE FROM PEST_LOCALTREESPECIES ");
                sql = " SELECT DISTINCT PESTBYCODE " + sb.ToString();
            }
            else
                sql = " SELECT PEST_LOCALPESTJOINID, BYORGNO, PESTBYCODE, SEARCHTYPE " + sb.ToString() + " ORDER BY BYORGNO,SEARCHTYPE ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_LOCALPESTJOIN_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_LOCALPESTJOIN WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")  //获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000" && sw.BYORGNO.Substring(4, 11) != "00000000000") //获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000" && sw.BYORGNO.Substring(6, 9) != "000000000")   //获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(9, 6) != "000000")   //获取所有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.SEARCHTYPE))
                sb.AppendFormat(" AND SEARCHTYPE = '{0}'", ClsSql.EncodeSql(sw.SEARCHTYPE));
            if (!string.IsNullOrEmpty(sw.PESTBYCODE))
                sb.AppendFormat(" AND PESTBYCODE = '{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            string sql = " SELECT PEST_LOCALPESTJOINID, BYORGNO, PESTBYCODE, SEARCHTYPE " + sb.ToString() + " ORDER BY BYORGNO,SEARCHTYPE ";
            string sqlC = "SELECT Count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT2(PEST_LOCALPESTJOIN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_LOCALPESTJOIN a INNER JOIN PEST_LOCALTREESPECIES b on a.PESTBYCODE = b.TSPCODE WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (!string.IsNullOrEmpty(sw.SEARCHTYPE))
                sb.AppendFormat(" AND a.SEARCHTYPE = '{0}'", ClsSql.EncodeSql(sw.SEARCHTYPE));
            string sql = " SELECT a.PESTBYCODE " + sb.ToString() + " ORDER BY a.PESTBYCODE ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_LOCALPESTJOIN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_LOCALPESTJOIN where 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.SEARCHTYPE) == false)
                sb.AppendFormat(" and SEARCHTYPE='{0}'", ClsSql.EncodeSql(sw.SEARCHTYPE));
            if (string.IsNullOrEmpty(sw.PESTBYCODE) == false)
                sb.AppendFormat(" and PESTBYCODE='{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
