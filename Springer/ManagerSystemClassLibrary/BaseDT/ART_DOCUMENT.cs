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
    /// 文档内容表
    /// </summary>
    public class ART_DOCUMENT
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(ART_DOCUMENT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  ART_DOCUMENT(  ARTTYPEID, ARTTITLE, ARTTAG, ARTTIME, ARTCONTENT, ARTCHECKSTATUS, ARTADDUSERID,                 ARTCHECKTIME, ARTCHECKUSERID,PLANFILENAME,BYORGNO)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.ARTTYPEID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTTAG));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now)));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTCONTENT));
            if (string.IsNullOrEmpty(m.ARTCHECKSTATUS))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTCHECKSTATUS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTADDUSERID));
            if (string.IsNullOrEmpty(m.ARTCHECKTIME))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTCHECKTIME));
            if (string.IsNullOrEmpty(m.ARTCHECKUSERID))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ARTCHECKUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PLANFILENAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));

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
        public static Message Mdy(ART_DOCUMENT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE ART_DOCUMENT");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" ARTTYPEID='{0}'", ClsSql.EncodeSql(m.ARTTYPEID));
            sb.AppendFormat(",ARTTITLE='{0}'", ClsSql.EncodeSql(m.ARTTITLE));
            sb.AppendFormat(",ARTTAG='{0}'", ClsSql.EncodeSql(m.ARTTAG));
            //sb.AppendFormat(",ARTTIME='{0}'", ClsSql.EncodeSql(m.ARTTIME));
            sb.AppendFormat(",ARTCONTENT='{0}'", ClsSql.EncodeSql(m.ARTCONTENT));
            sb.AppendFormat(",ARTCHECKSTATUS='{0}'", ClsSql.EncodeSql(m.ARTCHECKSTATUS));
            sb.AppendFormat(",ARTADDUSERID='{0}'", ClsSql.EncodeSql(m.ARTADDUSERID));
            sb.AppendFormat(",ARTCHECKTIME='{0}'", ClsSql.EncodeSql(m.ARTCHECKTIME));
            sb.AppendFormat(",ARTCHECKUSERID='{0}'", ClsSql.EncodeSql(m.ARTCHECKUSERID));
            sb.AppendFormat(",PLANFILENAME='{0}'", ClsSql.EncodeSql(m.PLANFILENAME));
            sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(" where ARTID= '{0}'", ClsSql.EncodeSql(m.ARTID));
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
        public static Message Del(ART_DOCUMENT_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete ART_DOCUMENT");
            sb.AppendFormat(" where ARTID= '{0}'", ClsSql.EncodeSql(m.ARTID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(ART_DOCUMENT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from ART_DOCUMENT where 1=1");
            if (string.IsNullOrEmpty(sw.ARTID) == false)
                sb.AppendFormat(" where ARTID= '{0}'", ClsSql.EncodeSql(sw.ARTID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(ART_DOCUMENT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
           
            sb.AppendFormat(" FROM      ART_DOCUMENT");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ARTID) == false)
                sb.AppendFormat(" AND ARTID = '{0}'", ClsSql.EncodeSql(sw.ARTID));
            if (string.IsNullOrEmpty(sw.ARTTYPEID) == false)
                sb.AppendFormat(" AND ARTTYPEID = '{0}'", ClsSql.EncodeSql(sw.ARTTYPEID));
            if (string.IsNullOrEmpty(sw.ARTCHECKSTATUS) == false)
                sb.AppendFormat(" AND ARTCHECKSTATUS = '{0}'", ClsSql.EncodeSql(sw.ARTCHECKSTATUS));
            if (string.IsNullOrEmpty(sw.ARTTITLE) == false)
                sb.AppendFormat(" AND ARTTITLE like '%{0}%'", ClsSql.EncodeSql(sw.ARTTITLE));
            if (string.IsNullOrEmpty(sw.ARTTAG) == false)
                sb.AppendFormat(" AND ARTTAG like '%{0}%'", ClsSql.EncodeSql(sw.ARTTAG));

            if (string.IsNullOrEmpty(sw.TIMEBegin) == false)
                sb.AppendFormat(" AND ARTTIME >= '{0}'", ClsSql.EncodeSql(sw.TIMEBegin));
            if (string.IsNullOrEmpty(sw.TIMEEnd) == false)
                sb.AppendFormat(" AND ARTTIME <= '{0}'", ClsSql.EncodeSql(sw.TIMEEnd));
            string sql  ="";
            if (string.IsNullOrEmpty(sw.TOPS))
                sql = "SELECT ARTID, ARTTYPEID, ARTTITLE, ARTTAG, ARTTIME, ARTCONTENT, ARTCHECKSTATUS, ARTADDUSERID,ARTCHECKTIME, ARTCHECKUSERID,PLANFILENAME"
                + sb.ToString()
                + " order by ARTTIME DESC";
            else
                sql = "SELECT top " + sw.TOPS + " ARTID, ARTTYPEID, ARTTITLE, ARTTAG, ARTTIME,  ARTCHECKSTATUS, ARTADDUSERID,ARTCHECKTIME, ARTCHECKUSERID,PLANFILENAME"
                   + sb.ToString()
                   + " order by ARTTIME DESC";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(ART_DOCUMENT_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM   ART_DOCUMENT");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ARTTYPEID) == false)
                sb.AppendFormat(" AND ARTTYPEID = '{0}'", ClsSql.EncodeSql(sw.ARTTYPEID));
            if (string.IsNullOrEmpty(sw.ARTCHECKSTATUS) == false)
                sb.AppendFormat(" AND ARTCHECKSTATUS = '{0}'", ClsSql.EncodeSql(sw.ARTCHECKSTATUS));
            if (string.IsNullOrEmpty(sw.ARTTITLE) == false)
                sb.AppendFormat(" AND ARTTITLE like '%{0}%'", ClsSql.EncodeSql(sw.ARTTITLE));
            if (string.IsNullOrEmpty(sw.ARTTAG) == false)
                sb.AppendFormat(" AND ARTTAG like '%{0}%'", ClsSql.EncodeSql(sw.ARTTAG));
            if (string.IsNullOrEmpty(sw.TIMEBegin) == false)
                sb.AppendFormat(" AND ARTTIME >= '{0}'", ClsSql.EncodeSql(sw.TIMEBegin));
            if (string.IsNullOrEmpty(sw.TIMEEnd) == false)
                sb.AppendFormat(" AND ARTTIME <= '{0}'", ClsSql.EncodeSql(sw.TIMEEnd));
            if (string.IsNullOrEmpty(sw.ARTBigTYPEID) == false)
                sb.AppendFormat(" AND ARTTYPEID like '{0}%'", ClsSql.EncodeSql(sw.ARTBigTYPEID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            string sql = "SELECT ARTID, ARTTYPEID, ARTTITLE, ARTTAG, ARTTIME,  ARTCHECKSTATUS, ARTADDUSERID,ARTCHECKTIME, ARTCHECKUSERID,PLANFILENAME,BYORGNO"
                + sb.ToString()+ " order by ARTTIME DESC";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion
    }
}
