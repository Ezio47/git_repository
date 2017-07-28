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
    /// 树种列表
    /// </summary>
    public class T_SYS_TREESPECIES
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_TREESPECIES_Model m)
        {
            if (isExists(new T_SYS_TREESPECIES_SW { TSPCODE = m.TSPCODE, }) == true)
                return new Message(false, "添加失败，该树种编码已存在!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_TREESPECIES(TSPCODE, TSPNAME, LATINNAME, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.TSPCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TSPNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LATINNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", m.returnUrl);
            else
                return new Message(false, "添加失败!", m.returnUrl);
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_TREESPECIES_Model m)
        {
            if (!isExists(new T_SYS_TREESPECIES_SW { TSPCODE = m.TSPCODE }))
                return new Message(false, "修改失败，该树种编码不存在!", m.returnUrl);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_TREESPECIES");
            sb.AppendFormat(" set ");
            sb.AppendFormat("TSPCODE='{0}'", ClsSql.EncodeSql(m.TSPCODE));
            sb.AppendFormat(",TSPNAME='{0}'", ClsSql.EncodeSql(m.TSPNAME));
            sb.AppendFormat(",LATINNAME='{0}'", ClsSql.EncodeSql(m.LATINNAME));
            sb.AppendFormat(",ORDERBY={0}", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" where TSPCODE= '{0}'", ClsSql.EncodeSql(m.TSPCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.returnUrl);
            else
                return new Message(false, "修改失败!", m.returnUrl);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除(包含所有的子目录）
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_TREESPECIES_Model m)
        {
            if (isExistsChild(new T_SYS_TREESPECIES_SW { TSPCODE = m.TSPCODE }))
                return new Message(false, "存在下属树种，暂无法删除!先删除下属树种，再删除当前树种!", m.returnUrl);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from T_SYS_TREESPECIES WHERE TSPCODE='{0}' ", ClsSql.EncodeSql(m.TSPCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", m.returnUrl);
            else
                return new Message(false, "删除失败!", m.returnUrl);
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(T_SYS_TREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_TREESPECIES where 1=1");
            if (string.IsNullOrEmpty(sw.TSPCODE) == false)
                sb.AppendFormat(" and TSPCODE='{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 判断是否有下级
        /// <summary>
        /// 判断记录是否存在下级
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExistsChild(T_SYS_TREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_TREESPECIES where 1=1");
            if (string.IsNullOrEmpty(sw.TSPCODE) == false)
            {
                sb.AppendFormat(" AND len(TSPCODE)>'{0}'", ClsSql.EncodeSql(sw.TSPCODE).Length);
                sb.AppendFormat(" AND substring(TSPCODE,1,{0})= '{1}'", ClsSql.EncodeSql(sw.TSPCODE).Length, ClsSql.EncodeSql(sw.TSPCODE));
            }
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 根据编码获取名称
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns>参见模型</returns>
        public static string getName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("TSPCODE='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["TSPNAME"].ToString();
            return str;
        }

        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="TSPCODE">编码</param>
        /// <returns>名称</returns>
        public static string getName(string TSPCODE)
        {
            if (string.IsNullOrEmpty(TSPCODE))
                return "";
            string str = DataBaseClass.ReturnSqlField("SELECT TSPNAME FROM T_SYS_TREESPECIES WHERE TSPCODE='" + TSPCODE + "'");
            return str;
        }
        #endregion

        #region 获取数据模型
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getModel(T_SYS_TREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TSPCODE,TSPNAME,LATINNAME,ORDERBY FROM T_SYS_TREESPECIES WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.TSPCODE))
                sb.AppendFormat(" AND TSPCODE = '{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            if (!string.IsNullOrEmpty(sw.TSPNAME))
                sb.AppendFormat(" AND TSPNAME = '{0}'", ClsSql.EncodeSql(sw.TSPNAME));
            if (!string.IsNullOrEmpty(sw.LATINNAME))
                sb.AppendFormat(" AND LATINNAME = '{0}'", ClsSql.EncodeSql(sw.LATINNAME));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_TREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TSPCODE,TSPNAME,LATINNAME,ORDERBY");
            sb.AppendFormat(" FROM  T_SYS_TREESPECIES");
            sb.AppendFormat(" WHERE   1=1");
            if (sw.IsGetTopCode)
                sb.AppendFormat(" AND len(TSPCODE)='2'");
            if (!string.IsNullOrEmpty(sw.TSPNAME))
                sb.AppendFormat(" AND TSPNAME like '%{0}%'", ClsSql.EncodeSql(sw.TSPNAME));
            if (string.IsNullOrEmpty(sw.LATINNAME) == false)
                sb.AppendFormat(" AND LATINNAME like '%{0}%'", ClsSql.EncodeSql(sw.LATINNAME));
            if (string.IsNullOrEmpty(sw.TSPCODE) == false)
            {
                sb.AppendFormat(" AND Len(TSPCODE) = '{0}'", ClsSql.EncodeSql(sw.ChildCODELength.ToString()));
                sb.AppendFormat(" AND Substring(TSPCODE,1,{0}) = '{1}'", ClsSql.EncodeSql(sw.TSPCODE).Length.ToString(), ClsSql.EncodeSql(sw.TSPCODE));
            }
            sb.AppendFormat(" ORDER BY TSPCODE,ORDERBY ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
