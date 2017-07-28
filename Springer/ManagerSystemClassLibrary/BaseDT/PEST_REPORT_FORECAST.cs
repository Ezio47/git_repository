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
    /// 有害生物_报表_预测表
    /// </summary>
    public class PEST_REPORT_FORECAST
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_FORECAST_Model m)
        {
            List<string> sqllist = new List<string>();           
            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrFORECASTSTAGECODE = m.FORECASTSTAGECODE.Split(',');
            string[] arrFORECASTAREA = m.FORECASTAREA.Split(',');           
            if (arrBYORGNO.Length - 1 > 0)
            {
                #region 先删除
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.AppendFormat("delete from PEST_REPORT_FORECAST where 1=1");
                sbDelete.AppendFormat(" and FORECASTYEAR='{0}'", m.FORECASTAREA);
                sbDelete.AppendFormat(" and PESTBYCODE='{0}'", m.PESTBYCODE);
                if (m.TopORGNO.Substring(4, 5) == "00000")
                    sbDelete.AppendFormat(" and SUBSTRING(BYORGNO,1,4)='{0}'", m.TopORGNO.Substring(0, 4));
                else if (m.TopORGNO.Substring(6, 3) == "000" && m.TopORGNO.Substring(4, 5) != "00000")
                    sbDelete.AppendFormat(" and SUBSTRING(BYORGNO,1,6)='{0}'", m.TopORGNO.Substring(0, 6));
                else
                    sbDelete.AppendFormat(" and BYORGNO='{0}'", m.TopORGNO);
                DataBaseClass.ExeSql(sbDelete.ToString());
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_FORECAST( BYORGNO, FORECASTYEAR, PESTBYCODE, FORECASTSTAGECODE, FORECASTAREA)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new PEST_REPORT_FORECAST_SW { BYORGNO = arrBYORGNO[i], FORECASTYEAR = m.FORECASTAREA, PESTBYCODE = m.PESTBYCODE, FORECASTSTAGECODE = arrFORECASTSTAGECODE[i] }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE PEST_REPORT_FORECAST SET ");
                        sbUpdate.AppendFormat(" FORECASTAREA='{0}',", ClsSql.saveNullField(arrFORECASTAREA[i]));
                        sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbUpdate.AppendFormat(" and FORECASTYEAR= '{0}'", ClsSql.EncodeSql(m.FORECASTYEAR));
                        sbUpdate.AppendFormat(" and PESTBYCODE= '{0}'", ClsSql.EncodeSql(m.PESTBYCODE));
                        sbUpdate.AppendFormat(" and FORECASTSTAGECODE= '{0}'", ClsSql.EncodeSql(arrFORECASTSTAGECODE[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FORECASTYEAR));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PESTBYCODE)); ;
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrFORECASTSTAGECODE[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrFORECASTAREA[i]));
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
        public static DataTable getDT(PEST_REPORT_FORECAST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_FORECAST WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.FORECASTYEAR))
                sb.AppendFormat(" AND FORECASTYEAR = '{0}'", ClsSql.EncodeSql(sw.FORECASTYEAR));
            if (!string.IsNullOrEmpty(sw.PESTBYCODE))
                sb.AppendFormat(" AND PESTBYCODE = '{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            if (!string.IsNullOrEmpty(sw.FORECASTSTAGECODE))
                sb.AppendFormat(" AND FORECASTSTAGECODE = '{0}'", ClsSql.EncodeSql(sw.FORECASTSTAGECODE));
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
        public static bool isExists(PEST_REPORT_FORECAST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_FORECAST where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_FORECASTID) == false)
                sb.AppendFormat(" and PEST_REPORT_FORECASTID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_FORECASTID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.FORECASTYEAR) == false)
                sb.AppendFormat(" and FORECASTYEAR='{0}'", ClsSql.EncodeSql(sw.FORECASTYEAR));
            if (string.IsNullOrEmpty(sw.PESTBYCODE) == false)
                sb.AppendFormat(" and PESTBYCODE='{0}'", ClsSql.EncodeSql(sw.PESTBYCODE));
            if (string.IsNullOrEmpty(sw.FORECASTSTAGECODE) == false)
                sb.AppendFormat(" and FORECASTSTAGECODE='{0}'", ClsSql.EncodeSql(sw.FORECASTSTAGECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
