using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 护林员统计表
    /// </summary>
    public class HUReport_HUCount_Model
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
        /// 护林员统计
        /// </summary>
       public string HUCount { get; set; }
        /// <summary>
        /// 性别男统计
        /// </summary>
       public string Sex0Count { get; set; }
        /// <summary>
        /// 性别女统计
        /// </summary>
       public string Sex1Count { get; set; }
        /// <summary>
        /// 固职统计
        /// </summary>
       public string Onstate0Count { get; set; }
        /// <summary>
        /// 兼职统计
        /// </summary>
       public string Onstate1Count { get; set; }
    }
    /// <summary>
    /// 巡检路线统计总表
    /// </summary>
    public class HUReport_PatrolRouteStat_Model
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 路线总数
        /// </summary>
        public string LineCount { get; set; }
        /// <summary>
        /// 已巡
        /// </summary>
        public string LineCount0 { get; set; }
        /// <summary>
        /// 未巡
        /// </summary>
        public string LineCount1 { get; set; }
        /// <summary>
        /// 巡检率
        /// </summary>
        public string LineCount2 { get; set; }
        /// <summary>
        /// 巡检点总数
        /// </summary>
        public string PointCount { get; set; }
        /// <summary>
        /// 已巡
        /// </summary>
        public string PointCount0 { get; set; }
        /// <summary>
        /// 未巡
        /// </summary>
        public string PointCount1 { get; set; }
        /// <summary>
        /// 巡检率
        /// </summary>
        public string PointCount2 { get; set; }
    }
    /// <summary>
    /// 巡检路线明细表
    /// </summary>
    public class HUReport_PatrolRouteDetail_Model
    {
        /// <summary>
        /// 行政编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 行政区划名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 护林员名称
        /// </summary>
        public string HNAME { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 路线总数
        /// </summary>
        public string LineCount { get; set; }
        /// <summary>
        /// 已巡
        /// </summary>
        public string LineCount0 { get; set; }
        /// <summary>
        /// 未巡
        /// </summary>
        public string LineCount1 { get; set; }
        /// <summary>
        /// 巡检率
        /// </summary>
        public string LineCount2 { get; set; }
        /// <summary>
        /// 巡检点总数
        /// </summary>
        public string PointCount { get; set; }
        /// <summary>
        /// 已巡
        /// </summary>
        public string PointCount0 { get; set; }
        /// <summary>
        /// 未巡
        /// </summary>
        public string PointCount1 { get; set; }
        /// <summary>
        /// 巡检率
        /// </summary>
        public string PointCount2 { get; set; }
    }
}
