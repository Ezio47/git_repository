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
    /// 灾损_损失分类_火灾扑救费用_工资伙食费(P3)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P3
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRELOST_LOSTTYPE_ATTACK_P3_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_ATTACK_P3(FIRELOST_FIREINFOID, P3NAME, P3CODE, LOSEMONEYCOUNT, P3MONEY, ATTACKNUMBERS, ATTACKDAYS, MARK)");
            sb.AppendFormat(" OUTPUT INSERTED.P3ID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P3NAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P3CODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P3MONEY));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ATTACKNUMBERS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ATTACKDAYS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(")");
            try
            {
                string sId = DataBaseClass.ReturnSqlField(sb.ToString());
                if (sId != "")
                    return new Message(true, "添加成功!", sId);
                else
                    return new Message(false, "添加失败!", "");
            }
            catch
            {
                return new Message(false, "添加失败!", "");
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(FIRELOST_LOSTTYPE_ATTACK_P3_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE FIRELOST_LOSTTYPE_ATTACK_P3 SET ");
            sb.AppendFormat(" P3NAME={0}", ClsSql.saveNullField(m.P3NAME));
            sb.AppendFormat(",P3CODE={0}", ClsSql.saveNullField(m.P3CODE));
            sb.AppendFormat(",LOSEMONEYCOUNT={0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",P3MONEY={0}", ClsSql.saveNullField(m.P3MONEY));
            sb.AppendFormat(",ATTACKNUMBERS={0}", ClsSql.saveNullField(m.ATTACKNUMBERS));
            sb.AppendFormat(",ATTACKDAYS={0}", ClsSql.saveNullField(m.ATTACKDAYS));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(" WHERE P3ID= '{0}'", ClsSql.EncodeSql(m.P3ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.P3ID);
            else
                return new Message(false, "修改失败!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(FIRELOST_LOSTTYPE_ATTACK_P3_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_ATTACK_P3");
            sb.AppendFormat(" where P3ID= '{0}'", ClsSql.EncodeSql(m.P3ID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_ATTACK_P3_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_ATTACK_P3 where 1=1");
            if (string.IsNullOrEmpty(sw.P3ID) == false)
                sb.AppendFormat(" and P3ID= '{0}'", ClsSql.EncodeSql(sw.P3ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_ATTACK_P3_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_ATTACK_P3 WHERE  1=1");
            if (string.IsNullOrEmpty(sw.P3ID) == false)
                sb.AppendFormat(" AND P3ID = '{0}'", ClsSql.EncodeSql(sw.P3ID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.P3NAME) == false)
                sb.AppendFormat(" AND P3NAME like '%{0}%'", ClsSql.EncodeSql(sw.P3NAME));
            if (string.IsNullOrEmpty(sw.P3CODE) == false)
                sb.AppendFormat(" AND P3CODE  = '{0}'", ClsSql.EncodeSql(sw.P3CODE));
            string sql = "SELECT P3ID, FIRELOST_FIREINFOID, P3NAME, P3CODE, LOSEMONEYCOUNT, P3MONEY, ATTACKNUMBERS, ATTACKDAYS, MARK "
                + sb.ToString() + " ORDER BY P3ID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
