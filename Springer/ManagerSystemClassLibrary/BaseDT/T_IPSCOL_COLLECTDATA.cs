using DataBaseClassLibrary;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 数据采集公共类
    /// </summary>
    public class T_IPSCOL_COLLECTDATA
    {
        /// <summary>
        /// 获取采集数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>返回DataTable</returns>
        public static DataTable getUnionDT(CollectDataSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT  *  FROM  T_IPSCOL_COLLECTDATA  a  Left join T_IPSCOL_DATADETAIL  b On  a.COLLECTID=b.COLLECTID"
            + " Left join T_IPSCOL_DATAUPLOAD c  On c.COLLECTID=b.COLLECTID");
            sb.AppendFormat(" WHERE  1=1");

            sb.AppendFormat(" AND a.HID = {0}", sw.HID);

            sb.AppendFormat(" ORDER BY a.COLLECTTIME desc");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
