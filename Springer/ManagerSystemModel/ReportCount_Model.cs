using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 上报统计Echarts
    /// </summary>
  public class ReportCount_Model
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
        /// 火情个数统计
        /// </summary>
        public string ReportType1Count { get; set; }
        /// <summary>
        /// 病虫害个数统计
        /// </summary>
        public string ReportType2Count { get; set; }
        /// <summary>
        /// 盗砍盗伐个数统计
        /// </summary>
        public string ReportType3Count { get; set; }
        /// <summary>
        /// 安全隐患个数统计
        /// </summary>
        public string ReportType4Count { get; set; }
    }
}
