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
    /// 森林防火办事机构人员统计
    /// </summary>
    public class FIRERECORD_REPORT9
    {
        #region 添加
        /// <summary>
        /// 森林防火办事机构人员统计
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRERECORD_REPORT9_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrREPORTCODE = m.REPORTCODE.Split(',');
            string[] arrSSXTYPELEVELCODE = m.SSXTYPELEVELCODE.Split(',');
            string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');            
            for (int i = 0; i < arrREPORTVALUE.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT  INTO  FIRERECORD_REPORT9(BYORGNO,REPORTYEAR,REPORTCODE,SSXTYPELEVELCODE,REPORTVALUE)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat(" {0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.REPORTYEAR));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTCODE[i]));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrSSXTYPELEVELCODE[i]));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTVALUE[i]));
                sb.AppendFormat(")");
                sqllist.Add(sb.ToString());
            }
            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j > 0)
            {
                return new Message(true, "添加成功！", "");
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
        public static DataTable getDT(FIRERECORD_REPORT9_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_REPORT9 WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.FIRERECORD_REPORT9ID))
                sb.AppendFormat(" AND FIRERECORD_REPORT9ID = '{0}'", sw.FIRERECORD_REPORT9ID);
            if (!string.IsNullOrEmpty(sw.BYORGNO))
                sb.AppendFormat(" AND BYORGNO = '{0}'", sw.BYORGNO);
            if (!string.IsNullOrEmpty(sw.REPORTYEAR))
                sb.AppendFormat(" AND REPORTYEAR = '{0}'", sw.REPORTYEAR);
            if (!string.IsNullOrEmpty(sw.REPORTCODE))
                sb.AppendFormat(" AND REPORTCODE = '{0}'", sw.REPORTCODE);
            if (!string.IsNullOrEmpty(sw.SSXTYPELEVELCODE))
                sb.AppendFormat(" AND SSXTYPELEVELCODE = '{0}'", sw.SSXTYPELEVELCODE);
            if (!string.IsNullOrEmpty(sw.REPORTVALUE))
                sb.AppendFormat(" AND REPORTVALUE = '{0}'", sw.REPORTVALUE);
            sb.AppendFormat(" ORDER BY FIRERECORD_REPORT9ID");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
       
    }
}
