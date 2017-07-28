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
    /// 灾损_损失分类_居民财产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_RESIDENTPROPERTY
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRELOST_LOSTTYPE_RESIDENTPROPERTY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO FIRELOST_LOSTTYPE_RESIDENTPROPERTY(FIRELOST_FIREINFOID, RESIDENTPROPERTYNAME, LOSEMONEYCOUNT, RESIDENTPROPERTYCOUNT, RESIDENTPROPERTYUNIT, RESIDENTPROPERTYPRICE, YEARAVGDEPRECIATIONRATE, HAVEUSEYEAR, MARK)");
            sb.AppendFormat(" OUTPUT INSERTED.RESIDENTPROPERTYID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIRELOST_FIREINFOID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.RESIDENTPROPERTYNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.RESIDENTPROPERTYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.RESIDENTPROPERTYUNIT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.RESIDENTPROPERTYPRICE));
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
        public static Message Mdy(FIRELOST_LOSTTYPE_RESIDENTPROPERTY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE FIRELOST_LOSTTYPE_RESIDENTPROPERTY SET ");
            sb.AppendFormat(" RESIDENTPROPERTYNAME={0}", ClsSql.saveNullField(m.RESIDENTPROPERTYNAME));
            sb.AppendFormat(",LOSEMONEYCOUNT={0}", ClsSql.saveNullField(m.LOSEMONEYCOUNT));
            sb.AppendFormat(",RESIDENTPROPERTYCOUNT={0}", ClsSql.saveNullField(m.RESIDENTPROPERTYCOUNT));
            sb.AppendFormat(",RESIDENTPROPERTYUNIT={0}", ClsSql.saveNullField(m.RESIDENTPROPERTYUNIT));
            sb.AppendFormat(",RESIDENTPROPERTYPRICE={0}", ClsSql.saveNullField(m.RESIDENTPROPERTYPRICE));
            sb.AppendFormat(",YEARAVGDEPRECIATIONRATE={0}", ClsSql.saveNullField(m.YEARAVGDEPRECIATIONRATE));
            sb.AppendFormat(",HAVEUSEYEAR={0}", ClsSql.saveNullField(m.HAVEUSEYEAR));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(" WHERE RESIDENTPROPERTYID= '{0}'", ClsSql.EncodeSql(m.RESIDENTPROPERTYID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.RESIDENTPROPERTYID);
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
        public static Message Del(FIRELOST_LOSTTYPE_RESIDENTPROPERTY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete FIRELOST_LOSTTYPE_RESIDENTPROPERTY");
            sb.AppendFormat(" where RESIDENTPROPERTYID= '{0}'", ClsSql.EncodeSql(m.RESIDENTPROPERTYID));
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
        public static bool isExists(FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_LOSTTYPE_RESIDENTPROPERTY where 1=1");
            if (string.IsNullOrEmpty(sw.RESIDENTPROPERTYID) == false)
                sb.AppendFormat(" and RESIDENTPROPERTYID= '{0}'", ClsSql.EncodeSql(sw.RESIDENTPROPERTYID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRELOST_LOSTTYPE_RESIDENTPROPERTY WHERE  1=1");
            if (string.IsNullOrEmpty(sw.RESIDENTPROPERTYID) == false)
                sb.AppendFormat(" AND RESIDENTPROPERTYID = '{0}'", ClsSql.EncodeSql(sw.RESIDENTPROPERTYID));
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.RESIDENTPROPERTYNAME) == false)
                sb.AppendFormat(" AND RESIDENTPROPERTYNAME like '%{0}%'", ClsSql.EncodeSql(sw.RESIDENTPROPERTYNAME));
            string sql = "SELECT RESIDENTPROPERTYID, FIRELOST_FIREINFOID, RESIDENTPROPERTYNAME, LOSEMONEYCOUNT, RESIDENTPROPERTYCOUNT, RESIDENTPROPERTYUNIT, RESIDENTPROPERTYPRICE, YEARAVGDEPRECIATIONRATE, HAVEUSEYEAR, MARK "
                + sb.ToString() + " ORDER BY RESIDENTPROPERTYID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion      
    }
}
