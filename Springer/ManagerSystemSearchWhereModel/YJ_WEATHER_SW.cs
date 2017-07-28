using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 预警_气象信息表
    /// </summary>
    public class YJ_WEATHER_SW
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 当前机构编码（需查询所属该编码下属单位）
        /// </summary>
        public string TopORGNO { get; set; }

        /// <summary>
        /// 气象日期
        /// </summary>
        public string WEATHERDATE { get; set; }
        
    }
}
