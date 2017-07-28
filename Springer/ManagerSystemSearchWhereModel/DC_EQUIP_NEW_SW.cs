using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
   /// <summary>
    /// 数据中心_装备_新
   /// </summary>
   public class DC_EQUIP_NEW_SW
   {
       /// <summary>
       /// 序号
       /// </summary>
       public string DC_EQUIP_NEW_ID { get; set; }
       /// <summary>
       /// 装备类型
       /// </summary>
       public string EQUIPTYPE { get; set; }
       /// <summary>
       /// 编号
       /// </summary>
       public string NUMBER { get; set; }
       /// <summary>
       /// 名称
       /// </summary>
       public string NAME { get; set; }
       /// <summary>
       /// 所属机构编码
       /// </summary>
       public string BYORGNO { get; set; }
       /// <summary>
       /// 型号
       /// </summary>
       public string MODEL { get; set; }
       /// <summary>
       /// 使用现状类型
       /// </summary>
       public string USESTATE { get; set; }
       /// <summary>
       /// 每页行数
       /// </summary>
       public int pageSize { get; set; }
       /// <summary>
       /// 当前页数
       /// </summary>
       public int curPage { get; set; }
       /// <summary>
       /// 最高级别组织机构
       /// </summary>
       public string TopORGNO { get; set; }
       /// <summary>
       /// 价值
       /// </summary>
       public string WORTH { get; set; }
       /// <summary>
       /// 数量
       /// </summary>
       public string EQUIPAMOUNT { get; set; }
       /// <summary>
       /// 单独取市县镇
       /// </summary>
       public string ORGNOSXZ { get; set; }
       /// <summary>
       /// 所属仓库序号
       /// </summary>
       public string REPID { get; set; }
       /// <summary>
       /// 装备单位
       /// </summary>
       public string DCSUPPROPUNIT { get; set; }
       /// <summary>
       ///单价
       /// </summary>
       public string PRICE { get; set; }

    }
}
