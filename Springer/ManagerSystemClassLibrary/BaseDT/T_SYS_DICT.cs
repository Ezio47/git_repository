using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 数据字典管理
    /// </summary>
    public class T_SYS_DICT
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_DICTModel m)
        {
            if (isExists(new T_SYS_DICTSW { DICTTYPEID = m.DICTTYPEID, DICTVALUE = m.DICTVALUE }))
                return new Message(false, "添加失败，该值已存在!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO T_SYS_DICT(DICTTYPEID,DICTNAME, DICTVALUE, ORDERBY,STANDBY1,STANDBY2,STANDBY3,STANDBY4)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.DICTTYPEID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DICTNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DICTVALUE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.STANDBY1));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.STANDBY2));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.STANDBY3));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.STANDBY4));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败，请检查输入框是否正确!", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_DICTModel m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_DICT ");
            sb.AppendFormat(" SET ");
            //sb.AppendFormat("DICTID={0}", ClsSql.EncodeSql(m.DICTID));
            //sb.AppendFormat("DICTFLAG='{0}'",ClsSql.EncodeSql(m.DICTFLAG));
            sb.AppendFormat(" DICTNAME='{0}'", ClsSql.EncodeSql(m.DICTNAME));
            sb.AppendFormat(",DICTVALUE='{0}'", ClsSql.EncodeSql(m.DICTVALUE));
                sb.AppendFormat(",STANDBY1='{0}'", ClsSql.EncodeSql(m.STANDBY1));
            sb.AppendFormat(",STANDBY2='{0}'", ClsSql.EncodeSql(m.STANDBY2));
            sb.AppendFormat(",STANDBY3='{0}'", ClsSql.EncodeSql(m.STANDBY3));
            sb.AppendFormat(",STANDBY4='{0}'", ClsSql.EncodeSql(m.STANDBY4));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            //sb.AppendFormat("ISMAN={0}", ClsSql.EncodeSql(m.ISMAN));
            sb.AppendFormat(" where DICTID='{0}'", ClsSql.EncodeSql(m.DICTID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_DICTModel m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE T_SYS_DICT ");
            sb.AppendFormat(" WHERE DICTID= '{0}' ", ClsSql.EncodeSql(m.DICTID));
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
        /// <returns>true 存在记录 false 不存在</returns>
        public static bool isExists(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    1");
            sb.AppendFormat(" FROM      T_SYS_DICT");
            sb.AppendFormat(" WHERE   1=1");
            //if (string.IsNullOrEmpty(sw.DICTID) == false)
            //    sb.AppendFormat(" AND DICTID = '{0}'", sw.DICTID);
            if (string.IsNullOrEmpty(sw.DICTTYPEID) == false)
                sb.AppendFormat(" AND DICTTYPEID = '{0}'", sw.DICTTYPEID);
            if (string.IsNullOrEmpty(sw.DICTVALUE) == false)
                sb.AppendFormat(" AND DICTVALUE = '{0}'", sw.DICTVALUE);
            //if (string.IsNullOrEmpty(sw.DICTTYPENAME) == false)
            //    sb.AppendFormat(" AND DICTTYPEID=(SELECT   DICTTYPEID  FROM      T_SYS_DICTTYPE where  DICTTYPENAME = '{0}')", sw.DICTTYPENAME);
            //if (string.IsNullOrEmpty(sw.DICTFLAG) == false)
            //    sb.AppendFormat(" AND DICTTYPEID=(SELECT   DICTTYPEID  FROM      T_SYS_DICTTYPE where  DICTTYPENAME = '{0}')", sw.DICTFLAG);
            sb.AppendFormat(" ORDER BY ORDERBY");
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取某项字典列表
        /// <summary>
        /// 获取某项字典列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT DICTID,DICTTYPEID,DICTNAME, DICTVALUE,ORDERBY, STANDBY1,STANDBY2, STANDBY3, STANDBY4");
            sb.AppendFormat(" FROM  T_SYS_DICT");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DICTID) == false)
                sb.AppendFormat(" AND DICTID = '{0}'", sw.DICTID);
            if (string.IsNullOrEmpty(sw.DICTTYPEID) == false)
                sb.AppendFormat(" AND DICTTYPEID = '{0}'", sw.DICTTYPEID);
            if (string.IsNullOrEmpty(sw.DICTVALUE) == false)
            {
                string[] arr = sw.DICTVALUE.Split(',');
                if (arr.Length == 1)
                    sb.AppendFormat(" AND DICTVALUE = '{0}'", sw.DICTVALUE);
                else
                    sb.AppendFormat(" AND DICTVALUE in({0})", sw.DICTVALUE);
            }
            if (string.IsNullOrEmpty(sw.DICTTYPENAME) == false)
                sb.AppendFormat(" AND DICTTYPEID=(SELECT   DICTTYPEID  FROM   T_SYS_DICTTYPE where  DICTTYPENAME = '{0}')", sw.DICTTYPENAME);
            if (string.IsNullOrEmpty(sw.DICTFLAG) == false)
                sb.AppendFormat(" AND DICTTYPEID=(SELECT   DICTTYPEID  FROM   T_SYS_DICTTYPE where  DICTTYPENAME = '{0}')", sw.DICTFLAG);
            if (!string.IsNullOrEmpty(sw.STANDBY1))
                sb.AppendFormat(" AND STANDBY1= '{0}'", sw.STANDBY1);
            sb.AppendFormat(" ORDER BY ORDERBY");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取字典类别列表
        /// <summary>
        /// 获取表T_SYS_DICTTYPE数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDICTFLAGDT(T_SYS_DICTTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT  DICTTYPEID,DICTTYPERID, DICTTYPENAME, SYSFLAG, ISMAN, MARK, ORDERBY");
            sb.AppendFormat(" FROM   T_SYS_DICTTYPE");
            sb.AppendFormat(" WHERE 1=1");
            if (string.IsNullOrEmpty(sw.DICTTYPEID) == false)
            {
                sb.AppendFormat(" AND DICTTYPEID={0}", sw.DICTTYPEID);
            }
            if (string.IsNullOrEmpty(sw.DICTTYPERID) == false)
            {
                sb.AppendFormat(" AND DICTTYPERID={0}", sw.DICTTYPERID);
            } 
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
            {
                sb.AppendFormat(" AND (SYSFLAG='{0}' or SYSFLAG='0')", sw.SYSFLAG);
            }
            if (string.IsNullOrEmpty(sw.ISMAN) == false)
            {
                sb.AppendFormat(" AND ISMAN='{0}'", sw.ISMAN);
            }
            sb.AppendFormat(" ORDER BY ORDERBY", "");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 根据编码获取名称
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="dt">字典DataTable</param>
        /// <param name="value">编码</param>
        /// <returns>名称</returns>
        public static string getName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("DICTVALUE='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["DICTNAME"].ToString();
            return str;
        }

        /// <summary>
        /// 根据编码获取字典类别名称
        /// </summary>
        /// <param name="dt">字典DataTable</param>
        /// <param name="value">编码</param>
        /// <returns>名称</returns>
        public static string getDicTypeName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("DICTTYPEID='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["DICTTYPENAME"].ToString();
            return str;
        }

        /// <summary>
        /// 获取字典名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static string getName(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT DICTNAME FROM T_SYS_DICT WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.DICTTYPEID))
                sb.AppendFormat(" AND DICTTYPEID='{0}'", sw.DICTTYPEID);
            if (!string.IsNullOrEmpty(sw.DICTVALUE))
                sb.AppendFormat(" AND DICTVALUE='{0}'", sw.DICTVALUE);
            return DataBaseClass.ReturnSqlField(sb.ToString());
        }

        /// <summary>
        /// 获取字典类别名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static string getDicTypeName(T_SYS_DICTTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT DICTTYPENAME FROM T_SYS_DICTTYPE WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.DICTTYPEID))
                sb.AppendFormat(" AND DICTTYPEID='{0}'", sw.DICTTYPEID);
            if (!string.IsNullOrEmpty(sw.DICTTYPERID))
                sb.AppendFormat(" AND DICTTYPERID='{0}'", sw.DICTTYPERID);
            return DataBaseClass.ReturnSqlField(sb.ToString());
        }
        #endregion
    }
}
