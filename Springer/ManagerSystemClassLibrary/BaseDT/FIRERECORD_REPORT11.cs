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
    /// 火情档案_森林防火基础设施统计年报表二
    /// </summary>
    public class FIRERECORD_REPORT11
    {
        #region 添加
        /// <summary>
        /// 火情档案_森林防火基础设施统计年报表二
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        //public static Message Add(FIRERECORD_REPORT11_Model m)
        //{

        //    List<string> sqllist = new List<string>();
        //    string[] arrREPORTCODE = m.REPORTCODE.Split(',');
        //    string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');
        //    for (int i = 0; i < arrREPORTVALUE.Length - 1; i++)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendFormat("INSERT  INTO  FIRERECORD_REPORT11(BYORGNO,REPORTYEAR,REPORTCODE,REPORTVALUE)");
        //        sb.AppendFormat("VALUES(");
        //        sb.AppendFormat("{0}", ClsSql.saveNullField(m.BYORGNO));
        //        sb.AppendFormat(",{0}", ClsSql.saveNullField(m.REPORTYEAR));
        //        sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTCODE[i]));
        //        sb.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTVALUE[i]));
        //        sb.AppendFormat(")");
        //        sqllist.Add(sb.ToString());
        //    }
        //    var j = DataBaseClass.ExecuteSqlTran(sqllist);
        //    if (j > 0)
        //    {
        //        return new Message(true, "保存成功！", "");
        //    }
        //    else
        //    {
        //        return new Message(false, "保存失败，事物回滚机制！", "");
        //    }
        //}


        public static Message Add(FIRERECORD_REPORT11_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrREPORTCODE = m.REPORTCODE.Split(',');
            string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');
            #region 保存

            for (int i = 0; i < arrREPORTVALUE.Length - 1; i++)
            {
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT  INTO  FIRERECORD_REPORT11(BYORGNO,REPORTYEAR,REPORTCODE,REPORTVALUE)");
                #region 更新
                if (isExists(new FIRERECORD_REPORT11_SW { BYORGNO = m.BYORGNO, REPORTYEAR = m.REPORTYEAR, REPORTCODE = arrREPORTCODE[i], }))
                {
                    StringBuilder sbUpdate = new StringBuilder();
                    sbUpdate.AppendFormat("UPDATE FIRERECORD_REPORT11 SET ");
                    sbUpdate.AppendFormat(" REPORTVALUE= '{0}'", ClsSql.EncodeSql(arrREPORTVALUE[i]));
                    sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                    sbUpdate.AppendFormat(" and REPORTYEAR= '{0}'", ClsSql.EncodeSql(m.REPORTYEAR));
                    sbUpdate.AppendFormat(" and REPORTCODE= '{0}'", ClsSql.EncodeSql(arrREPORTCODE[i]));
                    //sbUpdate.AppendFormat(" and REPORTVALUE= '{0}'", ClsSql.EncodeSql(arrREPORTVALUE[i]));
                    sqllist.Add(sbUpdate.ToString());
                }
                #endregion

                #region 添加
                else
                {
                    sbInsert.AppendFormat("VALUES(");
                    sbInsert.AppendFormat(" {0}", ClsSql.saveNullField(m.BYORGNO));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(m.REPORTYEAR));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTCODE[i]));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTVALUE[i]));
                    sbInsert.AppendFormat(")");
                    sqllist.Add(sbInsert.ToString());
                }
                #endregion
            }
            #endregion
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
        public static DataTable getDT(FIRERECORD_REPORT11_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_REPORT11 WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.FIRERECORD_REPORT11ID))
                sb.AppendFormat(" AND FIRERECORD_REPORT11ID = '{0}'", sw.FIRERECORD_REPORT11ID);
            if (!string.IsNullOrEmpty(sw.BYORGNO))
                sb.AppendFormat(" AND BYORGNO = '{0}'", sw.BYORGNO);
            if (!string.IsNullOrEmpty(sw.REPORTYEAR))
                sb.AppendFormat(" AND REPORTYEAR <= '{0}'", sw.REPORTYEAR);
            if (!string.IsNullOrEmpty(sw.REPORTCODE))
                sb.AppendFormat(" AND REPORTCODE = '{0}'", sw.REPORTCODE);
            if (!string.IsNullOrEmpty(sw.REPORTVALUE))
                sb.AppendFormat(" AND REPORTVALUE = '{0}'", sw.REPORTVALUE);
            sb.AppendFormat(" ORDER BY FIRERECORD_REPORT11ID");
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
        public static bool isExists(FIRERECORD_REPORT11_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRERECORD_REPORT11 where 1=1");
            if (string.IsNullOrEmpty(sw.FIRERECORD_REPORT11ID) == false)
                sb.AppendFormat(" and FIRERECORD_REPORT11ID='{0}'", ClsSql.EncodeSql(sw.FIRERECORD_REPORT11ID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.REPORTYEAR) == false)
                sb.AppendFormat(" and REPORTYEAR='{0}'", ClsSql.EncodeSql(sw.REPORTYEAR));
            if (string.IsNullOrEmpty(sw.REPORTCODE) == false)
                sb.AppendFormat(" and REPORTCODE='{0}'", ClsSql.EncodeSql(sw.REPORTCODE));

            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
