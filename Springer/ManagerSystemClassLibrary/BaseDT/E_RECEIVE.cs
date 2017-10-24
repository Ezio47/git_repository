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
    /// 收件表-管理
    /// </summary>
    public class E_RECEIVE
    {
        #region 增加
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(E_RECEIVE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO E_RECEIVE(BYEMAILID,RECEIVETYPE,EMAILRECEIVESTATUS, RECEIVEUSERID, EMAILSENDTIME)");//, EMAILRECEIVETIME
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYEMAILID));//非空
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RECEIVETYPE));//非空 抄送 发送 密送
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILRECEIVESTATUS));//非空 未读 0
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RECEIVEUSERID));//非空
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILSENDTIME));//非空 主题表时间
            //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILRECEIVETIME));//就是为空
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "", "");
            else
                return new Message(false, "", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(E_RECEIVE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE E_RECEIVE set ");
            sb.AppendFormat(" EMAILRECEIVESTATUS='{0}'", ClsSql.EncodeSql(m.EMAILRECEIVESTATUS));//未读 -->已读 删除 已读-->删除
            if (string.IsNullOrEmpty(m.EMAILRECEIVETIME) == false)//用于删除标识时，不需要传查看时间/接收时间
                sb.AppendFormat(",EMAILRECEIVETIME='{0}'", ClsSql.EncodeSql(m.EMAILRECEIVETIME));
            sb.AppendFormat(" where 1=1");
            if (string.IsNullOrEmpty(m.ERID))
                return new Message(false, "操作失败!", "");
            string[] arr = m.ERID.Split(',');
            if (arr.Length == 1)
                sb.AppendFormat(" and ERID= '{0}'", ClsSql.EncodeSql(m.ERID));
            else
                sb.AppendFormat(" and ERID in({0})", ClsSql.EncodeSql(m.ERID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "", "");
            else
                return new Message(false, "操作失败!", "");
        }
        #endregion

        #region 多条收件表删除
        /// <summary>
        /// 多条收件表删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message SendMdy(E_RECEIVE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE E_RECEIVE set ");
            sb.AppendFormat(" EMAILRECEIVESTATUS ={0}", ClsSql.EncodeSql(m.EMAILRECEIVESTATUS));//未读 -->已读 删除 已读-->删除
            sb.AppendFormat(" where ERID in({0})", ClsSql.EncodeSql(m.ERID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "操作成功!", "");
            else
                return new Message(false, "操作失败!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(E_RECEIVE_Model m)
        {
            if (string.IsNullOrEmpty(m.ERID))
                return new Message(false, "删除失败!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete E_RECEIVE");
            sb.AppendFormat(" where 1=1");
            string[] arr = m.ERID.Split(',');
            if (arr.Length == 1)
                sb.AppendFormat(" and ERID= '{0}'", ClsSql.EncodeSql(m.ERID));
            else
                sb.AppendFormat(" and ERID in({0})", ClsSql.EncodeSql(m.ERID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(E_RECEIVE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FROM  E_RECEIVE b left join E_SUBJECT a");
            sb.AppendFormat("  on b.BYEMAILID=a.EMAILID");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ERID) == false)
                sb.AppendFormat(" AND b.ERID = '{0}'", ClsSql.EncodeSql(sw.ERID));
            if (string.IsNullOrEmpty(sw.BYEMAILID) == false)
                sb.AppendFormat(" AND b.BYEMAILID ='{0}'", ClsSql.EncodeSql(sw.BYEMAILID));
            if (string.IsNullOrEmpty(sw.RECEIVETYPE) == false)
                sb.AppendFormat(" AND b.RECEIVETYPE = '{0}'", ClsSql.EncodeSql(sw.RECEIVETYPE));
            if (string.IsNullOrEmpty(sw.RECEIVEUSERID) == false)//接收人序号 用于收件箱
                sb.AppendFormat(" AND b.RECEIVEUSERID ='{0}'", ClsSql.EncodeSql(sw.RECEIVEUSERID));
            if (string.IsNullOrEmpty(sw.EMAILRECEIVESTATUS) == false)//接收状态 用于收件箱[已读、未读]\已删除 收件箱传 0,1 已删除 -1
                sb.AppendFormat(" AND b.EMAILRECEIVESTATUS in({0})", ClsSql.EncodeSql(sw.EMAILRECEIVESTATUS));
            if (string.IsNullOrEmpty(sw.EMAILTITLE) == false)//接收状态 用于收件箱[已读、未读]\已删除 收件箱传 0,1 已删除 -1
                sb.AppendFormat(" AND a.EMAILTITLE like '%{0}%'", ClsSql.EncodeSql(sw.EMAILTITLE));
            string sql = "select a.*,b.*" + sb.ToString() + " order by b.EMAILSENDTIME DESC";
            string sqlC = "select count(1) " + sb.ToString();
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
            //return ds.Tables[0];
            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat(" FROM      E_RECEIVE a");
            //sb.AppendFormat(" WHERE   1=1");
            //if (string.IsNullOrEmpty(sw.ERID) == false)
            //    sb.AppendFormat(" AND ERID = '{0}'", ClsSql.EncodeSql(sw.ERID));
            //if (string.IsNullOrEmpty(sw.BYEMAILID) == false)
            //    sb.AppendFormat(" AND BYEMAILID ='{0}'", ClsSql.EncodeSql(sw.BYEMAILID));
            //if (string.IsNullOrEmpty(sw.RECEIVETYPE) == false)
            //    sb.AppendFormat(" AND RECEIVETYPE = '{0}'", ClsSql.EncodeSql(sw.RECEIVETYPE));
            //if (string.IsNullOrEmpty(sw.RECEIVEUSERID) == false)//接收人序号 用于收件箱
            //    sb.AppendFormat(" AND RECEIVEUSERID ='{0}'", ClsSql.EncodeSql(sw.RECEIVEUSERID));

            //if (string.IsNullOrEmpty(sw.EMAILRECEIVESTATUS) == false)//接收状态 用于收件箱[已读、未读]\已删除 收件箱传 0,1 已删除 -1
            //    sb.AppendFormat(" AND EMAILRECEIVESTATUS in({0}", ClsSql.EncodeSql(sw.EMAILRECEIVESTATUS));
            //string sql = "SELECT   ERID, BYEMAILID, RECEIVETYPE, RECEIVEUSERID, EMAILRECEIVESTATUS, EMAILSENDTIME,EMAILRECEIVETIME"
            //    + sb.ToString()
            //    + " order by EMAILSENDTIME DESC";
            //DataSet ds = DataBaseClass.FullDataSet(sql);
            //return ds.Tables[0];
        }
        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(E_RECEIVE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FROM      E_RECEIVE b left join E_SUBJECT a on b.BYEMAILID=a.EMAILID");
            sb.AppendFormat(" WHERE   1=1 ");
            if (string.IsNullOrEmpty(sw.ERID) == false)
                sb.AppendFormat(" AND b.ERID = '{0}'", ClsSql.EncodeSql(sw.ERID));
            if (string.IsNullOrEmpty(sw.BYEMAILID) == false)
                sb.AppendFormat(" AND b.BYEMAILID ='{0}'", ClsSql.EncodeSql(sw.BYEMAILID));
            if (string.IsNullOrEmpty(sw.RECEIVETYPE) == false)
                sb.AppendFormat(" AND b.RECEIVETYPE = '{0}'", ClsSql.EncodeSql(sw.RECEIVETYPE));
            if (string.IsNullOrEmpty(sw.RECEIVEUSERID) == false)//接收人序号 用于收件箱
                sb.AppendFormat(" AND b.RECEIVEUSERID ='{0}'", ClsSql.EncodeSql(sw.RECEIVEUSERID));
            if (sw.EMAILRECEIVESTATUS != null && sw.EMAILRECEIVESTATUS.Trim() != "")//接收状态 用于收件箱[已读、未读]\已删除 收件箱传 0,1 已删除 -1
                sb.AppendFormat(" AND b.EMAILRECEIVESTATUS in({0})", ClsSql.EncodeSql(sw.EMAILRECEIVESTATUS));
            if (!string.IsNullOrEmpty(sw.EMAILTITLE))//主题
                sb.AppendFormat(" AND a.EMAILTITLE like '%{0}%'", ClsSql.EncodeSql(sw.EMAILTITLE));
            string sql = "select a.*,b.*" + sb.ToString() + " order by b.EMAILSENDTIME DESC";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 根据邮件接收的状态判断计算邮件的数量
        /// <summary>
        /// 根据邮件接收的状态判断计算邮件的数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(E_RECEIVE_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    E_RECEIVE  ");
            sb.AppendFormat("where 1 = 1 ");
            if (string.IsNullOrEmpty(sw.RECEIVEUSERID) == false)//接收人序号 用于收件箱
                sb.AppendFormat(" AND RECEIVEUSERID ='{0}'", ClsSql.EncodeSql(sw.RECEIVEUSERID));
            if (sw.EMAILRECEIVESTATUS != null && sw.EMAILRECEIVESTATUS.Trim() != "")//接收状态 用于收件箱[已读、未读]\已删除 收件箱传 0,1 已删除 -1
                sb.AppendFormat(" AND EMAILRECEIVESTATUS in({0})", ClsSql.EncodeSql(sw.EMAILRECEIVESTATUS));
            string sqlC = "select count(1) " + sb.ToString();
            total = (DataBaseClass.ReturnSqlField(sqlC)).ToString();
            return total;
        }
        #endregion
    }
}
