using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 预警_天气情况
    /// </summary>
    public class Weather_Model
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string TOWNNAME { get; set; }

        /// <summary>
        /// 天气时间
        /// </summary>
        public string WEATHERDATE { get; set; }

        /// <summary>
        ///雨量
        /// </summary>
        public string P { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public string TCUR { get; set; }

        /// <summary>
        /// 最高温度
        /// </summary>
        public string THIGH { get; set; }


        /// <summary>
        /// 最低温度
        /// </summary>
        public string TLOWER { get; set; }

        /// <summary>
        /// 组织结构代码
        /// </summary>
        public string BYORGNO { get; set; }


    }
}
