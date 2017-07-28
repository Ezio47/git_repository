using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 采集详情单
    /// </summary>
    public class CollectDataListModel
    {
        /// <summary>
        ///采集ID
        /// </summary>
        public long COLLECTID { get; set; }

        /// <summary>
        /// 护林员ID
        /// </summary>
        public int HID { get; set; }

        /// <summary>
        /// 采集类型
        /// </summary>
        public string SYSTYPEVALUE { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string ADDRESS { get; set; }

        /// <summary>
        /// 采集名称
        /// </summary>
        public string COLLECTNAME { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime COLLECTTIME { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal LONGITUDE { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal LATITUDE { get; set; }

    }

}
