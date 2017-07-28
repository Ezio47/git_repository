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
    /// 预警_卫星云图表
    /// </summary>
    public class YJ_SATELLITECLOUD
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(YJ_SATELLITECLOUD_SW sw)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      YJ_SATELLITECLOUD a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.CLOUDID) == false)
                sb.AppendFormat(" AND CLOUDID = '{0}'", ClsSql.EncodeSql(sw.CLOUDID));
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND CLOUDTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND CLOUDTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            string sql = "SELECT CLOUDID, CLOUDTIME, CLOUDNAME, CLOUDFILENAME"
                + sb.ToString()
                + " order by CLOUDTIME DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取最新数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getTopDT(YJ_SATELLITECLOUD_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(sw.TopCount))//获取最新记录个数
                sw.TopCount = "10";//默认10条
            sb.AppendFormat(" SELECT top {0}  CLOUDID, CLOUDTIME, CLOUDNAME, CLOUDFILENAME,CLOUDORIGIONNAME", sw.TopCount);
            sb.AppendFormat(" FROM      YJ_SATELLITECLOUD");
            string sql = sb.ToString()
                + " order by CLOUDTIME  desc,CLOUDID DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
