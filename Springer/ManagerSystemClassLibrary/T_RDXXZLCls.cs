using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 热点自动采集操作类
    /// </summary>
    public class T_RDXXZLCls
    {
       /// <summary>
       /// 获取热点图片信息
       /// </summary>
       /// <param name="fjbh"></param>
       /// <returns>参见模型</returns>
        public static T_RDXXZLModel getModel(string fjbh)
        {
            var model = new T_RDXXZLModel();
            var dt = BaseDT.T_RDXXZL.getDT(fjbh);
            if (dt != null)
            {
                model.LX = dt.Rows[0]["LX"].ToString();
                model.CRSJ = Convert.ToDateTime(dt.Rows[0]["CRSJ"].ToString());
                model.FJ = (byte[])dt.Rows[0]["FJ"];
                model.FJBH = dt.Rows[0]["FJBH"].ToString();
            }
            return model;
        }

    }
}
