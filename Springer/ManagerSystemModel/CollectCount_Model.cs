using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    ///  Collect统计模型
    /// </summary>
   public class CollectCount_Model
    {
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 建筑物个数统计
        /// </summary>
        public string CollectType1Count { get; set; }
        /// <summary>
        /// 消防设施个数统计
        /// </summary>
        public string CollectType2Count { get; set; }
        /// <summary>
        ///道路个数统计
        /// </summary>
        public string CollectType3Count { get; set; }
        /// <summary>
        /// 可燃物载量个数统计
        /// </summary>
        public string CollectType4Count { get; set; }
    }
}
