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
    /// 灾损_损失分类_停减产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_STOPREDUCTION
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRELOST_LOSTTYPE_STOPREDUCTION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_STOPREDUCTION( FIRELOST_FIREINFOID, STOPREDUCTIONNAME, STOPREDUCTIONCODE, LOSEMONEYCOUNT, STOPREDUCTIONCOUNT, STOPREDUCTIONTIME, STOPREDUCTIONPRICE, MARK)");
            sb.AppendFormat(" OUTPUT INSERTED.STOPREDUCTIONID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOPREDUCTIONNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOPREDUCTIONCODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOPREDUCTIONCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOPREDUCTIONTIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOPREDUCTIONPRICE));
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
        public static Message Mdy(FIRELOST_LOSTTYPE_STOPREDUCTION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE FIRELOST_LOSTTYPE_STOPREDUCTION SET ");
            sb.AppendFormat(" STOPREDUCTIONNAME={0}", ClsSql.saveNullField(m.STOPREDUCTIONNAME));
            sb.AppendFormat(",STOPREDUCTIONCODE={0}", ClsSql.saveNullField(m.STOPREDUCTIONCODE));
            sb.AppendFormat(",LOSEMONEYCOUNT={0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",STOPREDUCTIONCOUNT={0}", ClsSql.saveNullField(m.STOPREDUCTIONCOUNT));
            sb.AppendFormat(",STOPREDUCTIONTIME={0}", ClsSql.saveNullField(m.STOPREDUCTIONTIME));
            sb.AppendFormat(",STOPREDUCTIONPRICE={0}", ClsSql.saveNullField(m.STOPREDUCTIONPRICE));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(" WHERE STOPREDUCTIONID= '{0}'", ClsSql.EncodeSql(m.STOPREDUCTIONID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.STOPREDUCTIONID);
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
        public static Message Del(FIRELOST_LOSTTYPE_STOPREDUCTION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_STOPREDUCTION");
            sb.AppendFormat(" where STOPREDUCTIONID= '{0}'", ClsSql.EncodeSql(m.STOPREDUCTIONID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_STOPREDUCTION_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_STOPREDUCTION where 1=1");
            if (string.IsNullOrEmpty(sw.STOPREDUCTIONID) == false)
                sb.AppendFormat(" and STOPREDUCTIONID= '{0}'", ClsSql.EncodeSql(sw.STOPREDUCTIONID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_STOPREDUCTION_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_STOPREDUCTION WHERE  1=1");
            if (string.IsNullOrEmpty(sw.STOPREDUCTIONID) == false)
                sb.AppendFormat(" AND STOPREDUCTIONID = '{0}'", ClsSql.EncodeSql(sw.STOPREDUCTIONID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.STOPREDUCTIONNAME) == false)
                sb.AppendFormat(" AND STOPREDUCTIONNAME like '%{0}%'", ClsSql.EncodeSql(sw.STOPREDUCTIONNAME));
            if (string.IsNullOrEmpty(sw.STOPREDUCTIONCODE) == false)
                sb.AppendFormat(" AND STOPREDUCTIONCODE = '{0}'", ClsSql.EncodeSql(sw.STOPREDUCTIONCODE));
            string sql = "SELECT STOPREDUCTIONID, FIRELOST_FIREINFOID, STOPREDUCTIONNAME, STOPREDUCTIONCODE, LOSEMONEYCOUNT, STOPREDUCTIONCOUNT, STOPREDUCTIONTIME, STOPREDUCTIONPRICE, MARK "
                + sb.ToString() + " ORDER BY STOPREDUCTIONID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion      
    }
}
