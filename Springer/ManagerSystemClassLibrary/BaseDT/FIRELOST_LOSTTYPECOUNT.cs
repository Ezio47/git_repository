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
    /// 灾损_损失分类统计表
    /// </summary>
    public class FIRELOST_LOSTTYPECOUNT
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(FIRELOST_LOSTTYPECOUNT_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrCode = m.FIRELOSETYPECODE.Split(',');
            string[] arrMonery = m.LOSEMONEY.Split(',');
            string[] arrMark = m.MARK.Split(',');
            StringBuilder sbInsert = new StringBuilder();
            sbInsert.AppendFormat("Insert into FIRELOST_LOSTTYPECOUNT(FIRELOST_FIREINFOID, FIRELOSETYPECODE, LOSEMONEY, MARK)");
            for (int i = 0; i < arrCode.Length; i++)
            {
                #region 更新
                if (isExists(new FIRELOST_LOSTTYPECOUNT_SW { FIRELOST_FIREINFOID = m.FIRELOST_FIREINFOID, FIRELOSETYPECODE = arrCode[i] }))
                {
                    StringBuilder sbUpdate = new StringBuilder();
                    sbUpdate.AppendFormat("UPDATE FIRELOST_LOSTTYPECOUNT SET ");
                    sbUpdate.AppendFormat(" LOSEMONEY={0}", ClsSql.saveNullField(arrMonery[i]));
                    sbUpdate.AppendFormat(",MARK={0}", ClsSql.saveNullField(arrMark[i]));
                    sbUpdate.AppendFormat(" where FIRELOST_FIREINFOID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_FIREINFOID));
                    sbUpdate.AppendFormat(" and FIRELOSETYPECODE= '{0}'", ClsSql.EncodeSql(arrCode[i]));
                    sqllist.Add(sbUpdate.ToString());
                }
                #endregion

                #region 添加
                else
                {
                    sbInsert.AppendFormat(" select {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrCode[i])); ;
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrMonery[i]));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrMark[i]));
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
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "损失分类数据保存成功!", "");
            else
                return new Message(false, "损失分类数据保存失败,事物回滚机制!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(FIRELOST_LOSTTYPECOUNT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPECOUNT");
            sb.AppendFormat(" where FIRELOST_LOSTTYPECOUNTID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_LOSTTYPECOUNTID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(FIRELOST_LOSTTYPECOUNT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPECOUNT where 1=1");
            if (string.IsNullOrEmpty(sw.FIRELOST_LOSTTYPECOUNTID) == false)
                sb.AppendFormat(" and FIRELOST_LOSTTYPECOUNTID= '{0}'", ClsSql.EncodeSql(sw.FIRELOST_LOSTTYPECOUNTID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" and FIRELOST_FIREINFOID= '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.FIRELOSETYPECODE) == false)
                sb.AppendFormat(" and FIRELOSETYPECODE= '{0}'", ClsSql.EncodeSql(sw.FIRELOSETYPECODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPECOUNT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPECOUNT WHERE  1=1");
            if (string.IsNullOrEmpty(sw.FIRELOST_LOSTTYPECOUNTID) == false)
                sb.AppendFormat(" AND FIRELOST_LOSTTYPECOUNTID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_LOSTTYPECOUNTID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.FIRELOSETYPECODE) == false)
                sb.AppendFormat(" AND FIRELOSETYPECODE = '{0}'", ClsSql.EncodeSql(sw.FIRELOSETYPECODE));
            string sql = "SELECT FIRELOST_LOSTTYPECOUNTID, FIRELOST_FIREINFOID, FIRELOSETYPECODE, LOSEMONEY, MARK "
                + sb.ToString() + " ORDER BY FIRELOST_LOSTTYPECOUNTID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
