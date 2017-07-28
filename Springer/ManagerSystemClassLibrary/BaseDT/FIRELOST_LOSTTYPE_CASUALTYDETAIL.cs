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
    /// 灾损_损失分类_人员伤亡损失明细表
    /// </summary>
    public class FIRELOST_LOSTTYPE_CASUALTYDETAIL
    {
        #region 保存
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrCODE = m.CASUALTYDETAILCODE.Split(',');
            string[] arrMONEY = m.CASUALTYDETAIMONEY.Split(',');
            StringBuilder sbInsert = new StringBuilder();
            sbInsert.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_CASUALTYDETAIL(FIRELOST_LOSTTYPE_CASUALTYID, CASUALTYDETAILCODE, CASUALTYDETAIMONEY)");
            for (int i = 0; i < arrCODE.Length; i++)
            {
                #region 更新
                if (isExists(new FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW { FIRELOST_LOSTTYPE_CASUALTYID = m.FIRELOST_LOSTTYPE_CASUALTYID, CASUALTYDETAILCODE = arrCODE[i] }))
                {
                    StringBuilder sbUpdate = new StringBuilder();
                    sbUpdate.AppendFormat("UPDATE FIRELOST_LOSTTYPE_CASUALTYDETAIL SET ");
                    sbUpdate.AppendFormat(" CASUALTYDETAIMONEY={0}", ClsSql.saveNullField(arrMONEY[i]));
                    sbUpdate.AppendFormat(" where FIRELOST_LOSTTYPE_CASUALTYID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_LOSTTYPE_CASUALTYID));
                    sbUpdate.AppendFormat(" and CASUALTYDETAILCODE= '{0}'", ClsSql.EncodeSql(arrCODE[i]));
                    sqllist.Add(sbUpdate.ToString());
                }
                #endregion

                #region 添加
                else
                {
                    sbInsert.AppendFormat(" select {0}", ClsSql.saveNullField(m.FIRELOST_LOSTTYPE_CASUALTYID));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrCODE[i])); ;
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrMONEY[i]));
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
            if (y >= 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，事物回滚机制!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_CASUALTYDETAIL where 1=1");
            if (!string.IsNullOrEmpty(m.CASUALTYDETAILID))
                sb.AppendFormat(" and CASUALTYDETAILID= '{0}'", ClsSql.EncodeSql(m.CASUALTYDETAILID));
            if (!string.IsNullOrEmpty(m.FIRELOST_LOSTTYPE_CASUALTYID))
                sb.AppendFormat(" and FIRELOST_LOSTTYPE_CASUALTYID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_LOSTTYPE_CASUALTYID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_CASUALTYDETAIL where 1=1");
            if (string.IsNullOrEmpty(sw.CASUALTYDETAILID) == false)
                sb.AppendFormat(" and CASUALTYDETAILID= '{0}'", ClsSql.EncodeSql(sw.CASUALTYDETAILID));
            if (string.IsNullOrEmpty(sw.FIRELOST_LOSTTYPE_CASUALTYID) == false)
                sb.AppendFormat(" and FIRELOST_LOSTTYPE_CASUALTYID= '{0}'", ClsSql.EncodeSql(sw.FIRELOST_LOSTTYPE_CASUALTYID));
            if (string.IsNullOrEmpty(sw.CASUALTYDETAILCODE) == false)
                sb.AppendFormat(" and CASUALTYDETAILCODE= '{0}'", ClsSql.EncodeSql(sw.CASUALTYDETAILCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_CASUALTYDETAIL WHERE  1=1");
            if (string.IsNullOrEmpty(sw.CASUALTYDETAILID) == false)
                sb.AppendFormat(" AND CASUALTYDETAILID = '{0}'", ClsSql.EncodeSql(sw.CASUALTYDETAILID));
            if (string.IsNullOrEmpty(sw.FIRELOST_LOSTTYPE_CASUALTYID) == false)
                sb.AppendFormat(" AND FIRELOST_LOSTTYPE_CASUALTYID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_LOSTTYPE_CASUALTYID));
            if (string.IsNullOrEmpty(sw.CASUALTYDETAILCODE) == false)
                sb.AppendFormat(" AND CASUALTYDETAILCODE = '{0}'", ClsSql.EncodeSql(sw.CASUALTYDETAILCODE));
            string sql = "SELECT CASUALTYDETAILID, FIRELOST_LOSTTYPE_CASUALTYID, CASUALTYDETAILCODE, CASUALTYDETAIMONEY "
                + sb.ToString() + " ORDER BY CASUALTYDETAILID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
