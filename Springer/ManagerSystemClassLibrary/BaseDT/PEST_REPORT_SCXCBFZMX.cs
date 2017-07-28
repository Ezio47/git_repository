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
    /// 有害生物_报表_松材线虫病防治明细表
    /// </summary>
    public class PEST_REPORT_SCXCBFZMX
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_SCXCBFZMX_Model m)
        {
            List<string> sqllist = new List<string>();

            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrSCXCBFZMXTYPEID = m.SCXCBFZMXTYPEID.Split(',');
            string[] arrSCXCBFZMXTYPEVALUE = m.SCXCBFZMXTYPEVALUE.Split(',');
            string[] arrSCXCBFZMXVARCHAR = m.SCXCBFZMXVARCHAR.Split(',');

            #region 更新
            if (arrSCXCBFZMXTYPEID.Length - 1 > 0)
            {
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_SCXCBFZMX( PEST_REPORT_SCXCBFZID, SCXCBFZMXTYPEID, SCXCBFZMXTYPEVALUE, SCXCBFZMXVARCHAR)");
                for (int i = 0; i < arrSCXCBFZMXTYPEID.Length - 1; i++)
                {
                    string sql = "Select PEST_REPORT_SCXCBFZID From PEST_REPORT_SCXCBFZ WHERE BYORGNO='" + arrBYORGNO[i] + "' AND SCXCBFZYEAR='" + m.SCXCBFZYEAR + "'";
                    string SCXCBFZID = DataBaseClass.ReturnSqlField(sql);
                    if (SCXCBFZID != "")
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(SCXCBFZID));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrSCXCBFZMXTYPEID[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrSCXCBFZMXTYPEVALUE[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrSCXCBFZMXVARCHAR[i]));
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
            #endregion

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
        public static DataTable getDT(PEST_REPORT_SCXCBFZMX_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_SCXCBFZMX WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.PEST_REPORT_SCXCBFZMXID))
                sb.AppendFormat(" AND PEST_REPORT_SCXCBFZMXID = '{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_SCXCBFZMXID));
            if (!string.IsNullOrEmpty(sw.PEST_REPORT_SCXCBFZID))
                sb.AppendFormat(" AND PEST_REPORT_SCXCBFZID = '{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_SCXCBFZID));
            if (!string.IsNullOrEmpty(sw.SCXCBFZMXTYPEID))
                sb.AppendFormat(" AND SCXCBFZMXTYPEID = '{0}'", ClsSql.EncodeSql(sw.SCXCBFZMXTYPEID));
            if (!string.IsNullOrEmpty(sw.SCXCBFZMXTYPEVALUE))
                sb.AppendFormat(" AND SCXCBFZMXTYPEVALUE = '{0}'", ClsSql.EncodeSql(sw.SCXCBFZMXTYPEVALUE));
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
        public static bool isExists(PEST_REPORT_SCXCBFZMX_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_SCXCBFZMX where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_SCXCBFZMXID) == false)
                sb.AppendFormat(" and PEST_REPORT_SCXCBFZMXID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_SCXCBFZMXID));
            if (string.IsNullOrEmpty(sw.PEST_REPORT_SCXCBFZID) == false)
                sb.AppendFormat(" and PEST_REPORT_SCXCBFZID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_SCXCBFZID));
            if (string.IsNullOrEmpty(sw.SCXCBFZMXTYPEID) == false)
                sb.AppendFormat(" and SCXCBFZMXTYPEID='{0}'", ClsSql.EncodeSql(sw.SCXCBFZMXTYPEID));
            if (string.IsNullOrEmpty(sw.SCXCBFZMXTYPEVALUE) == false)
                sb.AppendFormat(" and SCXCBFZMXTYPEVALUE='{0}'", ClsSql.EncodeSql(sw.SCXCBFZMXTYPEVALUE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
