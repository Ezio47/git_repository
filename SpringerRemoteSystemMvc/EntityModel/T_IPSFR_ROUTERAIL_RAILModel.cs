using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// 出围栏
    /// </summary>
    public class T_IPSFR_ROUTERAIL_RAILModel
    {
        /// <summary>
        /// 护林员id
        /// </summary>
        public int HID { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal LONGITUDE { get; set; }

        /// <summary>
        ///纬度
        /// </summary>
        public decimal LATITUDE { get; set; }

        /// <summary>
        /// 上报时间
        /// </summary>
        public DateTime SBTIME { get; set; }
    }
}
