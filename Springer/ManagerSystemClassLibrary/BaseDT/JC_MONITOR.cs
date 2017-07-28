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
    /// 监测_电子监控表
    /// </summary>
    public class JC_MONITOR
    {
        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_MONITOR_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_MONITOR(TTBH, IMBTIME, JD, WD, SPJ, FYJ, IMBIMGURL)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.TTBH));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.IMBTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SPJ));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FYJ));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.IMBIMGURL));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(JC_MONITOR_Model m)
        {
            bool bln = DataBaseClass.ExeSql("delete  from JC_MONITOR where IMBID=" + m.IMBID + "");
           
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 管理
        /// <summary>
        /// 管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Man(JC_MONITOR_Model m)
        {
            if (string.IsNullOrEmpty(m.MANTIME))
                m.MANTIME = ClsSwitch.SwitTM(DateTime.Now);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_MONITOR");
            sb.AppendFormat(" set ");
            sb.AppendFormat("MANSTATE='{0}'", ClsSql.EncodeSql(m.MANSTATE));
            sb.AppendFormat(",MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            sb.AppendFormat(",MANTIME='{0}'", ClsSql.EncodeSql(m.MANTIME));
            sb.AppendFormat(",MANUSERID='{0}'", ClsSql.EncodeSql(m.MANUSERID));
            sb.AppendFormat(" where IMBID= '{0}'", ClsSql.EncodeSql(m.IMBID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_MONITOR_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" SELECT   IMBID, TTBH, IMBTIME, JD, WD, SPJ, FYJ, IMBIMGURL, MANSTATE, MANRESULT, MANTIME,MANUSERID");
            sb.AppendFormat(" from JC_MONITOR");
            sb.AppendFormat(" WHERE   1 = 1");

            if (string.IsNullOrEmpty(sw.IMBID) == false)
                sb.AppendFormat(" AND IMBID = '{0}'", ClsSql.EncodeSql(sw.IMBID));
            if (string.IsNullOrEmpty(sw.TTBH) == false)
                sb.AppendFormat(" AND TTBH = '{0}'", ClsSql.EncodeSql(sw.TTBH));

            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND IMBTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND IMBTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE = '{0}'", ClsSql.EncodeSql(sw.MANSTATE));
            string sql = sb.ToString()
                + " order by IMBTIME DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
