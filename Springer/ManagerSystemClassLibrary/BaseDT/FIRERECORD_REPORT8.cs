﻿using System;
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
    /// 森林防火组织机构统计
    /// </summary>
    public class FIRERECORD_REPORT8
    {
        #region 添加
        /// <summary>
        /// 添加森林防火组织机构统计
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRERECORD_REPORT8_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrREPORTCODE = m.REPORTCODE.Split(',');
            string[] arrSSXTYPELEVELCODE = m.SSXTYPELEVELCODE.Split(',');
            string[] arrREPORTVALUE = m.REPORTVALUE.Split(',');
            for (int i = 0; i < arrREPORTVALUE.Length - 1; i++)
            {
                #region 更新
                if (isExists(new FIRERECORD_REPORT8_SW { BYORGNO = m.BYORGNO, REPORTYEAR = m.REPORTYEAR, REPORTCODE = arrREPORTCODE[i], SSXTYPELEVELCODE = arrSSXTYPELEVELCODE[i] }))
                {
                    StringBuilder sbUpdate = new StringBuilder();
                    sbUpdate.AppendFormat(" UPDATE FIRERECORD_REPORT8 SET ");
                    sbUpdate.AppendFormat(" REPORTVALUE= '{0}'", ClsSql.EncodeSql(arrREPORTVALUE[i]));
                    sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                    sbUpdate.AppendFormat(" and REPORTYEAR= '{0}'", ClsSql.EncodeSql(m.REPORTYEAR));
                    sbUpdate.AppendFormat(" and REPORTCODE= '{0}'", ClsSql.EncodeSql(arrREPORTCODE[i]));
                    sbUpdate.AppendFormat(" and SSXTYPELEVELCODE= '{0}'", ClsSql.EncodeSql(arrSSXTYPELEVELCODE[i]));
                    sqllist.Add(sbUpdate.ToString());
                }
                #endregion

                #region 添加
                else
                {
                    StringBuilder sbInsert = new StringBuilder();
                    sbInsert.AppendFormat(" INSERT  INTO  FIRERECORD_REPORT8(BYORGNO,REPORTYEAR,REPORTCODE,SSXTYPELEVELCODE,REPORTVALUE)");
                    sbInsert.AppendFormat(" VALUES(");
                    sbInsert.AppendFormat(" {0}", ClsSql.saveNullField(m.BYORGNO));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(m.REPORTYEAR));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTCODE[i]));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrSSXTYPELEVELCODE[i]));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrREPORTVALUE[i]));
                    sbInsert.AppendFormat(")");
                    sqllist.Add(sbInsert.ToString());
                }
                #endregion
            }
            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j >= 0)
            {
                return new Message(true, "保存成功!", "");
            }
            else
            {
                return new Message(false, "保存失败,事物回滚机制!", "");
            }
        }
        #endregion

        #region 获取报表数据列表
        /// <summary>
        /// 获取报表数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDT(FIRERECORD_REPORT8_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_REPORT8 WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.FIRERECORD_REPORT8ID))
                sb.AppendFormat(" AND FIRERECORD_REPORT8ID = '{0}'", sw.FIRERECORD_REPORT8ID);
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,9) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.REPORTYEAR))
                sb.AppendFormat(" AND REPORTYEAR = '{0}'", sw.REPORTYEAR);
            if (!string.IsNullOrEmpty(sw.REPORTCODE))
                sb.AppendFormat(" AND REPORTCODE = '{0}'", sw.REPORTCODE);
            if (!string.IsNullOrEmpty(sw.SSXTYPELEVELCODE))
                sb.AppendFormat(" AND SSXTYPELEVELCODE = '{0}'", sw.SSXTYPELEVELCODE);
            if (!string.IsNullOrEmpty(sw.REPORTVALUE))
                sb.AppendFormat(" AND REPORTVALUE = '{0}'", sw.REPORTVALUE);
            sb.AppendFormat(" ORDER BY BYORGNO, FIRERECORD_REPORT8ID");
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
        public static bool isExists(FIRERECORD_REPORT8_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRERECORD_REPORT8 where 1=1");
            if (string.IsNullOrEmpty(sw.FIRERECORD_REPORT8ID) == false)
                sb.AppendFormat(" and FIRERECORD_REPORT8ID='{0}'", ClsSql.EncodeSql(sw.FIRERECORD_REPORT8ID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.REPORTYEAR) == false)
                sb.AppendFormat(" and REPORTYEAR='{0}'", ClsSql.EncodeSql(sw.REPORTYEAR));
            if (string.IsNullOrEmpty(sw.REPORTCODE) == false)
                sb.AppendFormat(" and REPORTCODE='{0}'", ClsSql.EncodeSql(sw.REPORTCODE));
            if (string.IsNullOrEmpty(sw.SSXTYPELEVELCODE) == false)
                sb.AppendFormat(" and SSXTYPELEVELCODE='{0}'", ClsSql.EncodeSql(sw.SSXTYPELEVELCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

    }
}
