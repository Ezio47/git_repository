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
    /// 有害生物与树种对应表
    /// </summary>
    public class T_SYS_TREESPECIES_PEST
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns></returns>
        public static Message Save(T_SYS_TREESPECIES_PEST_Model m)
        {
            string[] arrTSPCODE = m.TSPCODE.Split(',');
            List<string> sqllist = new List<string>();

            #region 先删除
            string sql = "delete from T_SYS_TREESPECIES_PEST where PESTCODE='" + m.PESTCODE + "'";
            DataBaseClass.ExeSql(sql);
            #endregion

            #region 再更新
            if (arrTSPCODE.Length > 0)
            {
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO T_SYS_TREESPECIES_PEST(PESTCODE,TSPCODE)");
                for (int i = 0; i < arrTSPCODE.Length; i++)
                {
                    sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.PESTCODE));
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrTSPCODE[i]));
                    if (i != arrTSPCODE.Length - 1)
                        sbInsert.AppendFormat(" UNION ALL ");
                }
                sqllist.Add(sbInsert.ToString());
            }
            #endregion

            #region 再更新本地有害生物状态
            StringBuilder sbUpdate = new StringBuilder();
            sbUpdate.AppendFormat("UPDATE T_SYS_PEST SET ISLOCAL='1' where PESTCODE= '{0}'", ClsSql.EncodeSql(m.PESTCODE));
            sqllist.Add(sbUpdate.ToString());
            #endregion

            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y >= 0)
                return new Message(true, "关联成功!", "");
            else
                return new Message(false, "关联失败，事物回滚机制!", "");
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_TREESPECIES_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT PESTCODE, TSPCODE FROM T_SYS_TREESPECIES_PEST WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.PESTCODE))
                sb.AppendFormat(" AND PESTCODE = '{0}'", ClsSql.EncodeSql(sw.PESTCODE));
            if (!string.IsNullOrEmpty(sw.TSPCODE))
                sb.AppendFormat(" AND TSPCODE = '{0}'", ClsSql.EncodeSql(sw.TSPCODE));
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
        public static bool isExists(T_SYS_TREESPECIES_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_TREESPECIES_PEST where 1=1");
            if (string.IsNullOrEmpty(sw.PESTCODE) == false)
                sb.AppendFormat(" and PESTCODE='{0}'", ClsSql.EncodeSql(sw.PESTCODE));
            if (string.IsNullOrEmpty(sw.TSPCODE) == false)
                sb.AppendFormat(" and TSPCODE='{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
