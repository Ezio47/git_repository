using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 护林员实时数据上报
    /// </summary>
    public class T_IPS_REALDATASW
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 查询日期
        /// </summary>
        public string searchDate { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }

        /// <summary>
        /// 地图类型
        /// </summary>
        public string MapType { get; set; }
    }
}
