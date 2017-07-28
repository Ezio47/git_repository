using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 营房图形展示Echarts
    /// </summary>
    public class CampCount_Model
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
        /// 钢构个数统计
        /// </summary>
        public string CampType1Count { get; set; }
        /// <summary>
        /// 砖混个数统计
        /// </summary>
        public string CampType2Count { get; set; }
        /// <summary>
        /// 钢混个数统计
        /// </summary>
        public string CampType3Count { get; set; }
        
    }
}
