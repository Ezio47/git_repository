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
    /// 有害生物_报表_目标考核表
    /// </summary>
    public class PEST_REPORT_ASSESSINGTARGET
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_ASSESSINGTARGET_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrASSESSINGTARGETTYPECODE = m.ASSESSINGTARGETTYPECODE.Split(',');
            string[] arrASSESSINGTARGETVALUE = m.ASSESSINGTARGETVALUE.Split(',');
            if (arrBYORGNO.Length - 1 > 0)
            {
                #region 先删除
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.AppendFormat("delete from PEST_REPORT_ASSESSINGTARGET where 1=1");
                sbDelete.AppendFormat(" and RCWYEAR='{0}'", m.RCWYEAR);
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
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_ASSESSINGTARGET(BYORGNO, RCWYEAR, ASSESSINGTARGETTYPECODE, ASSESSINGTARGETVALUE)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new PEST_REPORT_ASSESSINGTARGET_SW { BYORGNO = arrBYORGNO[i], RCWYEAR = m.RCWYEAR, ASSESSINGTARGETTYPECODE = arrASSESSINGTARGETTYPECODE[i] }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE PEST_REPORT_ASSESSINGTARGET SET ");
                        sbUpdate.AppendFormat(" ASSESSINGTARGETVALUE='{0}',", ClsSql.saveNullField(arrASSESSINGTARGETVALUE[i]));
                        sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbUpdate.AppendFormat(" and RCWYEAR= '{0}'", ClsSql.EncodeSql(m.RCWYEAR));
                        sbUpdate.AppendFormat(" and ASSESSINGTARGETTYPECODE= '{0}'", ClsSql.EncodeSql(arrASSESSINGTARGETVALUE[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RCWYEAR)); ;
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrASSESSINGTARGETTYPECODE[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrASSESSINGTARGETVALUE[i]));
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
        public static DataTable getDT(PEST_REPORT_ASSESSINGTARGET_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_ASSESSINGTARGET WHERE 1=1");
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
            if (!string.IsNullOrEmpty(sw.RCWYEAR))
                sb.AppendFormat(" AND RCWYEAR = '{0}'", ClsSql.EncodeSql(sw.RCWYEAR));
            if (!string.IsNullOrEmpty(sw.ASSESSINGTARGETTYPECODE))
                sb.AppendFormat(" AND ASSESSINGTARGETTYPECODE = '{0}'", ClsSql.EncodeSql(sw.ASSESSINGTARGETTYPECODE));
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
        public static bool isExists(PEST_REPORT_ASSESSINGTARGET_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_ASSESSINGTARGET where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_ASSESSINGTARGETID) == false)
                sb.AppendFormat(" and PEST_REPORT_ASSESSINGTARGETID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_ASSESSINGTARGETID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.RCWYEAR) == false)
                sb.AppendFormat(" and RCWYEAR='{0}'", ClsSql.EncodeSql(sw.RCWYEAR));
            if (string.IsNullOrEmpty(sw.ASSESSINGTARGETTYPECODE) == false)
                sb.AppendFormat(" and ASSESSINGTARGETTYPECODE='{0}'", ClsSql.EncodeSql(sw.ASSESSINGTARGETTYPECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
