using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// Echarts车辆统计模型
    /// </summary>
    public class CarCount_Model
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
        /// 车辆个数统计
        /// </summary>
        public string CarTypeCount { get; set; }
        /// <summary>
        /// 指挥车车辆统计
        /// </summary>
        public string CarType1Count { get; set; }
        /// <summary>
        /// 运兵车车辆统计
        /// </summary>
        public string CarType2Count { get; set; }
        /// <summary>
        /// 供水车车辆统计
        /// </summary>
        public string CarType3Count { get; set; }
        /// <summary>
        /// 通讯车车辆统计
        /// </summary>
        public string CarType4Count { get; set; }
        /// <summary>
        /// 宣传车车辆统计
        /// </summary>
        public string CarType5Count { get; set; }
       
    }
}
