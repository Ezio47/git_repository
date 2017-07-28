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
    /// 通知公告管理基本类
    /// </summary>
    public class T_SYS_NOTICE
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_NOTICE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_NOTICE( INFOTITLE, INFOCONTENT, INFOURL, FBTIME, LABLE, NUM, INFOTYPE, INFOUSERID,SYSFLAG)");
            sb.AppendFormat("VALUES("); 
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.INFOTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.INFOCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.INFOURL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FBTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LABLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.NUM));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.INFOTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.INFOUSERID));
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
        public static Message Mdy(T_SYS_NOTICE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE T_SYS_NOTICE");
            sb.AppendFormat(" set "); 
            sb.AppendFormat("INFOTITLE='{0}'", ClsSql.EncodeSql(m.INFOTITLE));
            sb.AppendFormat(",INFOCONTENT='{0}'", ClsSql.EncodeSql(m.INFOCONTENT));
            if (string.IsNullOrEmpty(ClsSql.EncodeSql(m.INFOURL)) == false)
            sb.AppendFormat(",INFOURL='{0}'", ClsSql.EncodeSql(m.INFOURL));
            if (string.IsNullOrEmpty(ClsSql.EncodeSql(m.FBTIME))==false)
                sb.AppendFormat(",FBTIME='{0}'", ClsSql.EncodeSql(m.FBTIME));
            sb.AppendFormat(",LABLE='{0}'", ClsSql.EncodeSql(m.LABLE));
            //sb.AppendFormat(",NUM='{0}'", ClsSql.EncodeSql(m.NUM));
            //sb.AppendFormat(",INFOTYPE='{0}'", ClsSql.EncodeSql(m.INFOTYPE));
            //sb.AppendFormat(",INFOUSERID='{0}'", ClsSql.EncodeSql(m.INFOUSERID));
            //sb.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(" where INFOID= '{0}'", ClsSql.EncodeSql(m.INFOID));

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
        public static Message Del(T_SYS_NOTICE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_NOTICE");
            sb.AppendFormat(" where INFOID= '{0}'", ClsSql.EncodeSql(m.INFOID));
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
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(T_SYS_NOTICE_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_NOTICE where 1=1");
            if (string.IsNullOrEmpty(sw.INFOID) == false)
                sb.AppendFormat(" and HID='{0}'", ClsSql.EncodeSql(sw.INFOID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_NOTICE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      T_SYS_NOTICE a left outer join T_SYSSEC_USER b on a.INFOUSERID=b.USERID");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.INFOID) == false)
                sb.AppendFormat(" AND a.INFOID = '{0}'", ClsSql.EncodeSql(sw.INFOID));
            if (string.IsNullOrEmpty(sw.INFOTITLE) == false)
                sb.AppendFormat(" AND a.INFOTITLE like '%{0}%'", ClsSql.EncodeSql(sw.INFOTITLE));
            if (string.IsNullOrEmpty(sw.INFOTYPE) == false)
                sb.AppendFormat(" AND a.INFOTYPE = '{0}'", ClsSql.EncodeSql(sw.INFOTYPE));
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" AND a.SYSFLAG = '{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            string sql = "SELECT a.INFOID, a.INFOTITLE, a.INFOCONTENT, a.INFOURL, a.FBTIME, a.LABLE, a.NUM, a.INFOTYPE, a.INFOUSERID, a.SYSFLAG,b.LOGINUSERNAME"
                + sb.ToString()
                + " order by a.FBTIME DESC";
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
        public static DataTable getDT(T_SYS_NOTICE_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT INFOID, INFOTITLE, INFOCONTENT, INFOURL, FBTIME, LABLE, NUM, INFOTYPE, INFOUSERID, SYSFLAG");
            sb.AppendFormat(" FROM      T_SYS_NOTICE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.INFOID) == false)
                sb.AppendFormat(" AND INFOID = '{0}'", ClsSql.EncodeSql(sw.INFOID));
            if (string.IsNullOrEmpty(sw.INFOTITLE) == false)
                sb.AppendFormat(" AND INFOTITLE like '%{0}%'", ClsSql.EncodeSql(sw.INFOTITLE));
            if (string.IsNullOrEmpty(sw.INFOTYPE) == false)
                sb.AppendFormat(" AND INFOTYPE = '{0}'", ClsSql.EncodeSql(sw.INFOTYPE));
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" AND SYSFLAG = '{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            sb.AppendFormat(" order by FBTIME DESC");
           
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
