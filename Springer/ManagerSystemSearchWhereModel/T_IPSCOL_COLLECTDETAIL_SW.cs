using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据采集详细
    /// </summary>
    public class T_IPSCOL_COLLECTDETAIL_SW
    {
        /// <summary>
        /// 数据采集ID
        /// </summary>
        public string COLLECTID { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string LON { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string LAT { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public string COLLECTTIME { get; set; }
    }
}
