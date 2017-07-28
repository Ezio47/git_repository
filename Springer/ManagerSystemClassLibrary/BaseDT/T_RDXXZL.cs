using DataBaseClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 热点火情图片
    /// </summary>
    public class T_RDXXZL
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(string fjbh)
        {
            string sql = "SELECT  *  FROM  RDXXZL WHERE FJBH='" + fjbh + "'";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
    }
}
