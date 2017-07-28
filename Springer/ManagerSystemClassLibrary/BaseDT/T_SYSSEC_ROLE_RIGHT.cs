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
    /// 角色权限关联基本类
    /// </summary>
    public class T_SYSSEC_ROLE_RIGHT
    {
        /// <summary>
        /// 保存角色拥有的权限
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(T_SYSSEC_ROLE_RIGHT_Model m)
        {
            if (string.IsNullOrEmpty(m.ROLEID))
                return null;
            //首先删除
            DataBaseClass.ExeSql("delete T_SYSSEC_ROLE_RIGHT where ROLEID=" + ClsSql.EncodeSql(m.ROLEID));
            if (string.IsNullOrEmpty(m.RIGHTID) == false)
            {
                //一次性插入该用户对应的角色
                DataBaseClass.ExeSql("INSERT INTO T_SYSSEC_ROLE_RIGHT (ROLEID, RIGHTID) SELECT   " + m.ROLEID + " , RIGHTID FROM      T_SYSSEC_RIGHT WHERE   RIGHTID IN (" + ClsSql.SwitchStrToSqlIn(m.RIGHTID) + ")");
            }
            return new Message(true, "保存成功！", "");

        }
        /// <summary>
        /// 根据权限编号判断是否存在
        /// </summary>
        /// <param name="dt">角色DataTable</param>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在false不存在 </returns>
        public static bool isDTExists(DataTable dt, T_SYSSEC_ROLE_RIGHT_SW sw)
        {
            DataRow[] dr = dt.Select("RIGHTID='"+sw.RIGHTID+"'");
            if (dr.Length == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 获取角色所拥有的权限信息，多个用户以逗号分隔
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYSSEC_ROLE_RIGHT_SW sw)
        {

            if (string.IsNullOrEmpty(sw.ROLEID))
                sw.ROLEID = "0";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ROLEID, RIGHTID");
            sb.AppendFormat(" FROM       T_SYSSEC_ROLE_RIGHT");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ROLEID) == false)
                sb.AppendFormat(" AND ROLEID in({0})", ClsSql.EncodeSql(sw.ROLEID));

            sb.AppendFormat(" ORDER BY RIGHTID ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回某用户拥有的权限
        /// </summary>
        /// <returns>返回DataTable</returns>
        public static DataTable getDTByUID(T_SYSSEC_IPSUSER_SW sw)
        {
            if (string.IsNullOrEmpty(sw.USERID))//如果未传递，则默认值为0，不取权限 该问题有可能为用户未登录，所以未获取到USERID
                sw.USERID = "0";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ROLEID, RIGHTID");
            sb.AppendFormat(" FROM       T_SYSSEC_ROLE_RIGHT");
            sb.AppendFormat(" where roleid in(select roleid from T_SYSSEC_USER_ROLE where userid={0})", sw.USERID);
            sb.AppendFormat(" and RIGHTID in(select RIGHTID from T_SYSSEC_RIGHT where SYSFLAG='{0}')",ConfigCls.getSystemFlag());//判断角色是否启用

            sb.AppendFormat(" ORDER BY RIGHTID ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
