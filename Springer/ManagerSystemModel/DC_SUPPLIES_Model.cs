using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心_物资表
    /// </summary>
     public class DC_SUPPLIES_Model
    {
         /// <summary>
        /// 序号
         /// </summary>
         public string DCSUPPLIESID { get; set; }
         /// <summary>
         /// 所属物资序号
         /// </summary>
         public string SUPID { get; set; }
         /// <summary>
         /// 所属仓库序号
         /// </summary>
         public string REPID { get; set; }
         /// <summary>
         /// 物资名称
         /// </summary>
         public string SUPNAME { get; set; }
         /// <summary>
         /// 物资库存数量
         /// </summary>
         public string DCSUPCOUNT { get; set; }
         /// <summary>
         /// 方法
         /// </summary>
         public string opMethod { get; set; }
         /// <summary>
         /// 返回网址
         /// </summary>
         public string returnUrl{ get; set; }
         /// <summary>
         /// 单价
         /// </summary>
         public string PRICE { get; set; }
         /// <summary>
         /// 物资类型
         /// </summary>
         public string EQUIPTYPEName { get; set; }
    }
}
