using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 值班班次表
    /// </summary>
    public class OD_CLASS
    {

        /// <summary>
        /// 不带参数的数据获取
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDT(OD_CLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select ONDUTYCLASSID,ONDUTYCLASSNAME,ONDUTYBEGINTIME,ONDUTYENDTIME from OD_CLASS");
            sb.AppendFormat(" Where 1=1");
            if (string.IsNullOrEmpty(sw.ONDUTYCLASSID)==false)
                sb.AppendFormat(" AND ONDUTYCLASSID='{0}'", sw.ONDUTYCLASSID);
            DataSet ds=DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
