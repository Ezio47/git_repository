using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 瞭望台统计图形Echarts
    /// </summary>
   public class OVERWATCHCount_Model
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
        public string OVERWATCHType1Count { get; set; }
        /// <summary>
        /// 砖混个数统计
        /// </summary>
        public string OVERWATCHType2Count { get; set; }
        /// <summary>
        /// 钢混个数统计
        /// </summary>
        public string OVERWATCHType3Count { get; set; }
    }
}
