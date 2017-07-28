using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 预警_卫星云图表
    /// </summary>
    public class YJ_SATELLITECLOUD_SW
    {
        /// <summary>
        /// 云图编号
        /// </summary>
        public string CLOUDID { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
        /// <summary>
        /// 获取最新记录个数
        /// </summary>
        public string TopCount { get; set; }
    }
}
