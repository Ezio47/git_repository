using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 森林防火组织机构统计
    /// </summary>
    public class FIRERECORD_REPORT8
    {
        #region 添加
        /// <summary>
        /// 添加森林防火组织机构统计
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRERECORD_REPORT8_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrREPORTCODE = m.REPORTCODE.Split(',');
            string[] arrSSXTYPELEVELCODE = m.SSXTYPELEVELCODE.Split(',');
            string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');
            for (int i = 0; i < arrREPORTVALUE.Length-1; i++)
            { 
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT  INTO  FIRERECORD_REPORT8(BYORGNO,REPORTYEAR,REPORTCODE,SSXTYPELEVELCODE,REPORTVALUE)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.REPORTYEAR));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTCODE[i]));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrSSXTYPELEVELCODE[i]));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTVALUE[i]));
                sb.AppendFormat(")");
                sqllist.Add(sb.ToString());
            }
            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j > 0)
            {
                return new Message(true, "添加成功！", "");
            }
            else
            {
                return new Message(false, "保存失败，事物回滚机制！", "");
            }
        }
       
        #endregion

        #region 获取报表数据列表
        /// <summary>
        /// 获取报表数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDT(FIRERECORD_REPORT8_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_REPORT8 WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.FIRERECORD_REPORT8ID))
                sb.AppendFormat(" AND FIRERECORD_REPORT8ID = '{0}'", sw.FIRERECORD_REPORT8ID);
            if (!string.IsNullOrEmpty(sw.BYORGNO))
                sb.AppendFormat(" AND BYORGNO = '{0}'", sw.BYORGNO);
            if (!string.IsNullOrEmpty(sw.REPORTYEAR))
                sb.AppendFormat(" AND REPORTYEAR = '{0}'", sw.REPORTYEAR);
            if (!string.IsNullOrEmpty(sw.REPORTCODE))
                sb.AppendFormat(" AND REPORTCODE = '{0}'", sw.REPORTCODE);
            if (!string.IsNullOrEmpty(sw.SSXTYPELEVELCODE))
                sb.AppendFormat(" AND SSXTYPELEVELCODE = '{0}'", sw.SSXTYPELEVELCODE);
            if (!string.IsNullOrEmpty(sw.REPORTVALUE))
                sb.AppendFormat(" AND REPORTVALUE = '{0}'", sw.REPORTVALUE);
            sb.AppendFormat(" ORDER BY FIRERECORD_REPORT8ID");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        //#region 获取火情档案数据列表
        ///// <summary>
        ///// 获取火情档案数据列表
        ///// </summary>
        ///// <param name="sw">参见模型</param>
        ///// <returns></returns>
        //public static DataTable getDT2(FIRERECORD_FIREINFO_SW sw)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("SELECT *  FROM   FIRERECORD_FIREINFO WHERE  1=1");
        //    if (!string.IsNullOrEmpty(sw.FRFIID))
        //        sb.AppendFormat(" AND FRFIID = '{0}'", sw.FRFIID);
        //    if (!string.IsNullOrEmpty(sw.JCFID))
        //        sb.AppendFormat(" AND JCFID = '{0}'", sw.JCFID);
        //    sb.AppendFormat(" ORDER BY FRFIID");
        //    DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// 分页获取火情档案数据列表
        ///// </summary>
        ///// <param name="sw">参见模型</param>
        ///// <param name="total"></param>
        ///// <returns></returns>
        //public static DataTable getDT(FIRERECORD_FIREINFO_SW sw, out int total)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat(" FROM  FIRERECORD_FIREINFO WHERE  1=1");
        //    if (!string.IsNullOrEmpty(sw.FIREADDRESSTOWNS))
        //    {
        //        if (sw.FIREADDRESSTOWNS.Substring(4, 5) == "00000")//获取所有市的
        //            sb.AppendFormat(" AND (SUBSTRING(FIREADDRESSTOWNS,1,4) = '{0}')", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS.Substring(0, 4)));
        //        else if (sw.FIREADDRESSTOWNS.Substring(4, 5) == "xxxxx")//单独市
        //            sb.AppendFormat(" AND (SUBSTRING(FIREADDRESSTOWNS,1,9) = '{0}')", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS.Substring(0, 4) + "00000"));
        //        else if (sw.FIREADDRESSTOWNS.Substring(6, 3) == "xxx")//获取所有县的
        //            sb.AppendFormat(" AND (SUBSTRING(FIREADDRESSTOWNS,1,6) = '{0}' )", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS.Substring(0, 6)));
        //        else
        //            sb.AppendFormat(" AND FIREADDRESSTOWNS = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
        //    }
        //    if (!string.IsNullOrEmpty(sw.FIRETIME))
        //        sb.AppendFormat(" AND FIRETIME>='{0}'", sw.FIRETIME);
        //    if (!string.IsNullOrEmpty(sw.FIREENDTIME))
        //        sb.AppendFormat(" AND FIREENDTIME<='{0}'", sw.FIREENDTIME);
        //    string sql = "SELECT * " + sb.ToString() + " ORDER BY FRFIID ";
        //    string sqlC = "select count(1) " + sb.ToString();
        //    total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
        //    sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
        //    DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
        //    return ds.Tables[0];
        //}
        //#endregion

        //#region 判断记录是否存在
        ///// <summary>
        ///// 判断记录是否存在
        ///// </summary>
        ///// <param name="sw">参见模型</param>
        ///// <returns>true存在 false不存在 </returns>
        //public static bool isExists(FIRERECORD_FIREINFO_SW sw)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("select 1 from FIRERECORD_FIREINFO where 1=1");
        //    if (string.IsNullOrEmpty(sw.FRFIID) == false)
        //        sb.AppendFormat(" and FRFIID='{0}'", ClsSql.EncodeSql(sw.FRFIID));
        //    return DataBaseClass.JudgeRecordExists(sb.ToString());
        //}
        //#endregion


    }
}
