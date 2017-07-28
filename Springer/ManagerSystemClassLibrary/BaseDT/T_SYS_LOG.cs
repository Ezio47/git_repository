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
   /// 日志管理基本类
   /// </summary>
    public class T_SYS_LOG
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_LOG_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_LOG(LOGTYPE, OPERATION, OPERATIONCONTENT, LOGINUSERID, USERIP, OPERATETIME,SYSFLAG)");
            sb.AppendFormat("VALUES("); 
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.LOGTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPERATION));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPERATIONCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LOGINUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.USERIP));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPERATETIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
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
        public static Message Mdy(T_SYS_LOG_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE T_SYS_LOG");
            sb.AppendFormat(" set ");
            sb.AppendFormat("LOGTYPE='{0}'", ClsSql.EncodeSql(m.LOGTYPE));
            sb.AppendFormat(",OPERATION='{0}'", ClsSql.EncodeSql(m.OPERATION));
            sb.AppendFormat(",OPERATIONCONTENT='{0}'", ClsSql.EncodeSql(m.OPERATIONCONTENT));
            if (string.IsNullOrEmpty(m.LOGINUSERID) == false)
            sb.AppendFormat(",LOGINUSERID='{0}'", ClsSql.EncodeSql(m.LOGINUSERID));
            if (string.IsNullOrEmpty(m.USERIP) == false)
            sb.AppendFormat(",USERIP='{0}'", ClsSql.EncodeSql(m.USERIP));
            if (string.IsNullOrEmpty(m.OPERATETIME)==false)
            sb.AppendFormat(",OPERATETIME='{0}'", ClsSql.EncodeSql(m.OPERATETIME));
            //sb.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(" where LOGID= '{0}'", ClsSql.EncodeSql(m.LOGID));


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
        public static Message Del(T_SYS_LOG_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_LOG");
            sb.AppendFormat(" where LOGID= '{0}'", ClsSql.EncodeSql(m.LOGID));
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
        /// <returns>true存在 false 不存在 </returns>
        public static bool isExists(T_SYS_LOG_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_LOG where 1=1");
            if (string.IsNullOrEmpty(sw.LOGID) == false)
                sb.AppendFormat(" where LOGID= '{0}'", ClsSql.EncodeSql(sw.LOGID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_LOG_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      T_SYS_LOG");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.LOGID) == false)
                sb.AppendFormat(" AND LOGID = '{0}'", ClsSql.EncodeSql(sw.LOGID));
            if (string.IsNullOrEmpty(sw.LOGTYPE) == false)
                sb.AppendFormat(" AND LOGTYPE = '{0}'", ClsSql.EncodeSql(sw.LOGTYPE));
            if (string.IsNullOrEmpty(sw.OPERATION) == false)
                sb.AppendFormat(" AND OPERATION like '%{0}%'", ClsSql.EncodeSql(sw.OPERATION));
            if (string.IsNullOrEmpty(sw.LOGINUSERID) == false)
                sb.AppendFormat(" AND LOGINUSERID IN({0})", ClsSql.EncodeSql(sw.LOGINUSERID));

            if (string.IsNullOrEmpty(sw.TIMEBegin) == false)
                sb.AppendFormat(" AND OPERATETIME >= '{0}'", ClsSql.EncodeSql(sw.TIMEBegin));
            if (string.IsNullOrEmpty(sw.TIMEEnd) == false)
                sb.AppendFormat(" AND OPERATETIME <= '{0}'", ClsSql.EncodeSql(sw.TIMEEnd));
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" AND SYSFLAG = '{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            string sql = "SELECT LOGID, LOGTYPE, OPERATION, OPERATIONCONTENT, LOGINUSERID, USERIP, OPERATETIME,SYSFLAG"
                + sb.ToString()
                + " order by OPERATETIME DESC";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_LOG_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT LOGID, LOGTYPE, OPERATION, OPERATIONCONTENT, LOGINUSERID, USERIP, OPERATETIME,SYSFLAG");
            sb.AppendFormat(" FROM      T_SYS_LOG");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.LOGID) == false)
                sb.AppendFormat(" AND LOGID = '{0}'", ClsSql.EncodeSql(sw.LOGID));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
