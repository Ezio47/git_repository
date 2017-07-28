using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心_队伍_装备表
    /// </summary>
     public class DC_ARMY_EQUIP_Model
    {
         /// <summary>
        /// 序号
         /// </summary>
         public string DC_ARMY_EQUIP_ID { get; set; }
         /// <summary>
         /// 队伍编号
         /// </summary>
         public string DC_ARMY_ID { get; set; }
         /// <summary>
         /// 装备名称
         /// </summary>
         public string EQUIPNAME { get; set; }
         /// <summary>
         /// 编号
         /// </summary>
         public string EQUIPNUM { get; set; }
         /// <summary>
         /// 型号
         /// </summary>
         public string EQUIPMODEL { get; set; }
         /// <summary>
         /// 使用现状类型
         /// </summary>
         public string EQUIPUSESTATE { get; set; }
         /// <summary>
         /// 数量
         /// </summary>
         public string EQUIPSUM { get; set; }
         /// <summary>
         /// 操作方法
         /// </summary>
         public string opMethod { get; set; }
         /// <summary>
         /// 返回网址
         /// </summary>
         public string returnUrl { get; set; }
         /// <summary>
         /// 使用现状名
         /// </summary>
         public string EQUIPUSESTATEName { get; set; }

    }
}
