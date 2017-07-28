﻿using DataBaseClassLibrary;
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
    /// 灾损_损失分类_火灾扑救费用_消防器材消耗表(P4)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P4
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRELOST_LOSTTYPE_ATTACK_P4_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_ATTACK_P4(FIRELOST_FIREINFOID, P4NAME, P4CODE, LOSEMONEYCOUNT, NOWPRICE, P4COUNT, P4UNIT, YEARAVGDEPRECIATIONRATE, HAVEUSEYEAR, MARK)");
            sb.AppendFormat(" OUTPUT INSERTED.P4ID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P4NAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P4CODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NOWPRICE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P4COUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.P4UNIT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.YEARAVGDEPRECIATIONRATE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.HAVEUSEYEAR));
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
        public static Message Mdy(FIRELOST_LOSTTYPE_ATTACK_P4_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE FIRELOST_LOSTTYPE_ATTACK_P4 SET ");
            sb.AppendFormat(" P4NAME={0}", ClsSql.saveNullField(m.P4NAME));
            sb.AppendFormat(",P4CODE={0}", ClsSql.saveNullField(m.P4CODE));
            sb.AppendFormat(",LOSEMONEYCOUNT={0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",NOWPRICE={0}", ClsSql.saveNullField(m.NOWPRICE));
            sb.AppendFormat(",P4COUNT={0}", ClsSql.saveNullField(m.P4COUNT));
            sb.AppendFormat(",P4UNIT={0}", ClsSql.saveNullField(m.P4UNIT));
            sb.AppendFormat(",YEARAVGDEPRECIATIONRATE={0}", ClsSql.saveNullField(m.YEARAVGDEPRECIATIONRATE));
            sb.AppendFormat(",HAVEUSEYEAR={0}", ClsSql.saveNullField(m.HAVEUSEYEAR));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(" WHERE P4ID= '{0}'", ClsSql.EncodeSql(m.P4ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.P4ID);
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
        public static Message Del(FIRELOST_LOSTTYPE_ATTACK_P4_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_ATTACK_P4");
            sb.AppendFormat(" where P4ID= '{0}'", ClsSql.EncodeSql(m.P4ID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_ATTACK_P4_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_ATTACK_P4 where 1=1");
            if (string.IsNullOrEmpty(sw.P4ID) == false)
                sb.AppendFormat(" and P4ID= '{0}'", ClsSql.EncodeSql(sw.P4ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_ATTACK_P4_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_ATTACK_P4 WHERE  1=1");
            if (string.IsNullOrEmpty(sw.P4ID) == false)
                sb.AppendFormat(" AND P4ID = '{0}'", ClsSql.EncodeSql(sw.P4ID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.P4NAME) == false)
                sb.AppendFormat(" AND P4NAME like '%{0}%'", ClsSql.EncodeSql(sw.P4NAME));
            if (string.IsNullOrEmpty(sw.P4CODE) == false)
                sb.AppendFormat(" AND P4CODE  ='{0}'", ClsSql.EncodeSql(sw.P4CODE));
            string sql = "SELECT P4ID, FIRELOST_FIREINFOID, P4NAME, P4CODE, LOSEMONEYCOUNT, NOWPRICE, P4COUNT, P4UNIT, YEARAVGDEPRECIATIONRATE, HAVEUSEYEAR, MARK "
                + sb.ToString() + " ORDER BY P4ID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
