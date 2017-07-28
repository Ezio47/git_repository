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
    /// 用户角色关联基本操作类
    /// </summary>
    public class T_SYSSEC_USER_ROLE
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(T_SYSSEC_USER_ROLE_Model m)
        {
            if (string.IsNullOrEmpty(m.USERID))
                return null;
            //首先删除该用户对应的所有角色
            DataBaseClass.ExeSql("delete T_SYSSEC_USER_ROLE where USERID=" + ClsSql.EncodeSql(m.USERID));
            string a = "INSERT INTO T_SYSSEC_USER_ROLE (USERID, ROLEID) SELECT   " + m.USERID + " , ROLEID FROM      T_SYSSEC_ROLE WHERE   ROLEID IN (" + m.ROLEID + ")";
            if (string.IsNullOrEmpty(m.ROLEID) == false)
            {
                //一次性插入该用户对应的角色
                DataBaseClass.ExeSql("INSERT INTO T_SYSSEC_USER_ROLE (USERID, ROLEID) SELECT   " + m.USERID + " , ROLEID FROM      T_SYSSEC_ROLE WHERE   ROLEID IN (" + m.ROLEID + ")");
            }
            return new Message(true, "添加成功！", "");
        }

        /// <summary>
        /// 获取用户所拥有的角色信息，多个用户以逗号分隔
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYSSEC_USER_ROLE_SW sw)
        {
            if (string.IsNullOrEmpty(sw.USERID))
                sw.USERID = "0";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ROLEID, USERID");
            sb.AppendFormat(" FROM      T_SYSSEC_USER_ROLE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.USERID) == false)
                sb.AppendFormat(" AND USERID in({0})", ClsSql.EncodeSql(sw.USERID));
            sb.AppendFormat(" ORDER BY ROLEID ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
