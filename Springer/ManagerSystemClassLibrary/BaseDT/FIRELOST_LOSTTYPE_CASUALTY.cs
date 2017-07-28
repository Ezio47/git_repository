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
    /// 灾损_损失分类_人员伤亡损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_CASUALTY
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRELOST_LOSTTYPE_CASUALTY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_CASUALTY(FIRELOST_FIREINFOID, CASUALTYCODE, CASUALTYNAME, CASUALTYNUMBERS, LOSEMONEYCOUNT, MARK)");
            sb.AppendFormat(" OUTPUT INSERTED.FIRELOST_LOSTTYPE_CASUALTYID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CASUALTYCODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CASUALTYNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CASUALTYNUMBERS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
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
        public static Message Mdy(FIRELOST_LOSTTYPE_CASUALTY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE FIRELOST_LOSTTYPE_CASUALTY SET ");
            sb.AppendFormat(" CASUALTYCODE={0}", ClsSql.saveNullField(m.CASUALTYCODE));
            sb.AppendFormat(",CASUALTYNAME={0}", ClsSql.saveNullField(m.CASUALTYNAME));
            sb.AppendFormat(",CASUALTYNUMBERS={0}", ClsSql.saveNullField(m.CASUALTYNUMBERS));
            sb.AppendFormat(",LOSEMONEYCOUNT={0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(" WHERE FIRELOST_LOSTTYPE_CASUALTYID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_LOSTTYPE_CASUALTYID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.FIRELOST_LOSTTYPE_CASUALTYID);
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
        public static Message Del(FIRELOST_LOSTTYPE_CASUALTY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_CASUALTY");
            sb.AppendFormat(" where FIRELOST_LOSTTYPE_CASUALTYID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_LOSTTYPE_CASUALTYID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_CASUALTY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_CASUALTY where 1=1");
            if (string.IsNullOrEmpty(sw.FIRELOST_LOSTTYPE_CASUALTYID) == false)
                sb.AppendFormat(" and FIRELOST_LOSTTYPE_CASUALTYID= '{0}'", ClsSql.EncodeSql(sw.FIRELOST_LOSTTYPE_CASUALTYID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_CASUALTY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_CASUALTY WHERE  1=1");
            if (string.IsNullOrEmpty(sw.FIRELOST_LOSTTYPE_CASUALTYID) == false)
                sb.AppendFormat(" AND FIRELOST_LOSTTYPE_CASUALTYID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_LOSTTYPE_CASUALTYID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.CASUALTYCODE) == false)
                sb.AppendFormat(" AND CASUALTYCODE = '{0}'", ClsSql.EncodeSql(sw.CASUALTYCODE));
            if (string.IsNullOrEmpty(sw.CASUALTYNAME) == false)
                sb.AppendFormat(" AND CASUALTYNAME like '%{0}%'", ClsSql.EncodeSql(sw.CASUALTYNAME));
            string sql = "SELECT FIRELOST_LOSTTYPE_CASUALTYID, FIRELOST_FIREINFOID, CASUALTYCODE, CASUALTYNAME, CASUALTYNUMBERS, LOSEMONEYCOUNT, MARK "
                + sb.ToString() + " ORDER BY FIRELOST_LOSTTYPE_CASUALTYID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
