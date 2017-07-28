using DataBaseClassLibrary;
using log4net;
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
    /// 火情报告上传
    /// </summary>
    public class JC_FIRE_REPORT
    {
        private static ILog logs = LogHelper.GetInstance();
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(JC_FIRE_REPORT_SW m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO JC_FIRE_REPORT ( OWERJCFID, FILENAME, FILESIZE, FILEURL, UPLOADTIME,UPLOADUSERID,UPLOADORGNO)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.OWERJCFID));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.FILENAME));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.FILESIZE));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.FILEURL));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(ClsSwitch.SwitTM(m.UPLOADTIME)));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(m.UPLOADUSERID));
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.UPLOADORGNO));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
            {
                logs.Error(sb.ToString());
                return new Message(false, "添加失败！", "");
            }
        }
        #endregion
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_FIRE_REPORT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      JC_FIRE_REPORT");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.OWERJCFID) == false)
                sb.AppendFormat(" AND OWERJCFID = '{0}'", ClsSql.EncodeSql(sw.OWERJCFID));
            if (string.IsNullOrEmpty(sw.FILENAME) == false)
                sb.AppendFormat(" AND FILENAME = '{0}'", ClsSql.EncodeSql(sw.FILENAME));
            if (string.IsNullOrEmpty(sw.FILESIZE) == false)
                sb.AppendFormat(" AND FILESIZE = '{0}'", ClsSql.EncodeSql(sw.FILESIZE));
            if (string.IsNullOrEmpty(sw.FILEURL) == false)
                sb.AppendFormat(" AND FILEURL = '{0}'", ClsSql.EncodeSql(sw.FILEURL));
            if (string.IsNullOrEmpty(sw.UPLOADTIME) == false)
                sb.AppendFormat(" AND UPLOADTIME = '{0}'", ClsSql.EncodeSql(sw.UPLOADTIME));
            if (string.IsNullOrEmpty(sw.UPLOADUSERID) == false)
                sb.AppendFormat(" AND UPLOADUSERID = '{0}'", ClsSql.EncodeSql(sw.UPLOADUSERID));
            if (string.IsNullOrEmpty(sw.UPLOADORGNO) == false)
                sb.AppendFormat(" AND UPLOADORGNO = '{0}'", ClsSql.EncodeSql(sw.UPLOADORGNO));
            string sql = "SELECT ID,OWERJCFID, FILENAME, FILESIZE, FILEURL, UPLOADTIME, UPLOADUSERID, UPLOADORGNO"
                + sb.ToString()
                + " order by OWERJCFID";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
