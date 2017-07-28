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
    /// 角色管理基本类
    /// </summary>
    public class T_SYSSEC_ROLE
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYSSEC_ROLE_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYSSEC_ROLE(ROLENAME, ROLENOTE, SYSFLAG,ROLELEVEL, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.ROLENAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ROLENOTE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ROLELEVEL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYSSEC_ROLE_Model m)
        {

            StringBuilder sb = new StringBuilder();
            //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
            sb.AppendFormat("UPDATE T_SYSSEC_ROLE");
            sb.AppendFormat(" set ");
            sb.AppendFormat("ROLENAME='{0}'", ClsSql.EncodeSql(m.ROLENAME));
            sb.AppendFormat(",ROLENOTE='{0}'", ClsSql.EncodeSql(m.ROLENOTE));
            sb.AppendFormat(",ROLELEVEL='{0}'", ClsSql.EncodeSql(m.ROLELEVEL));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" where ROLEID= '{0}'", ClsSql.EncodeSql(m.ROLEID));
            sb.AppendFormat(" and SYSFLAG= '{0}'", ClsSql.EncodeSql(m.SYSFLAG));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYSSEC_ROLE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYSSEC_ROLE");
            sb.AppendFormat(" where ROLEID= '{0}'", ClsSql.EncodeSql(m.ROLEID));
            sb.AppendFormat(" and SYSFLAG= '{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 获取数据,获取所有角色
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYSSEC_ROLE_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ROLEID, ROLENAME, ROLENOTE, SYSFLAG,ROLELEVEL, ORDERBY");
            sb.AppendFormat(" FROM      T_SYSSEC_ROLE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ROLEID) == false)
                sb.AppendFormat(" AND ROLEID = '{0}'", ClsSql.EncodeSql(sw.ROLEID));
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" AND SYSFLAG = '{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            //if (string.IsNullOrEmpty(sw.USERID) == false)
            //    sb.AppendFormat(" AND USERID = '{0}'", ClsSql.EncodeSql(sw.USERID));
            string curOrg = SystemCls.getCurUserOrgNo();
            if (PublicCls.OrgIsShi(curOrg))
                sb.AppendFormat(" AND ROLELEVEL>=1");
            else if (PublicCls.OrgIsXian(curOrg))
                sb.AppendFormat(" AND ROLELEVEL>=2");
            else
                sb.AppendFormat(" AND ROLELEVEL>=3");
            sb.AppendFormat(" ORDER BY  ORDERBY ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="dt">角色DataTable</param>
        /// <param name="value">编码</param>
        /// <returns>名称</returns>
        public static string getName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("ROLEID='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["ROLENAME"].ToString();
            return str;
        } 
    }
}
