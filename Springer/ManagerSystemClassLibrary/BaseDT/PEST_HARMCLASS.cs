using DataBaseClassLibrary;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerSystemModel;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 有害生物_危害等级表
    /// </summary>
    public class PEST_HARMCLASS
    {
        #region 增、删、改
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_HARMCLASS_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrTOWNNAME = m.TOWNNAME.Split(',');
            string[] arrDCDATE = m.DCDATE.Split(',');
            string[] arrJD = m.JD.Split(',');
            string[] arrWD = m.WD.Split(',');
            string[] arrHARMCLASS = m.HARMCLASS.Split(',');
            if (arrBYORGNO.Length - 1 > 0)
            {
                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat(" INSERT INTO PEST_HARMCLASS(DCDATE, BYORGNO, TOWNNAME, JD, WD, HARMCLASS) ");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new PEST_HARMCLASS_SW { BYORGNO = arrBYORGNO[i], DCDATE = arrDCDATE[i] }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat(" UPDATE PEST_HARMCLASS SET ");
                        sbUpdate.AppendFormat(" JD={0}", ClsSql.saveNullField(arrJD[i]));
                        sbUpdate.AppendFormat(",WD={0}", ClsSql.saveNullField(arrWD[i]));
                        sbUpdate.AppendFormat(",HARMCLASS={0}", ClsSql.saveNullField(arrHARMCLASS[i]));
                        sbUpdate.AppendFormat(" WHERE BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbUpdate.AppendFormat(" AND  DCDATE= '{0}'", ClsSql.EncodeSql(arrDCDATE[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" SELECT '{0}'", ClsSql.EncodeSql(arrDCDATE[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrTOWNNAME[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrJD[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrWD[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrHARMCLASS[i]));
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
                return new Message(false, "保存失败,事物回滚!", "");
        }

        /// <summary>
        /// 危害等级导入
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message AddImport(PEST_HARMCLASS_Model m)
        {
            Message ms = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO  PEST_HARMCLASS(DCDATE, BYORGNO, TOWNNAME, JD, WD, HARMCLASS)");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.DCDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOWNNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HARMCLASS));
            sb.AppendFormat(")");
            List<string> sqllist = new List<string>();
            sqllist.Add(sb.ToString());
            var i = DataBaseClass.ExecuteSqlTran(sqllist);
            if (i > 0)
            {
                ms = new Message(true, "导入成功!", "");
            }
            else
            {
                ms = new Message(false, "导入失败,事物回滚机制!", "");
            }
            return ms;
        }

        /// <summary>
        /// 根据时间删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message DeleteByDCDATE(PEST_HARMCLASS_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" DELETE  FROM  PEST_HARMCLASS  WHERE DCDATE='" + m.DCDATE + "'");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }

        /// <summary>
        /// 更新三维库危害等级
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message UpdateAceHarmClass(PEST_HARMCLASS_Model m)
        {
            List<string> sqllist = new List<string>();
            var arrBYORGNO = m.BYORGNO.Split(',');
            var arrHARMCLASS = m.HARMCLASS.Split(',');
            var arrDCDATE = m.DCDATE.Split(',');
            for (int i = 0; i < arrBYORGNO.Length - 1; i++)
            {
                if (ClsSql.EncodeSql(arrHARMCLASS[i]) != "")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(" UPDATE PESTHARMCLASS SET ");
                    sb.AppendFormat(" HARMCLASS='{0}'", ClsSql.EncodeSql(arrHARMCLASS[i]));
                    sb.AppendFormat(",DCDATE={0}", ClsSql.saveNullField(arrDCDATE[i]));
                    sb.AppendFormat(" WHERE BYORGNO='{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                    sqllist.Add(sb.ToString());
                }
            }
            var j = SDEDataBaseClass.ExecuteSqlTran(sqllist);
            if (j >= 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败,事物回滚!", "");
        }

        /// <summary>
        /// 更新三维库危害等级
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message UpdateAceHarmClass_XZ(PEST_HARMCLASS_Model m)
        {
            Message ms = null;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(" Update PESTHARMCLASS_XZ Set DValue={0}", ClsSql.EncodeSql(m.DValue));
            sb1.AppendFormat(" Where NAME='{0}'", ClsSql.EncodeSql(m.Name));
            List<string> sqllist = new List<string>();
            sqllist.Add(sb1.ToString());
            var i = SDEDataBaseClass.ExecuteSqlTran(sqllist);
            if (i > 0)
            {
                ms = new Message(true, "更新成功!", "");
            }
            else
            {
                ms = new Message(false, "更新失败,事物回滚机制!", "");
            }
            return ms;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_HARMCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_HARMCLASS WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.DCDATE) == false)
                sb.AppendFormat(" AND DCDATE = '{0}'", ClsSql.EncodeSql(sw.DCDATE));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (!string.IsNullOrEmpty(sw.TOWNNAME))
                sb.AppendFormat(" AND TOWNNAME = '{0}'", ClsSql.EncodeSql(sw.TOWNNAME));
            string sql = " SELECT PEST_HARMCLASSID, DCDATE, BYORGNO, TOWNNAME, JD, WD, HARMCLASS " + sb.ToString() + " ORDER BY PEST_HARMCLASSID,BYORGNO ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取最新数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getTopDT(PEST_HARMCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_HARMCLASS WHERE 1=1 ");
            if (string.IsNullOrEmpty(sw.DCDATE) == false)
                sb.AppendFormat(" AND DCDATE = '{0}'", ClsSql.EncodeSql(sw.DCDATE));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (!string.IsNullOrEmpty(sw.TOWNNAME))
                sb.AppendFormat(" AND TOWNNAME = '{0}'", ClsSql.EncodeSql(sw.TOWNNAME));
            sb.AppendFormat(" AND DCDATE = (SELECT MAX( DCDATE) FROM PEST_HARMCLASS)");
            string sql = " SELECT PEST_HARMCLASSID, DCDATE, BYORGNO, TOWNNAME, JD, WD, HARMCLASS " + sb.ToString() + " ORDER BY BYORGNO";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_HARMCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_HARMCLASS where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_HARMCLASSID) == false)
                sb.AppendFormat(" and PEST_HARMCLASSID='{0}'", ClsSql.EncodeSql(sw.PEST_HARMCLASSID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.TOWNNAME) == false)
                sb.AppendFormat(" and TOWNNAME='{0}'", ClsSql.EncodeSql(sw.TOWNNAME));
            if (string.IsNullOrEmpty(sw.DCDATE) == false)
                sb.AppendFormat(" and DCDATE='{0}'", ClsSql.EncodeSql(sw.DCDATE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 根据机构编码和日期获取序号
        /// <summary>
        /// 根据机构编码和日期获取序号
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">机构编码</param>
        /// <param name="dcDate">日期</param>
        /// <returns>名称</returns>
        public static string getID(DataTable dt, string orgNo, string dcDate)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(orgNo))
                return "";
            if (string.IsNullOrEmpty(dcDate))
                return "";
            string str = "";
            DataRow[] dr = dt.Select(" BYORGNO='" + orgNo + "' AND  DCDATE='" + dcDate + "'");
            if (dr.Length > 0)
                str = dr[0]["PEST_HARMCLASSID"].ToString();
            return str;
        }
        #endregion
    }
}
