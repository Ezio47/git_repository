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
    /// 灾损_火情基本信息表
    /// </summary>
    public class FIRELOST_FIREINFO
    {
        #region 增、删、改
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Init(FIRELOST_FIREINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO FIRELOST_FIREINFO(JCFID) OUTPUT INSERTED.FIRELOST_FIREINFOID VALUES({0})", ClsSql.saveNullField(m.JCFID));
            try
            {
                string sId = DataBaseClass.ReturnSqlField(sb.ToString());
                return new Message(true, "初始化成功!", sId);
            }
            catch
            {
                return new Message(false, "初始化失败!", "");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(FIRELOST_FIREINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            if (!isExists(new FIRELOST_FIREINFO_SW { FIRELOST_FIREINFOID = m.FIRELOST_FIREINFOID }))
            {
                sb.AppendFormat("INSERT INTO FIRELOST_FIREINFO(JCFID, TOTALAREA, TOTALPERSON, TOTALXJL, FIREAREA, FIRELOSEAREA, XJLLOSE, CASUALTYCOUNT, BUILDINGLOSECOUNT,MACHINERYLOSECOUNT,");
                sb.AppendFormat("TOTALAREAJWDLIST, FIREAREAJWDLIST, FIRELOSEAREAJWDLIST, LOSSCOUNT, FORESTRESOURCELOSSRATIO, AVGLOSSPERCATITAVALUE, WOODLANDLOSSAVGVALUE, FIRESUPPEFFECTTHAN)");
                sb.AppendFormat(" OUTPUT INSERTED.FIRELOST_FIREINFOID VALUES(");
                sb.AppendFormat(" {0}", ClsSql.saveNullField(m.JCFID));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.TOTALAREA));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.TOTALPERSON));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.TOTALXJL));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREAREA));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRELOSEAREA));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.XJLLOSE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CASUALTYCOUNT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUILDINGLOSECOUNT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MACHINERYLOSECOUNT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.TOTALAREAJWDLIST));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREAREAJWDLIST));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRELOSEAREAJWDLIST));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LOSSCOUNT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FORESTRESOURCELOSSRATIO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.AVGLOSSPERCATITAVALUE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WOODLANDLOSSAVGVALUE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRESUPPEFFECTTHAN));
                sb.AppendFormat(")");
                try
                {
                    string sId = DataBaseClass.ReturnSqlField(sb.ToString());
                    return new Message(true, "评估成功!", sId);
                }
                catch
                {
                    return new Message(false, "评估失败!", "");
                }
            }
            else
            {
                sb.AppendFormat("UPDATE FIRELOST_FIREINFO SET");
                sb.AppendFormat(" JCFID={0}", ClsSql.saveNullField(m.JCFID));
                sb.AppendFormat(",TOTALAREA={0}", ClsSql.saveNullField(m.TOTALAREA));
                sb.AppendFormat(",TOTALPERSON={0}", ClsSql.saveNullField(m.TOTALPERSON));
                sb.AppendFormat(",TOTALXJL={0}", ClsSql.saveNullField(m.TOTALXJL));
                sb.AppendFormat(",FIREAREA={0}", ClsSql.saveNullField(m.FIREAREA));
                sb.AppendFormat(",FIRELOSEAREA={0}", ClsSql.saveNullField(m.FIRELOSEAREA));
                sb.AppendFormat(",XJLLOSE={0}", ClsSql.saveNullField(m.XJLLOSE));
                sb.AppendFormat(",CASUALTYCOUNT={0}", ClsSql.saveNullField(m.CASUALTYCOUNT));
                sb.AppendFormat(",BUILDINGLOSECOUNT={0}", ClsSql.saveNullField(m.BUILDINGLOSECOUNT));
                sb.AppendFormat(",MACHINERYLOSECOUNT={0}", ClsSql.saveNullField(m.MACHINERYLOSECOUNT));
                sb.AppendFormat(",TOTALAREAJWDLIST={0}", ClsSql.saveNullField(m.TOTALAREAJWDLIST));
                sb.AppendFormat(",FIREAREAJWDLIST={0}", ClsSql.saveNullField(m.FIREAREAJWDLIST));
                sb.AppendFormat(",FIRELOSEAREAJWDLIST={0}", ClsSql.saveNullField(m.FIRELOSEAREAJWDLIST));
                sb.AppendFormat(",LOSSCOUNT={0}", ClsSql.saveNullField(m.LOSSCOUNT));
                sb.AppendFormat(",FORESTRESOURCELOSSRATIO={0}", ClsSql.saveNullField(m.FORESTRESOURCELOSSRATIO));
                sb.AppendFormat(",AVGLOSSPERCATITAVALUE={0}", ClsSql.saveNullField(m.AVGLOSSPERCATITAVALUE));
                sb.AppendFormat(",WOODLANDLOSSAVGVALUE={0}", ClsSql.saveNullField(m.WOODLANDLOSSAVGVALUE));
                sb.AppendFormat(",FIRESUPPEFFECTTHAN={0}", ClsSql.saveNullField(m.FIRESUPPEFFECTTHAN));
                sb.AppendFormat(" WHERE FIRELOST_FIREINFOID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_FIREINFOID));
                bool bln = DataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "评估成功!", m.FIRELOST_FIREINFOID);
                else
                    return new Message(false, "评估失败!", "");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(FIRELOST_FIREINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from FIRELOST_FIREINFO");
            sb.AppendFormat(" where FIRELOST_FIREINFOID= '{0}'", ClsSql.EncodeSql(m.FIRELOST_FIREINFOID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 判断数据是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(FIRELOST_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_FIREINFO  where 1=1");
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID) == false)
                sb.AppendFormat(" and FIRELOST_FIREINFOID='{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        /// <param name="sw">查询模型</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_FIREINFO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM FIRELOST_FIREINFO  WHERE 1=1");
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" AND JCFID = '{0}'", ClsSql.EncodeSql(sw.JCFID));
            string sql = "SELECT * " + sb.ToString() + " order by FIRELOST_FIREINFOID ";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "FIRELOST_FIREINFO");
            return ds.Tables[0];
        }

        /// <summary>
        ///  获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRELOST_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM FIRELOST_FIREINFO  WHERE 1=1");
            if (string.IsNullOrEmpty(sw.FIRELOST_FIREINFOID)==false)
                sb.AppendFormat(" AND FIRELOST_FIREINFOID = '{0}'", ClsSql.EncodeSql(sw.FIRELOST_FIREINFOID));
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" AND JCFID = '{0}'", ClsSql.EncodeSql(sw.JCFID));
            string sql = "SELECT * " + sb.ToString() + " order by FIRELOST_FIREINFOID ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 判断火情记录是否评估
        /// <summary>
        /// 判断火情记录是否评估
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isAssess(FIRELOST_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRELOST_FIREINFO where 1=1");
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" and JCFID='{0}'", ClsSql.EncodeSql(sw.JCFID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取最大灾损火情序号
        /// <summary>
        /// 获取最大灾损火情序号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxFIREINFOID()
        {
            string sql = "select max(FIRELOST_FIREINFOID) from FIRELOST_FIREINFO";
            string value = SDEDataBaseClass.ReturnSqlField(sql);
            if (value != "")
                return int.Parse(value);
            else
                return 0;
        }
        #endregion
    }
}
