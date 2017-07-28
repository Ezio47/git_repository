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
    /// 有害生物_报表_人财物表
    /// </summary>
    public class PEST_REPORT_RCW
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_RCW_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrRCWCODE = m.RCWCODE.Split(',');
            string[] arrRCWVALUE = m.RCWVALUE.Split(',');
            if (arrRCWCODE.Length - 1 > 0)
            {
                #region 先删除
                string sql = "delete from PEST_REPORT_RCW where  BYORGNO='" + m.BYORGNO + "' and RCWYEAR='" + m.RCWYEAR + "'";
                DataBaseClass.ExeSql(sql);
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_RCW(BYORGNO, RCWYEAR, RCWCODE, RCWVALUE)");
                for (int i = 0; i < arrRCWCODE.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new PEST_REPORT_RCW_SW { BYORGNO = m.BYORGNO, RCWYEAR = m.RCWYEAR }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE PEST_REPORT_RCW SET ");
                        sbUpdate.AppendFormat(" RCWVALUE='{0}',", ClsSql.saveNullField(arrRCWVALUE[i]));
                        sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                        sbUpdate.AppendFormat(" and RCWYEAR= '{0}'", ClsSql.EncodeSql(m.RCWYEAR));
                        sbUpdate.AppendFormat(" and RCWCODE= '{0}'", ClsSql.EncodeSql(arrRCWCODE[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RCWYEAR)); ;
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrRCWCODE[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrRCWVALUE[i]));
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
        public static DataTable getDT(PEST_REPORT_RCW_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_RCW WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (!string.IsNullOrEmpty(sw.RCWYEAR))
                sb.AppendFormat(" AND RCWYEAR = '{0}'", ClsSql.EncodeSql(sw.RCWYEAR));
            if (!string.IsNullOrEmpty(sw.RCWCODE))
                sb.AppendFormat(" AND RCWCODE = '{0}'", ClsSql.EncodeSql(sw.RCWCODE));
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
        public static bool isExists(PEST_REPORT_RCW_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_RCW where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_RCWID) == false)
                sb.AppendFormat(" and PEST_REPORT_RCWID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_RCWID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.RCWYEAR) == false)
                sb.AppendFormat(" and RCWYEAR='{0}'", ClsSql.EncodeSql(sw.RCWYEAR));
            if (string.IsNullOrEmpty(sw.RCWCODE) == false)
                sb.AppendFormat(" and RCWCODE='{0}'", ClsSql.EncodeSql(sw.RCWCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
