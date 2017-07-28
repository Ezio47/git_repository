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
        public static Message Add(FIRERECORD_REPORT11_Model m)
        {
           
            List<string> sqllist = new List<string>();
            string[] arrREPORTCODE = m.REPORTCODE.Split(',');
            string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');
            for (int i = 0; i < arrREPORTVALUE.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT  INTO  FIRERECORD_REPORT11(BYORGNO,REPORTYEAR,REPORTCODE,REPORTVALUE)");
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
        //    bool bln = DataBaseClass.ExeSql(sb.ToString());
        //    #endregion;
        //    if (bln == true)
        //        return new Message(true, "添加成功!", m.returnUrl);
        //    else
        //        return new Message(false, "添加失败，请检查各输入框是否正确!", m.returnUrl);
        //}

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

        //#region 判断记录是否存在
        ///// <summary>
        ///// 判断记录是否存在
        ///// </summary>
        ///// <param name="sw">参见模型</param>
        ///// <returns>true存在 false不存在 </returns>
        //public static bool isExists(FIRERECORD_FIREINF11_SW sw)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("select 1 from FIRERECORD_FIREINFO11 where 1=1");
        //    if (string.IsNullOrEmpty(sw.FIRERECORD_REPORT11ID) == false)
        //        sb.AppendFormat(" and FIRERECORD_REPORT11ID='{0}'", ClsSql.EncodeSql(sw.FIRERECORD_REPORT11ID));
        //    return DataBaseClass.JudgeRecordExists(sb.ToString());
        //}
        //#endregion


    }
}
