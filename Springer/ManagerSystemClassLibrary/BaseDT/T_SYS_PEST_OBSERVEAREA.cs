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
    /// 有害生物应施面积表
    /// </summary>
    public class T_SYS_PEST_OBSERVEAREA
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(T_SYS_PEST_OBSERVEAREA_Model m)
        {
            List<string> sqllist = new List<string>();

            string[] arrBYORGNO = m.BYORGNO.Split(',');
            string[] arrOBSERVEAREA = m.OBSERVEAREA.Split(',');

            #region 先删除
            StringBuilder sbDelete = new StringBuilder();
            sbDelete.AppendFormat(" Delete From T_SYS_PEST_OBSERVEAREA WHERE 1=1");
            sbDelete.AppendFormat(" AND OBSERVEYEAR='{0}'", m.OBSERVEYEAR);
            if (m.TopORGNO.Substring(4, 5) == "00000")
                sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,4)='{0}'", m.TopORGNO.Substring(0, 4));
            if (m.TopORGNO.Substring(6, 3) == "000" && m.TopORGNO.Substring(4, 5) != "00000")
                sbDelete.AppendFormat(" AND SUBSTRING(BYORGNO,1,6)='{0}'", m.TopORGNO.Substring(0, 6));
            if (m.TopORGNO.Substring(6, 3) != "000")
                sbDelete.AppendFormat(" AND BYORGNO='{0}'", m.TopORGNO);
            DataBaseClass.ExeSql(sbDelete.ToString());
            #endregion

            #region 再更新
            if (arrBYORGNO.Length - 1 > 0)
            {
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO T_SYS_PEST_OBSERVEAREA(BYORGNO,OBSERVEYEAR, OBSERVEAREA)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(arrOBSERVEAREA[i]))
                    {
                        #region 更新
                        if (isExists(new T_SYS_PEST_OBSERVEAREA_SW { BYORGNO = arrBYORGNO[i], OBSERVEYEAR = m.OBSERVEYEAR }))
                        {
                            StringBuilder sbUpdate = new StringBuilder();
                            sbUpdate.AppendFormat("UPDATE T_SYS_PEST_OBSERVEAREA SET ");
                            sbUpdate.AppendFormat(" OBSERVEAREA='{0}'", ClsSql.EncodeSql(arrOBSERVEAREA[i]));
                            sbUpdate.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                            sbUpdate.AppendFormat(" and OBSERVEYEAR= '{0}'", ClsSql.EncodeSql(m.OBSERVEYEAR));
                            sqllist.Add(sbUpdate.ToString());
                        }
                        #endregion

                        #region 添加
                        else
                        {
                            sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                            sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OBSERVEYEAR));
                            sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrOBSERVEAREA[i]));
                            sbInsert.AppendFormat(" UNION ALL ");
                        }
                        #endregion
                    }
                }
                string insertStr = sbInsert.ToString();
                if (insertStr.Contains(" UNION ALL "))
                {
                    insertStr = insertStr.Substring(0, insertStr.Length - 10);
                    sqllist.Add(insertStr);
                }
            }
            #endregion

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
        public static DataTable getDT(T_SYS_PEST_OBSERVEAREA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT T_SYS_PEST_OBSERVEAREAID,BYORGNO,OBSERVEAREA,OBSERVEYEAR FROM T_SYS_PEST_OBSERVEAREA WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.OBSERVEAREA))
                sb.AppendFormat(" AND OBSERVEAREA = '{0}'", ClsSql.EncodeSql(sw.OBSERVEAREA));
            if (!string.IsNullOrEmpty(sw.OBSERVEYEAR))
                sb.AppendFormat(" AND OBSERVEYEAR = '{0}'", ClsSql.EncodeSql(sw.OBSERVEYEAR));
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
        public static bool isExists(T_SYS_PEST_OBSERVEAREA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_PEST_OBSERVEAREA where 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.OBSERVEAREA) == false)
                sb.AppendFormat(" and OBSERVEAREA='{0}'", ClsSql.EncodeSql(sw.OBSERVEAREA));
            if (string.IsNullOrEmpty(sw.OBSERVEYEAR) == false)
                sb.AppendFormat(" and OBSERVEYEAR='{0}'", ClsSql.EncodeSql(sw.OBSERVEYEAR));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
