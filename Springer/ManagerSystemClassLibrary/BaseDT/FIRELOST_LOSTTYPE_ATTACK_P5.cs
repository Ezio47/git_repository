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
    /// 灾损_损失分类_火灾扑救费用_组织管理费(P5)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P5
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRELOST_LOSTTYPE_ATTACK_P5_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_ATTACK_P5(FIRELOST_FIREINFOID, P5NAME, P5CODE, LOSEMONEYCOUNT, P5MONEY, ATTACKDAYS, ELSEMONEY, MARK)");
            sb.AppendFormat(" OUTPUT INSERTED.P5ID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P5NAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P5CODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P5MONEY));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ATTACKDAYS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ELSEMONEY));
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
        public static Message Mdy(FIRELOST_LOSTTYPE_ATTACK_P5_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE FIRELOST_LOSTTYPE_ATTACK_P5 SET ");
            sb.AppendFormat(" P5NAME={0}", ClsSql.saveNullField(m.P5NAME));
            sb.AppendFormat(",P5CODE={0}", ClsSql.saveNullField(m.P5CODE));
            sb.AppendFormat(",LOSEMONEYCOUNT={0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",P5MONEY={0}", ClsSql.saveNullField(m.P5MONEY));
            sb.AppendFormat(",ATTACKDAYS={0}", ClsSql.saveNullField(m.ATTACKDAYS));
            sb.AppendFormat(",ELSEMONEY={0}", ClsSql.saveNullField(m.ELSEMONEY));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(" WHERE P5ID= '{0}'", ClsSql.EncodeSql(m.P5ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.P5ID);
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
        public static Message Del(FIRELOST_LOSTTYPE_ATTACK_P5_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_ATTACK_P5");
            sb.AppendFormat(" where P5ID= '{0}'", ClsSql.EncodeSql(m.P5ID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_ATTACK_P5_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_ATTACK_P5 where 1=1");
            if (string.IsNullOrEmpty(sw.P5ID) == false)
                sb.AppendFormat(" and P5ID= '{0}'", ClsSql.EncodeSql(sw.P5ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_ATTACK_P5_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_ATTACK_P5 WHERE  1=1");
            if (string.IsNullOrEmpty(sw.P5ID) == false)
                sb.AppendFormat(" AND P5ID = '{0}'", ClsSql.EncodeSql(sw.P5ID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.P5NAME) == false)
                sb.AppendFormat(" AND P5NAME like '%{0}%'", ClsSql.EncodeSql(sw.P5NAME));
            if (string.IsNullOrEmpty(sw.P5CODE) == false)
                sb.AppendFormat(" AND P5CODE  = '{0}'", ClsSql.EncodeSql(sw.P5CODE));
            string sql = "SELECT P5ID, FIRELOST_FIREINFOID, P5NAME, P5CODE, LOSEMONEYCOUNT, P5MONEY, ATTACKDAYS, ELSEMONEY, MARK "
                + sb.ToString() + " ORDER BY P5ID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
