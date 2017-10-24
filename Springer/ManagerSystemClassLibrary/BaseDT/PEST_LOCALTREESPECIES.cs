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
    /// 有害生物_本地化树种信息表
    /// </summary>
    public class PEST_LOCALTREESPECIES
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_LOCALTREESPECIES_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrTSPCODE = m.TSPCODE.Split(','); ;
            StringBuilder sbInsert = new StringBuilder();
            if (arrTSPCODE.Length > 0)
            {
                sbInsert.AppendFormat("INSERT INTO PEST_LOCALTREESPECIES(BYORGNO,TSPCODE) ");
                for (int i = 0; i < arrTSPCODE.Length; i++)
                {
                    if (!isExists(new PEST_LOCALTREESPECIES_SW { BYORGNO = m.BYORGNO, TSPCODE = arrTSPCODE[i] }))
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrTSPCODE[i]));
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
                return new Message(true, "该树种已关联!", "");
            else
                return new Message(false, "添加失败,事物回滚!", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_LOCALTREESPECIES_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrId = m.PEST_LOCALTREESPECIESID.Split(',');
            string[] arrTSPAREA = m.TSPAREA.Split(',');
            if (arrId.Length > 1)
            {
                for (int i = 0; i < arrId.Length - 1; i++)
                {
                    if (isExists(new PEST_LOCALTREESPECIES_SW { PEST_LOCALTREESPECIESID = arrId[i] }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE PEST_LOCALTREESPECIES SET ");
                        sbUpdate.AppendFormat(" TSPAREA={0}", ClsSql.saveNullField(arrTSPAREA[i]));
                        sbUpdate.AppendFormat(" where PEST_LOCALTREESPECIESID= '{0}'", ClsSql.EncodeSql(arrId[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                }
            }
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y >= 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，事物回滚机制!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_LOCALTREESPECIES_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Delete From  PEST_LOCALTREESPECIES WHERE 1=1 ");
            sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(" AND TSPCODE  IN ({0})", ClsSql.SwitchStrToSqlIn(m.TSPCODE));
            sqllist.Add(sb.ToString());
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败,事物回滚!", "");
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_LOCALTREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_LOCALTREESPECIES where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_LOCALTREESPECIESID) == false)
                sb.AppendFormat(" and PEST_LOCALTREESPECIESID='{0}'", ClsSql.EncodeSql(sw.PEST_LOCALTREESPECIESID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.TSPCODE) == false)
                sb.AppendFormat(" and TSPCODE='{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_LOCALTREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_LOCALTREESPECIES WHERE 1=1");
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
            if (!string.IsNullOrEmpty(sw.TSPCODE))
                sb.AppendFormat(" AND TSPCODE = '{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            if (!string.IsNullOrEmpty(sw.TSPAREA))
                sb.AppendFormat(" AND TSPAREA = '{0}'", ClsSql.EncodeSql(sw.TSPAREA));
            string sql = " SELECT PEST_LOCALTREESPECIESID, BYORGNO, TSPCODE, TSPAREA " + sb.ToString() + " ORDER BY BYORGNO ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_LOCALTREESPECIES_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_LOCALTREESPECIES WHERE 1=1");
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
            if (!string.IsNullOrEmpty(sw.TSPCODE))
                sb.AppendFormat(" AND TSPCODE = '{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            if (!string.IsNullOrEmpty(sw.TSPAREA))
                sb.AppendFormat(" AND TSPAREA = '{0}'", ClsSql.EncodeSql(sw.TSPAREA));
            string sql = " SELECT PEST_LOCALTREESPECIESID, BYORGNO, TSPCODE, TSPAREA " + sb.ToString() + " ORDER BY BYORGNO ";
            string sqlC = "SELECT Count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 获取有害生物面积列表
        /// <summary>
        /// 获取面积列表
        /// </summary>
        /// <param name="PESTCODE">有害生物编码</param>
        /// <returns></returns>
        public static DataTable getAREADT(string PESTCODE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT BYORGNO,TSPCODE,TSPAREA FROM PEST_LOCALTREESPECIES WHERE 1=1");
            if (!string.IsNullOrEmpty(PESTCODE))
                sb.AppendFormat(" AND TSPCODE in (SELECT TREESPECIESCODE  from PEST_TREESPECIES_PEST where  PESTBYCODE='{0}')", ClsSql.EncodeSql(PESTCODE));
            sb.AppendFormat(" ORDER BY BYORGNO");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
