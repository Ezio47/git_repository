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
    /// 数据中心-类别表
    /// </summary>
    public class DC_TYPE
    {

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_TYPE_Model m)
        {
            if (m.DCTYPETOPID == "0")
                return new Message(false, "操作失败，顶级类别 禁止操作！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  DC_TYPE(  DCTYPETOPID, DCTYPENAME, ORDERBY, DCTYPEFLAG)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.DCTYPETOPID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DCTYPENAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DCTYPEFLAG));
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
        public static Message Mdy(DC_TYPE_Model m)
        {
            if (m.DCTYPETOPID == "0")
                return new Message(false, "操作失败，顶级类别 禁止操作！", "");
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_TYPE");
            sb.AppendFormat(" set ");
            sb.AppendFormat("DCTYPETOPID='{0}'", ClsSql.EncodeSql(m.DCTYPETOPID));
            sb.AppendFormat(",DCTYPENAME='{0}'", ClsSql.EncodeSql(m.DCTYPENAME));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(",DCTYPEFLAG='{0}'", ClsSql.EncodeSql(m.DCTYPEFLAG));

            sb.AppendFormat(" where DCTYPEID= '{0}'", ClsSql.EncodeSql(m.DCTYPEID));
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
        public static Message Del(DC_TYPE_Model m)
        {
            if (m.DCTYPETOPID == "0")
                return new Message(false, "操作失败，顶级类别 禁止操作！", "");
            if (DataBaseClass.JudgeRecordExists("select DCTYPEID from    DC_TYPE where DCTYPETOPID="+m.DCTYPEID+"") == true)
                return new Message(false, "删除失败，请先删除子类别！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_TYPE");
            sb.AppendFormat(" where DCTYPEID= '{0}'", ClsSql.EncodeSql(m.DCTYPEID));
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
        public static DataTable getDT(DC_TYPE_SW sw) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_TYPE a ");
            sb.AppendFormat("where 1=1");
            if (string.IsNullOrEmpty(sw.DCTYPEID) == false)
                sb.AppendFormat("and DCTYPEID = '{0}'", ClsSql.EncodeSql(sw.DCTYPEID));
            if (string.IsNullOrEmpty(sw.DCTYPETOPID) == false)
                sb.AppendFormat("and DCTYPETOPID = '{0}'", ClsSql.EncodeSql(sw.DCTYPETOPID));
            if (string.IsNullOrEmpty(sw.DCTYPENAME) == false)
                sb.AppendFormat("and DCTYPENAME = '{0}'", ClsSql.EncodeSql(sw.DCTYPENAME));
            if (string.IsNullOrEmpty(sw.ORDERBY) == false)
                sb.AppendFormat("and ORDERBY = '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            if (string.IsNullOrEmpty(sw.DCTYPEFLAG) == false)
                sb.AppendFormat(" AND DCTYPEFLAG = '{0}'", ClsSql.EncodeSql(sw.DCTYPEFLAG));
            string sql = ("select DCTYPEID,DCTYPETOPID,DCTYPENAME,ORDERBY,DCTYPEFLAG") + sb.ToString() + (" order by DCTYPEID ");
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
        #region 获取序号id
        /// <summary>
        /// 获取序号id
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getID(DC_TYPE_SW sw) 
        {
            DataTable dt = getDT(sw);
            string dctypeid = "";
            if (dt.Rows.Count > 0) 
                dctypeid = dt.Rows[0]["DCTYPEID"].ToString();
            dt.Clear();
            dt.Dispose();
            return dctypeid;
        }
        #endregion
    }
}
