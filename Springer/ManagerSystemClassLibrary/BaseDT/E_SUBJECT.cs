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
    /// 主题
    /// </summary>
    public class E_SUBJECT
    {
        #region 增加
        /// <summary>
        /// 没有附件添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(E_SUBJECT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  E_SUBJECT(EMAILTITLE,EMAILSTATUS,EMAILSENDUSERID, EMAILTIME, EMAILCONTENT,EMAILRECUSERLIST, EMAILCOPYUSERLIST, EMAILSECRETUSERLIST)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.EMAILTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSTATUS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSENDUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILRECUSERLIST));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILCOPYUSERLIST));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSECRETUSERLIST));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "操作成功!", "");
            else
                return new Message(false, "操作失败!", "");
        }
        #endregion

        #region 增加--返回值
        /// <summary>
        /// 增加--返回值
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string AddReturn(E_SUBJECT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  E_SUBJECT(EMAILTITLE,EMAILSTATUS,EMAILSENDUSERID, EMAILTIME, EMAILCONTENT,EMAILRECUSERLIST, EMAILCOPYUSERLIST, EMAILSECRETUSERLIST)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.EMAILTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSTATUS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSENDUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILRECUSERLIST));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILCOPYUSERLIST));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSECRETUSERLIST));
            sb.AppendFormat(") select @@identity");
            return DataBaseClass.ReturnSqlField(sb.ToString());
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(E_SUBJECT_Model m)
        {
            //if (string.IsNullOrEmpty(m.EMAILTITLE))
            //    return new Message(false, "请输入主题", "");
            //if (string.IsNullOrEmpty(m.EMAILRECUSERLIST))
            //    return new Message(false, "请选择收件人", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE E_SUBJECT set ");
            sb.AppendFormat(" EMAILTITLE='{0}'", ClsSql.EncodeSql(m.EMAILTITLE));
            sb.AppendFormat(",EMAILSTATUS='{0}'", ClsSql.EncodeSql(m.EMAILSTATUS));
            sb.AppendFormat(",EMAILSENDUSERID='{0}'", ClsSql.EncodeSql(m.EMAILSENDUSERID));
            sb.AppendFormat(",EMAILTIME='{0}'", ClsSql.EncodeSql(m.EMAILTIME));
            sb.AppendFormat(",EMAILCONTENT='{0}'", ClsSql.EncodeSql(m.EMAILCONTENT));
            sb.AppendFormat(",EMAILRECUSERLIST='{0}'", ClsSql.EncodeSql(m.EMAILRECUSERLIST));
            sb.AppendFormat(",EMAILCOPYUSERLIST='{0}'", ClsSql.EncodeSql(m.EMAILCOPYUSERLIST));
            sb.AppendFormat(",EMAILSECRETUSERLIST='{0}'", ClsSql.EncodeSql(m.EMAILSECRETUSERLIST));
            sb.AppendFormat(" where EMAILID= '{0}'", ClsSql.EncodeSql(m.EMAILID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "更新成功!", "");
            else
                return new Message(false, "更新失败!", "");
        }
        #endregion

        #region 多条修改
        /// <summary>
        /// 多条修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message SendMdy(E_SUBJECT_Model m)//涉及到多条记录的修改；已发送的列表
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE E_SUBJECT set ");
            sb.AppendFormat("EMAILSTATUS='{0}'", ClsSql.EncodeSql(m.EMAILSTATUS));
            sb.AppendFormat(" where EMAILID in({0})", ClsSql.EncodeSql(m.EMAILID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除 只能删除草稿 对于已发送邮件需要更改状态为已删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(E_SUBJECT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete E_SUBJECT ");
            sb.AppendFormat(" where EMAILID in({0})", ClsSql.EncodeSql(m.EMAILID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 获取邮件主题ID
        /// <summary>
        /// 获取邮件主题ID
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getID(E_SUBJECT_SW sw)
        {
            DataTable dt = getDT(sw);
            string EMAILid = "";
            if (dt.Rows.Count > 0)
                EMAILid = dt.Rows[0]["EMAILID"].ToString();
            dt.Clear();
            dt.Dispose();
            return EMAILid;

        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(E_SUBJECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      E_SUBJECT a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.EMAILID) == false)
                sb.AppendFormat(" AND EMAILID = '{0}'", ClsSql.EncodeSql(sw.EMAILID));
            if (string.IsNullOrEmpty(sw.EMAILTIME) == false)
                sb.AppendFormat(" AND EMAILTIME = '{0}'", ClsSql.EncodeSql(sw.EMAILTIME));
            if (string.IsNullOrEmpty(sw.EMAILTITLE) == false)
                sb.AppendFormat(" AND EMAILTITLE like '%{0}%'", ClsSql.EncodeSql(sw.EMAILTITLE));
            if (string.IsNullOrEmpty(sw.EMAILSENDUSERID) == false)
                sb.AppendFormat(" AND EMAILSENDUSERID ='{0}'", ClsSql.EncodeSql(sw.EMAILSENDUSERID));
            if (string.IsNullOrEmpty(sw.EMAILSTATUS) == false)//状态
                sb.AppendFormat(" AND EMAILSTATUS = '{0}'", ClsSql.EncodeSql(sw.EMAILSTATUS));
            string sql = "SELECT    EMAILID, EMAILTITLE, EMAILSTATUS, EMAILSENDUSERID, EMAILTIME, EMAILCONTENT, EMAILRECUSERLIST, EMAILCOPYUSERLIST, EMAILSECRETUSERLIST"
                + sb.ToString()
                + " order by EMAILTIME DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(E_SUBJECT_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      E_SUBJECT a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.EMAILID) == false)
                sb.AppendFormat(" AND EMAILID = '{0}'", ClsSql.EncodeSql(sw.EMAILID));
            if (string.IsNullOrEmpty(sw.EMAILTITLE) == false)
                sb.AppendFormat(" AND EMAILTITLE like '%{0}%'", ClsSql.EncodeSql(sw.EMAILTITLE));
            if (string.IsNullOrEmpty(sw.EMAILSENDUSERID) == false)
                sb.AppendFormat(" AND EMAILSENDUSERID ='{0}'", ClsSql.EncodeSql(sw.EMAILSENDUSERID));
            if (string.IsNullOrEmpty(sw.EMAILSTATUS) == false)//状态
                sb.AppendFormat(" AND EMAILSTATUS = '{0}'", ClsSql.EncodeSql(sw.EMAILSTATUS));
            //if (string.IsNullOrEmpty(sw.RECEIVEUSERID))//接收人序号不为空，获取收件箱
            //{

            //}
            string sql = "SELECT   EMAILID, EMAILTITLE, EMAILSTATUS, EMAILSENDUSERID, EMAILTIME, EMAILCONTENT, EMAILRECUSERLIST, EMAILCOPYUSERLIST, EMAILSECRETUSERLIST"
                + sb.ToString()
                + " order by EMAILTIME DESC";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion
    }
}
