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
    /// 有害生物_报表_松材线虫病防治表
    /// </summary>
    public class PEST_REPORT_SCXCBFZ
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_SCXCBFZ_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrSCXCBFZAREA = m.SCXCBFZAREA.Split(',');
            string[] arrSCXCBFZPLANAREA = m.SCXCBFZPLANAREA.Split(',');
            string[] arrSCXCBFZFINISHAREA = m.SCXCBFZFINISHAREA.Split(',');
            if (arrBYORGNO.Length - 1 > 0)
            {
                #region 先删除
                List<string> sqldeletelist = new List<string>();

                #region 先删除防治明显表
                StringBuilder sbDeleteFZMX = new StringBuilder();
                sbDeleteFZMX.AppendFormat("Delete From PEST_REPORT_SCXCBFZMX where  1=1 ");
                sbDeleteFZMX.AppendFormat(" AND PEST_REPORT_SCXCBFZID IN (");
                sbDeleteFZMX.AppendFormat(" Select PEST_REPORT_SCXCBFZID From PEST_REPORT_SCXCBFZ WHERE 1=1 ");
                sbDeleteFZMX.AppendFormat(" AND SCXCBFZYEAR='{0}' ", ClsSql.EncodeSql(m.SCXCBFZYEAR));
                if (m.TopORGNO.Substring(4, 5) == "00000")
                    sbDeleteFZMX.AppendFormat(" AND SUBSTRING(BYORGNO,1,4)='{0}'", m.TopORGNO.Substring(0, 4));
                else if (m.TopORGNO.Substring(6, 3) == "000" && m.TopORGNO.Substring(4, 5) != "00000")
                    sbDeleteFZMX.AppendFormat(" AND SUBSTRING(BYORGNO,1,6)='{0}'", m.TopORGNO.Substring(0, 6));
                else
                    sbDeleteFZMX.AppendFormat(" AND BYORGNO='{0}'", m.TopORGNO);
                sbDeleteFZMX.AppendFormat(")");
                sqldeletelist.Add(sbDeleteFZMX.ToString());
                #endregion

                #region 再删除防治表
                StringBuilder sbDeleteFZ = new StringBuilder();
                sbDeleteFZ.AppendFormat("Delete From PEST_REPORT_SCXCBFZ where 1=1");
                sbDeleteFZ.AppendFormat(" AND SCXCBFZYEAR='{0}'", m.SCXCBFZYEAR);
                if (m.TopORGNO.Substring(4, 5) == "00000")
                    sbDeleteFZ.AppendFormat(" AND SUBSTRING(BYORGNO,1,4)='{0}'", m.TopORGNO.Substring(0, 4));
                else if (m.TopORGNO.Substring(6, 3) == "000" && m.TopORGNO.Substring(4, 5) != "00000")
                    sbDeleteFZ.AppendFormat(" AND SUBSTRING(BYORGNO,1,6)='{0}'", m.TopORGNO.Substring(0, 6));
                else
                    sbDeleteFZ.AppendFormat(" AND BYORGNO='{0}'", m.TopORGNO);
                sqldeletelist.Add(sbDeleteFZ.ToString());
                #endregion

                DataBaseClass.ExecuteSqlTran(sqldeletelist);
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_SCXCBFZ(BYORGNO, SCXCBFZYEAR, SCXCBFZAREA, SCXCBFZPLANAREA, SCXCBFZFINISHAREA)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new PEST_REPORT_SCXCBFZ_SW { BYORGNO = arrBYORGNO[i], SCXCBFZYEAR = m.SCXCBFZYEAR }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE PEST_REPORT_SCXCBFZ SET ");
                        sbUpdate.AppendFormat(" SCXCBFZAREA={0},", ClsSql.saveNullField(arrSCXCBFZAREA[i]));
                        sbUpdate.AppendFormat(" SCXCBFZPLANAREA={0},", ClsSql.saveNullField(arrSCXCBFZPLANAREA[i]));
                        sbUpdate.AppendFormat(" SCXCBFZFINISHAREA={0}", ClsSql.saveNullField(arrSCXCBFZFINISHAREA[i]));
                        sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbUpdate.AppendFormat(" and SCXCBFZYEAR= '{0}'", ClsSql.EncodeSql(m.SCXCBFZYEAR));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SCXCBFZYEAR));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrSCXCBFZAREA[i])); ;
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrSCXCBFZPLANAREA[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrSCXCBFZFINISHAREA[i]));
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
        public static DataTable getDT(PEST_REPORT_SCXCBFZ_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_SCXCBFZ WHERE 1=1");
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
            if (!string.IsNullOrEmpty(sw.SCXCBFZYEAR))
                sb.AppendFormat(" AND SCXCBFZYEAR = '{0}'", ClsSql.EncodeSql(sw.SCXCBFZYEAR));
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
        public static bool isExists(PEST_REPORT_SCXCBFZ_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_SCXCBFZ where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_SCXCBFZID) == false)
                sb.AppendFormat(" and PEST_REPORT_SCXCBFZID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_SCXCBFZID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.SCXCBFZYEAR) == false)
                sb.AppendFormat(" and SCXCBFZYEAR='{0}'", ClsSql.EncodeSql(sw.SCXCBFZYEAR));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
