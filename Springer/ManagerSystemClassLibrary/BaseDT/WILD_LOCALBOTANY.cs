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
    public class WILD_LOCALBOTANY
    {

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(WILD_LOCALBOTANY_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrBIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE.Split(',');
            StringBuilder sb = new StringBuilder();
            if (arrBIOLOGICALTYPECODE.Length > 0)
            {
                sb.AppendFormat("INSERT INTO WILD_LOCALBOTANY(BYORGNO, BIOLOGICALTYPECODE) ");
                for (int i = 0; i < arrBIOLOGICALTYPECODE.Length; i++)
                {
                    if (!isExists(new WILD_LOCALBOTANY_SW { BYORGNO = m.BYORGNO, BIOLOGICALTYPECODE = arrBIOLOGICALTYPECODE[i] }))
                    {
                        sb.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrBIOLOGICALTYPECODE[i]));
                        sb.AppendFormat(" UNION ALL ");
                    }
                }
                string insertStr = sb.ToString();
                if (insertStr.Contains(" UNION ALL "))
                {
                    insertStr = insertStr.Substring(0, insertStr.Length - 10);
                    sqllist.Add(insertStr);
                }
            }
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败，事物回滚!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(WILD_LOCALBOTANY_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Delete From  WILD_LOCALBOTANY WHERE 1=1 ");
            sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(" AND BIOLOGICALTYPECODE  IN ({0})", ClsSql.SwitchStrToSqlIn(m.BIOLOGICALTYPECODE));
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
        public static DataTable getDT(WILD_LOCALBOTANY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT WILD_LOCALBOTANYID, BYORGNO, BIOLOGICALTYPECODE FROM WILD_LOCALBOTANY WHERE 1=1");
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
            {
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            }
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
            if (!string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE))
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            string sql = sb.ToString() + "ORDER BY BYORGNO";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 获取数据列表(分页)
        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总数</param>
        /// <returns></returns>
        public static DataTable getDT(WILD_LOCALBOTANY_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  FROM WILD_LOCALBOTANY WHERE 1=1");
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
            if (!string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE))
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));

            string sql = "SELECT WILD_LOCALBOTANYID, BYORGNO, BIOLOGICALTYPECODE " + sb.ToString() + "ORDER BY BYORGNO";
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
        public static bool isExists(WILD_LOCALBOTANY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from WILD_LOCALBOTANY where 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" and BIOLOGICALTYPECODE='{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取不重复的数据列表
        /// <summary>
        /// 获取不重复的数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDISDT(WILD_LOCALBOTANY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT DISTINCT(BIOLOGICALTYPECODE)  FROM WILD_LOCALBOTANY WHERE 1=1");
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
            {
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            }
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
            if (!string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE))
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            string sql = sb.ToString();
            //string sql = sb.ToString() + "ORDER BY BYORGNO";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
