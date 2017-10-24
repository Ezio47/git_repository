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
    /// 有害生物_报表_松材线虫病普查表
    /// </summary>
    public class PEST_REPORT_SCXCBPC
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_REPORT_SCXCBPC_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrSCXCBPCTYPECODE = m.SCXCBPCTYPECODE.Split(',');
            string[] arrSCXCBPCVALUE = m.SCXCBPCVALUE.Split(',');
            if (arrBYORGNO.Length - 1 > 0)
            {
                #region 先删除
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.AppendFormat("Delete from PEST_REPORT_SCXCBPC WHERE 1=1 ");
                sbDelete.AppendFormat(" AND SCXCBPCYEAR='{0}'", m.SCXCBPCYEAR);
                sbDelete.AppendFormat(" AND SCXCBPCSEASONCODE='{0}'", m.SCXCBPCSEASONCODE);
                if (m.TopORGNO.Substring(4, 11) == "00000000000") //所有市
                    sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,4)='{0}'", m.TopORGNO.Substring(0, 4));
                else if (m.TopORGNO.Substring(6, 9) == "000000000" && m.TopORGNO.Substring(4, 11) != "00000000000") //所有县
                    sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,6)='{0}'", m.TopORGNO.Substring(0, 6));
                else if (m.TopORGNO.Substring(9, 6) == "000000" && m.TopORGNO.Substring(6, 9) != "000000000") //所有乡镇
                    sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,9)='{0}'", m.TopORGNO.Substring(0, 9));
                else if (m.TopORGNO.Substring(9, 6) != "000000") //所有村
                    sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,12)='{0}'", m.TopORGNO.Substring(0, 12));
                else
                    sbDelete.AppendFormat(" AND BYORGNO='{0}'", m.TopORGNO);
                DataBaseClass.ExeSql(sbDelete.ToString());
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_REPORT_SCXCBPC( BYORGNO, SCXCBPCYEAR, SCXCBPCSEASONCODE, SCXCBPCTYPECODE, SCXCBPCVALUE)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SCXCBPCYEAR));
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SCXCBPCSEASONCODE)); ;
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrSCXCBPCTYPECODE[i]));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrSCXCBPCVALUE[i]));
                    if (i != arrBYORGNO.Length - 2)
                        sbInsert.AppendFormat(" UNION ALL ");
                }
                sqllist.Add(sbInsert.ToString());
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
        public static DataTable getDT(PEST_REPORT_SCXCBPC_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_REPORT_SCXCBPC WHERE 1=1");
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
            if (!string.IsNullOrEmpty(sw.SCXCBPCYEAR))
                sb.AppendFormat(" AND SCXCBPCYEAR = '{0}'", ClsSql.EncodeSql(sw.SCXCBPCYEAR));
            if (!string.IsNullOrEmpty(sw.SCXCBPCSEASONCODE))
                sb.AppendFormat(" AND SCXCBPCSEASONCODE = '{0}'", ClsSql.EncodeSql(sw.SCXCBPCSEASONCODE));
            if (!string.IsNullOrEmpty(sw.SCXCBPCTYPECODE))
                sb.AppendFormat(" AND SCXCBPCTYPECODE = '{0}'", ClsSql.EncodeSql(sw.SCXCBPCTYPECODE));
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
        public static bool isExists(PEST_REPORT_SCXCBPC_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_REPORT_SCXCBPC where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_REPORT_SCXCBPCID) == false)
                sb.AppendFormat(" and PEST_REPORT_SCXCBPCID='{0}'", ClsSql.EncodeSql(sw.PEST_REPORT_SCXCBPCID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.SCXCBPCYEAR) == false)
                sb.AppendFormat(" and SCXCBPCYEAR='{0}'", ClsSql.EncodeSql(sw.SCXCBPCYEAR));
            if (string.IsNullOrEmpty(sw.SCXCBPCSEASONCODE) == false)
                sb.AppendFormat(" and SCXCBPCSEASONCODE='{0}'", ClsSql.EncodeSql(sw.SCXCBPCSEASONCODE));
            if (string.IsNullOrEmpty(sw.SCXCBPCTYPECODE) == false)
                sb.AppendFormat(" and SCXCBPCTYPECODE='{0}'", ClsSql.EncodeSql(sw.SCXCBPCTYPECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
