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
    /// 火情档案_森林防火建设资金统计年报表
    /// </summary>
    public class FIRERECORD_REPORT12
    {
        #region 添加
        /// <summary>
        /// 火情档案_森林防火建设资金统计年报表
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRERECORD_REPORT12_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrREPORTCODE = m.REPORTCODE.Split(',');
            string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');
            for (int i = 0; i < arrREPORTVALUE.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT  INTO  FIRERECORD_REPORT12(BYORGNO,REPORTYEAR,REPORTCODE,REPORTVALUE)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.REPORTYEAR));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTCODE[i]));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTVALUE[i]));
                sb.AppendFormat(")");
                sqllist.Add(sb.ToString());
            }
            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j > 0)
            {
                return new Message(true, "保存成功！", "");
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
        public static DataTable getDT(FIRERECORD_REPORT12_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_REPORT12 WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.FIRERECORD_REPORT12ID))
                sb.AppendFormat(" AND FIRERECORD_REPORT12ID = '{0}'", sw.FIRERECORD_REPORT12ID);
            if (!string.IsNullOrEmpty(sw.BYORGNO))
                sb.AppendFormat(" AND BYORGNO = '{0}'", sw.BYORGNO);
            if (!string.IsNullOrEmpty(sw.REPORTYEAR))
                sb.AppendFormat(" AND REPORTYEAR <= '{0}'", sw.REPORTYEAR);
            if (!string.IsNullOrEmpty(sw.REPORTCODE))
                sb.AppendFormat(" AND REPORTCODE = '{0}'", sw.REPORTCODE);
            if (!string.IsNullOrEmpty(sw.REPORTVALUE))
                sb.AppendFormat(" AND REPORTVALUE = '{0}'", sw.REPORTVALUE);
            sb.AppendFormat(" ORDER BY FIRERECORD_REPORT12ID");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion


    }
}
