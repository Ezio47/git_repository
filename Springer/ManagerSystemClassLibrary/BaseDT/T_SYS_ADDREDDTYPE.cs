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
    /// 通讯录类别管理
    /// </summary>
    public class T_SYS_ADDREDDTYPE
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_ADDREDDTYPE_Model m)
        { 
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_ADDREDDTYPE(  RATID, RTNAME, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.RATID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RTNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_ADDREDDTYPE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE T_SYS_ADDREDDTYPE");
            sb.AppendFormat(" set ");
            sb.AppendFormat("RATID='{0}'", ClsSql.EncodeSql(m.RATID));
            sb.AppendFormat(",RTNAME='{0}'", ClsSql.EncodeSql(m.RTNAME));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));

            sb.AppendFormat(" where ATID= '{0}'", ClsSql.EncodeSql(m.ATID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_ADDREDDTYPE_Model m)
        {
            if (DataBaseClass.JudgeRecordExists("select ATID from    T_SYS_ADDREDDTYPE where RATID=" + m.ATID + "") == true)
                return new Message(false, "删除失败，请先删除子类别！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_ADDREDDTYPE");
            sb.AppendFormat(" where ATID= '{0}'", ClsSql.EncodeSql(m.ATID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(T_SYS_ADDREDDTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    T_SYS_ADDREDDTYPE a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.ATID) == false)
                sb.AppendFormat("and ATID = '{0}'", ClsSql.EncodeSql(sw.ATID));
            if (string.IsNullOrEmpty(sw.RATID) == false)
                sb.AppendFormat("and RATID = '{0}'", ClsSql.EncodeSql(sw.RATID));
            string sql = ("select ATID, RATID, RTNAME, ORDERBY") + sb.ToString() + (" order by ATID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
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
            DataRow[] dr = dt.Select("ATID='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["RTNAME"].ToString();
            return str;
        }
        #endregion


    }
}
