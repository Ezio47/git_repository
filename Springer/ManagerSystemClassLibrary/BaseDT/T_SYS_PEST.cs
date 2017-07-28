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
    /// 有害生物表
    /// </summary>
    public class T_SYS_PEST
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_PEST_Model m)
        {
            if (isExists(new T_SYS_PEST_SW { PESTCODE = m.PESTCODE, }) == true)
                return new Message(false, "添加失败，该有害生物编码已存在!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT  INTO  T_SYS_PEST(PESTCODE,PESTNAME,LATINNAME,ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.PESTCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PESTNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LATINNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ORDERBY));
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
        public static Message Mdy(T_SYS_PEST_Model m)
        {
            if (!isExists(new T_SYS_PEST_SW { PESTCODE = m.PESTCODE }))
                return new Message(false, "修改失败，该有害生物编码不存在!", m.returnUrl);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update T_SYS_PEST");
            sb.AppendFormat(" set ");
            sb.AppendFormat("PESTCODE='{0}'", ClsSql.EncodeSql(m.PESTCODE));
            sb.AppendFormat(",PESTNAME='{0}'", ClsSql.EncodeSql(m.PESTNAME));
            sb.AppendFormat(",LATINNAME='{0}'", ClsSql.EncodeSql(m.LATINNAME));
            sb.AppendFormat(",ORDERBY={0}", ClsSql.saveNullField(m.ORDERBY));
            sb.AppendFormat(" where PESTCODE= '{0}'", ClsSql.EncodeSql(m.PESTCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.returnUrl);
            else
                return new Message(false, "修改失败!", m.returnUrl);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_PEST_Model m)
        {
            string[] arrPESTCODE = m.PESTCODE.Split(',');
            for (int i = 0; i < arrPESTCODE.Length; i++)
            {
                if (isExistsChild(new T_SYS_PEST_SW { PESTCODE = arrPESTCODE[i] }))
                {
                    return new Message(false, "存在有害生物有下属有害生物，暂无法删除!先删除下属有害生物，再删除当前有害生物!", m.returnUrl);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from T_SYS_PEST");
            sb.AppendFormat(" where PESTCODE  in  (");
            for (int i = 0; i < arrPESTCODE.Length; i++)
            {
                if (i != arrPESTCODE.Length - 1)
                    sb.AppendFormat("'{0}',", ClsSql.EncodeSql(arrPESTCODE[i]));
                else
                    sb.AppendFormat("'{0}'", ClsSql.EncodeSql(arrPESTCODE[i]));
            }
            sb.AppendFormat(")");
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
        public static bool isExists(T_SYS_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_PEST where 1=1");
            if (string.IsNullOrEmpty(sw.PESTCODE) == false)
                sb.AppendFormat(" and PESTCODE='{0}'", ClsSql.EncodeSql(sw.PESTCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 判断是否有下级
        /// <summary>
        /// 判断记录是否存在下级
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExistsChild(T_SYS_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_PEST where 1=1");
            if (string.IsNullOrEmpty(sw.PESTCODE) == false)
            {
                sb.AppendFormat(" AND len(PESTCODE)>'{0}'", ClsSql.EncodeSql(sw.PESTCODE).Length);
                sb.AppendFormat(" AND substring(PESTCODE,1,{0})= '{1}'", ClsSql.EncodeSql(sw.PESTCODE).Length, ClsSql.EncodeSql(sw.PESTCODE));
            }
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 根据编码获取名称
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="PESTCODE">编码</param>
        /// <returns>名称</returns>
        public static string getName(string PESTCODE)
        {
            if (string.IsNullOrEmpty(PESTCODE))
                return "";
            string str = DataBaseClass.ReturnSqlField("SELECT PESTNAME FROM T_SYS_PEST WHERE PESTCODE='" + PESTCODE + "'");
            return str;
        }

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
            DataRow[] dr = dt.Select("PESTCODE='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["PESTNAME"].ToString();
            return str;
        }
        #endregion

        #region 获取数据模型
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getModel(T_SYS_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT PESTCODE,PESTNAME,LATINNAME,ISLOCAL,ORDERBY FROM T_SYS_PEST WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.PESTCODE))
                sb.AppendFormat(" AND PESTCODE = '{0}'", ClsSql.EncodeSql(sw.PESTCODE));
            if (!string.IsNullOrEmpty(sw.PESTNAME))
                sb.AppendFormat(" AND PESTNAME = '{0}'", ClsSql.EncodeSql(sw.PESTNAME));
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
        public static DataTable getDT(T_SYS_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT PESTCODE,PESTNAME,LATINNAME,ISLOCAL,ORDERBY");
            sb.AppendFormat(" FROM  T_SYS_PEST");
            sb.AppendFormat(" WHERE   1=1");
            if (sw.IsGetTopCode)
                sb.AppendFormat(" AND Len(PESTCODE)='2'");
            if (!string.IsNullOrEmpty(sw.PESTNAME))
                sb.AppendFormat(" AND PESTNAME like '%{0}%'", ClsSql.EncodeSql(sw.PESTNAME));
            if (string.IsNullOrEmpty(sw.LATINNAME) == false)
                sb.AppendFormat(" AND LATINNAME like '%{0}%'", ClsSql.EncodeSql(sw.LATINNAME));

            if (string.IsNullOrEmpty(sw.PESTCODE) == false)
            {
                if (sw.GetAllChileCode)
                {
                    string lenth = sw.PESTCODE.Length.ToString();
                    sb.AppendFormat(" AND Len(PESTCODE) >'{0}' AND Substring(PESTCODE,1," + lenth + ")='{1}'", ClsSql.EncodeSql(sw.PESTCODE.Length.ToString()), ClsSql.EncodeSql(sw.PESTCODE));
                }
                else
                {
                    sb.AppendFormat(" AND Len(PESTCODE) = '{0}'", ClsSql.EncodeSql(sw.ChildCODELength.ToString()));
                    sb.AppendFormat(" AND Substring(PESTCODE,1,{0}) = '{1}'", ClsSql.EncodeSql(sw.PESTCODE).Length.ToString(), ClsSql.EncodeSql(sw.PESTCODE));
                }
            }
            sb.AppendFormat(" ORDER BY PESTCODE,ORDERBY ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 获取最长编码长度
        /// <summary>
        /// 获取最长编码长度
        /// </summary>
        /// <returns></returns>
        public static string GetMaxCodeLength()
        {
            string sql = "select len(PESTCODE) as lenth from T_SYS_PEST order by  lenth desc";
            return DataBaseClass.ReturnSqlField(sql);
        }
        #endregion
    }
}
