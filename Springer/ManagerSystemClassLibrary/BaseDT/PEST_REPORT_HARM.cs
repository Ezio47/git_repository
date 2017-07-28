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
    /// 有害生物_报表_成灾表
    /// </summary>
    public class PEST_REPORT_HARM
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_HARM_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrDISASTERAREA = m.DISASTERAREA.Split(',');
            string[] arrFORECASTDISASTERAREA = m.FORECASTDISASTERAREA.Split(',');
            string[] arrDIEPLATECOUNT = m.DIEPLATECOUNT.Split(',');
            if (arrBYORGNO.Length - 1 > 0)
            {
                #region 先删除
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.AppendFormat("delete from PEST_REPORT_HARM where 1=1");
                sbDelete.AppendFormat(" and PESTBYCODE='{0}'", m.PESTBYCODE);
                sbDelete.AppendFormat(" and HAPPENYEAR='{0}'", m.HAPPENYEAR);
                sbDelete.AppendFormat(" and HAPPENMONTH='{0}'", m.HAPPENMONTH);
                if (m.TopORGNO.Substring(4, 5) == "00000")
                    sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,4)='{0}'", m.TopORGNO.Substring(0, 4));
                else if (m.TopORGNO.Substring(6, 3) == "000" && m.TopORGNO.Substring(4, 5) != "00000")
                    sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,6)='{0}'", m.TopORGNO.Substring(0, 6));
                else
                    sbDelete.AppendFormat(" AND BYORGNO='{0}'", m.TopORGNO);
                DataBaseClass.ExeSql(sbDelete.ToString());
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_HARM(BYORGNO, HAPPENYEAR, HAPPENMONTH, PESTBYCODE, DISASTERAREA, FORECASTDISASTERAREA, DIEPLATECOUNT)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new PEST_REPORT_HARM_SW { BYORGNO = arrBYORGNO[i], HAPPENYEAR = m.HAPPENYEAR, HAPPENMONTH = m.HAPPENMONTH, PESTBYCODE = m.PESTBYCODE }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE PEST_REPORT_HARM SET ");
                        sbUpdate.AppendFormat(" DISASTERAREA={0},", ClsSql.saveNullField(arrDISASTERAREA[i]));
                        sbUpdate.AppendFormat(" FORECASTDISASTERAREA={0},", ClsSql.saveNullField(arrFORECASTDISASTERAREA[i]));
                        sbUpdate.AppendFormat(" DIEPLATECOUNT={0},", ClsSql.saveNullField(arrDIEPLATECOUNT[i]));
                        sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbUpdate.AppendFormat(" and HAPPENYEAR= '{0}'", ClsSql.EncodeSql(m.HAPPENYEAR));
                        sbUpdate.AppendFormat(" and HAPPENMONTH= '{0}'", ClsSql.EncodeSql(m.HAPPENMONTH));
                        sbUpdate.AppendFormat(" and PESTBYCODE= '{0}'", ClsSql.EncodeSql(m.PESTBYCODE));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HAPPENYEAR));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HAPPENMONTH));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PESTBYCODE));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrDISASTERAREA[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrFORECASTDISASTERAREA[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrDIEPLATECOUNT[i]));
                        sbInsert.AppendFormat(" UNION ALL ");
                    }
                    #endregion
                }
                string insertStr = sbInsert.ToString();
                if (insertStr.Contains(" UNION ALL "))
                {
                    insertStr = insertStr.Substring(0, insertStr.Length - 10);
                    sqllist.Add(insertStr);
                }
                #endregion
            }
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y >= 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，事物回滚机制!", "");
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_REPORT_HARM_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_HARM WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.HAPPENYEAR))
                sb.AppendFormat(" AND HAPPENYEAR = '{0}'", ClsSql.EncodeSql(sw.HAPPENYEAR));
            if (!string.IsNullOrEmpty(sw.HAPPENMONTH))
                sb.AppendFormat(" AND HAPPENMONTH = '{0}'", ClsSql.EncodeSql(sw.HAPPENMONTH));
            if (!string.IsNullOrEmpty(sw.PESTBYCODE))
                sb.AppendFormat(" AND PESTBYCODE = '{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_REPORT_HARM_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_HARM where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_HARMID) == false)
                sb.AppendFormat(" and PEST_REPORT_HARMID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_HARMID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.HAPPENYEAR) == false)
                sb.AppendFormat(" and HAPPENYEAR='{0}'", ClsSql.EncodeSql(sw.HAPPENYEAR));
            if (string.IsNullOrEmpty(sw.HAPPENMONTH) == false)
                sb.AppendFormat(" and HAPPENMONTH='{0}'", ClsSql.EncodeSql(sw.HAPPENMONTH));
            if (string.IsNullOrEmpty(sw.PESTBYCODE) == false)
                sb.AppendFormat(" and PESTBYCODE='{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
