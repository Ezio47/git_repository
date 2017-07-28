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
    /// 权限管理基本类
    /// </summary>
    public class T_SYSSEC_RIGHT
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYSSEC_RIGHT_Model m)
        {
            if (isExists(new T_SYSSEC_RIGHT_SW { RIGHTID = m.RIGHTID, SYSFLAG = m.SYSFLAG }) == true)
                return new Message(false, "添加失败，该编号已存在！", "");
            if (string.IsNullOrEmpty(m.ORDERBY)) m.ORDERBY = "0";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYSSEC_RIGHT(RIGHTID, RIGHTNAME, SYSFLAG,ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.RIGHTID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RIGHTNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
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
        public static Message Mdy(T_SYSSEC_RIGHT_Model m)
        {
            if (isExists(new T_SYSSEC_RIGHT_SW { RIGHTID = m.RIGHTID }) == false)
                return new Message(false, "修改失败，该编号不存在！", "");
            if (string.IsNullOrEmpty(m.ORDERBY)) m.ORDERBY = "0";
            StringBuilder sb = new StringBuilder();
            //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
            sb.AppendFormat("UPDATE T_SYSSEC_RIGHT");
            sb.AppendFormat(" set ");
            sb.AppendFormat("RIGHTNAME='{0}'", ClsSql.EncodeSql(m.RIGHTNAME));
            sb.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" where RIGHTID= '{0}'", ClsSql.EncodeSql(m.RIGHTID));

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
        public static Message Del(T_SYSSEC_RIGHT_Model m)
        {
            if (isExists(new T_SYSSEC_RIGHT_SW { RIGHTID = m.RIGHTID, SYSFLAG = m.SYSFLAG }) == false)
                return new Message(false, "删除失败，该编号不存在！", "");
            //删除所有的子目录
            //DataBaseClass.ExeSql("delete T_SYSSEC_RIGHT where RIGHTID= '"+ClsSql.EncodeSql(m.SYSFLAG)+"'");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYSSEC_RIGHT");
            sb.AppendFormat(" where substring(RIGHTID,1,{0})= '{1}'", ClsSql.EncodeSql(m.RIGHTID).Length.ToString(), ClsSql.EncodeSql(m.RIGHTID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 判断记录是否存在 
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(T_SYSSEC_RIGHT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYSSEC_RIGHT where 1=1");
            if (string.IsNullOrEmpty(sw.RIGHTID) == false)
                sb.AppendFormat(" and RIGHTID='{0}'", ClsSql.EncodeSql(sw.RIGHTID));
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" and SYSFLAG='{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        /// <summary>
        /// 获取数据,获取所有角色
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYSSEC_RIGHT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    RIGHTID, RIGHTNAME, SYSFLAG,ORDERBY");
            sb.AppendFormat(" FROM      T_SYSSEC_RIGHT");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" AND SYSFLAG = '{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            if (string.IsNullOrEmpty(sw.RIGHTID) == false)
                sb.AppendFormat(" AND RIGHTID = '{0}'", ClsSql.EncodeSql(sw.RIGHTID));
            if (string.IsNullOrEmpty(sw.SubRightID) == false)
            {
                if (sw.SubRightID == "0")
                    sb.AppendFormat(" AND Len(RIGHTID)=3");
                else
                    sb.AppendFormat(" AND len(RIGHTID)=" + (sw.SubRightID.Length + 3).ToString() + " AND SUBSTRING(RIGHTID,1," + sw.SubRightID.Length.ToString() + ")=" + sw.SubRightID);
            }
            sb.AppendFormat(" ORDER BY ORDERBY,RIGHTID ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据编号查询权限名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getNameByID(T_SYSSEC_RIGHT_SW sw)
        {
            string sql = "SELECT RIGHTNAME FROM T_SYSSEC_RIGHT WHERE RIGHTID='" + sw.RIGHTID + "' and SYSFLAG='" + sw.SYSFLAG + "'";
            return DataBaseClass.ReturnSqlField(sql);
        }
    }
}
