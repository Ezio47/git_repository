using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 行政区划Model
    /// </summary>
    public class T_ALL_AREA_Model
    {
        /// <summary>
        /// 行政区划码
        /// </summary>
        public string AREACODE { get; set;}
        /// <summary>
        /// 行政区划名
        /// </summary>
        public string AREANAME { get; set;}
        /// <summary>
        /// 行政区划ID
        /// </summary>
        public string AREAID { get; set; }
        /// <summary>
        /// 行政区划简称
        /// </summary>
        public string AREAJC { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string returnUrl { get; set; }
        
    }
}
