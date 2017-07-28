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
    /// 有害生物_报表_枯死松树调查表
    /// </summary>
    public class PEST_REPORT_DIEPINESURVEY
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_REPORT_DIEPINESURVEY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO PEST_REPORT_DIEPINESURVEY(BYORGNO, FINDER, FINDDATE, LINKTELL, DIEPINECOUNT, REPORTDATE, SAMPLINGCOUNT, AUTHENTICATERESULT)");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FINDER));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FINDDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LINKTELL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DIEPINECOUNT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.REPORTDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SAMPLINGCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.AUTHENTICATERESULT));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", m.returnUrl);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确!", m.returnUrl);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_REPORT_DIEPINESURVEY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_REPORT_DIEPINESURVEY");
            sb.AppendFormat(" SET ");
            sb.AppendFormat("BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",FINDER='{0}'", ClsSql.EncodeSql(m.FINDER));
            sb.AppendFormat(",FINDDATE='{0}'", ClsSql.EncodeSql(m.FINDDATE));
            sb.AppendFormat(",LINKTELL='{0}'", ClsSql.EncodeSql(m.LINKTELL));
            sb.AppendFormat(",DIEPINECOUNT={0}", ClsSql.saveNullField(m.DIEPINECOUNT));
            sb.AppendFormat(",REPORTDATE='{0}'", ClsSql.EncodeSql(m.REPORTDATE));
            sb.AppendFormat(",SAMPLINGCOUNT='{0}'", ClsSql.EncodeSql(m.SAMPLINGCOUNT));
            sb.AppendFormat(",AUTHENTICATERESULT={0}", ClsSql.saveNullField(m.AUTHENTICATERESULT));
            sb.AppendFormat(" WHERE PEST_REPORT_DIEPINESURVEYID= '{0}'", ClsSql.EncodeSql(m.PEST_REPORT_DIEPINESURVEYID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.returnUrl);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确!", m.returnUrl);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_REPORT_DIEPINESURVEY_Model m)
        {
            string[] arrDIEPINESURVEYID = m.PEST_REPORT_DIEPINESURVEYID.Split(',');
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM PEST_REPORT_DIEPINESURVEY WHERE PEST_REPORT_DIEPINESURVEYID in (");
            for (int i = 0; i < arrDIEPINESURVEYID.Length; i++)
            {
                if (i != arrDIEPINESURVEYID.Length - 1)
                    sb.AppendFormat("'{0}',", ClsSql.EncodeSql(arrDIEPINESURVEYID[i]));
                else
                    sb.AppendFormat("'{0}'", ClsSql.EncodeSql(arrDIEPINESURVEYID[i]));
            }
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", m.returnUrl);
            else
                return new Message(false, "删除失败!", m.returnUrl);
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_REPORT_DIEPINESURVEY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELETE 1 FROM PEST_REPORT_DIEPINESURVEY WHERE 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_DIEPINESURVEYID) == false)
                sb.AppendFormat(" AND PEST_REPORT_DIEPINESURVEYID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_DIEPINESURVEYID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(PEST_REPORT_DIEPINESURVEY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM  PEST_REPORT_DIEPINESURVEY  WHERE  1=1");
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.PEST_REPORT_DIEPINESURVEYID))
                sb.AppendFormat(" AND PEST_REPORT_DIEPINESURVEYID = '{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_DIEPINESURVEYID));
            if (!string.IsNullOrEmpty(sw.FINDER))
                sb.AppendFormat(" AND FINDER LIKE '%{0}%'", ClsSql.EncodeSql(sw.FINDER));
            if (!string.IsNullOrEmpty(sw.FINDDATE))
                sb.AppendFormat(" AND FINDDATE >= '{0}'", ClsSql.EncodeSql(sw.FINDDATE));
            sb.AppendFormat(" ORDER BY FINDDATE DESC,BYORGNO");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取数据列表-分页
        /// </summary>
        /// <param name="sw">查询模型</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        public static DataTable getDT(PEST_REPORT_DIEPINESURVEY_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_REPORT_DIEPINESURVEY  WHERE 1=1");

            #region 查询条件
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")  //获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000") //获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.FINDER))
                sb.AppendFormat(" AND FINDER LIKE '%{0}%'", ClsSql.EncodeSql(sw.FINDER));
            if (!string.IsNullOrEmpty(sw.STARTDATE))
                sb.AppendFormat(" AND FINDDATE >= '{0}'", DateTime.Parse(sw.STARTDATE).ToString("yyyy-MM-dd HH:mm:ss"));
            if (!string.IsNullOrEmpty(sw.ENDDATE))
                sb.AppendFormat(" AND FINDDATE <= '{0}'", DateTime.Parse(sw.ENDDATE).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"));
            #endregion

            string sql = "SELECT * " + sb.ToString() + " ORDER BY FINDDATE DESC,BYORGNO ";
            string sqlC = "SELECT COUNT(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "PEST_REPORT_DIEPINESURVEY");
            return ds.Tables[0];
        }
        #endregion

        #region 获取最新序号
        /// <summary>
        /// 获取最新序号
        /// </summary>
        /// <returns></returns>
        public static string GetMaxID()
        {
            string sql = "select max(PEST_REPORT_DIEPINESURVEYID) from PEST_REPORT_DIEPINESURVEY";
            return DataBaseClass.ReturnSqlField(sql);
        }
        #endregion
    }
}
