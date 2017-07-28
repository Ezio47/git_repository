using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心_出入库明细表
    /// </summary>
     public class DC_DETAILS_Model
    {
         /// <summary>
        /// 序号
         /// </summary>
         public string DCDETAILSID { get; set; }
         /// <summary>
         /// 所属物资序号
         /// </summary>
         public string SUPID { get; set; }
         /// <summary>
         /// 所属仓库序号
         /// </summary>
         public string REPID { get; set; }
         /// <summary>
         /// 出入库时间
         /// </summary>
         public string DCREPTIME { get; set; }
         /// <summary>
         /// 出入库标志
         /// </summary>
         public string DCREPFLAG { get; set; }
         /// <summary>
         /// 出入库数量
         /// </summary>
         public string DCREPSUPCOUNT { get; set; }
         /// <summary>
         /// 录入人员
         /// </summary>
         public string DCENTYMANID { get; set; }
         /// <summary>
         /// 领用人
         /// </summary>
         public string DCUSERID { get; set; }
         /// <summary>
         /// 保管人
         /// </summary>
         public string DCCUSTODIANID { get; set; }
         /// <summary>
         /// 方法
         /// </summary>
         public string opMethod { get; set; }
         /// <summary>
         /// 返回地址
         /// </summary>
         public string returnUrl { get; set; }
         /// <summary>
         /// 物资名称
         /// </summary>
         public string SUPNAME { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
         public string DPNAME { get; set; }
         /// <summary>
         /// 物资和id的组合
         /// </summary>
         public string CountID { get; set; }
         /// <summary>
         /// 价格
         /// </summary>
         public string PRICE { get; set; }
         /// <summary>
         /// 备注
         /// </summary>
         public string MARK { get; set; }
         /// <summary>
         /// 领用人单位
         /// </summary>
         public string DCUSERORG { get; set; }
         /// <summary>
         /// 录入人名
         /// </summary>
         public string DCENTYMANNAME { get; set; }
         /// <summary>
         /// 领用人
         /// </summary>
         public string DCUSERNAME { get; set; }
         /// <summary>
         /// 保管人
         /// </summary>
         public string DCCUSTODIANNAME { get; set; }
         /// <summary>
         /// 金额
         /// </summary>
         public string SUM { get; set; }
         /// <summary>
         /// 物资剩余数量
         /// </summary>
         public string REPERTORYCOUNT { get; set; }
         /// <summary>
         ///物资型号
         /// </summary>
         public string DCSUPPROPMODEL { get; set; }
         /// <summary>
         /// 物资单位
         /// </summary>
         public string DCSUPPROPUNIT { get; set; }
         /// <summary>
         /// 物资的厂家
         /// </summary>
         public string DCSUPPROPFACTORY { get; set; }
         /// <summary>
         /// 仓库负责人
         /// </summary>
         public string RESPONSIBLEMAN { get; set; }
         /// <summary>
         /// 制单人
         /// </summary>
         public string DCZHIBIAOREN { get; set; }
         /// <summary>
         /// 发放人
         /// </summary>
         public string DCFAFANGREN { get; set; }
         /// <summary>
         /// 编号
         /// </summary>
         public string NUMBER { get; set; }
         
    }
}
