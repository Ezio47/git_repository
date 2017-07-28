using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据中心_出入库明细表
    /// </summary>
     public class DC_DETAILS_SW
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
         /// 当前页
         /// </summary>
        public int curPage { get; set; }
         /// <summary>
         /// 页面大小
         /// </summary>
        public int pageSize { get; set; }
         /// <summary>
         /// 开始时间
         /// </summary>
        public string DateBegin { get; set; }
         /// <summary>
         /// 结束时间
         /// </summary>
        public string DateEnd { get; set; }
         /// <summary>
         /// 仓库名称
         /// </summary>
        public string DPNAME { get; set; }
         /// <summary>
         /// 物资名称
         /// </summary>
        public string SUPNAME { get; set; }
         /// <summary>
         /// 剩余物资数量
         /// </summary>
        public string REPERTORYCOUNT { get; set; }
         /// <summary>
         /// 编号
         /// </summary>
        public string NUMBER { get; set; }
        
    }

}
