using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 出围统计
    /// </summary>
    public class OutRaiLCount_Model
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 出围数量
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// 各日期列表,逗号分隔
        /// </summary>
        public string DayCountList { get; set; }

    }
    /// <summary>
    /// 出围详单
    /// </summary>
    public class OutRaiLDetail_Model
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 护林员序号
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 护林员姓名
        /// </summary>
        public string HNAME { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 正常
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Y { get; set; }

    }
    /// <summary>
    /// 怠工Model
    /// </summary>
    public class SabotageDetail_Model
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 护林员序号
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 护林员姓名
        /// </summary>
        public string HNAME { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
       
        /// <summary>
        /// 正常
        /// </summary>
        public string Count0 { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Count1 { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public double CountPer { get; set; }


    }
    /// <summary>
    /// 怠工统计
    /// </summary>
    public class getSabotageCount_Model
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 合计
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// 正常
        /// </summary>
        public string Count0 { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Count1 { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public string CountPer { get; set; }
        /// <summary>
        /// 各日期列表,逗号分隔
        /// </summary>
        public string DayCountList { get; set; }
    }


    /// <summary>
    /// 漏检统计详单
    /// </summary>
    public class HUCheck_OmitDetail_Model
    {
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 护林员姓名
        /// </summary>
        public string HNAME { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string ROUTEDATE { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string ROUTESTATE { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string LONGITUDE { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string LATITUDE { get; set; }
    }
    /// <summary>
    /// 漏检统计
    /// </summary>
    public class HUCheck_OmitCount_Model
    {
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 护林员人数
        /// </summary>
        public string HUCount { get; set; }
        /// <summary>
        /// 应巡总数
        /// </summary>
        public string OmitCount0 { get; set; }
        /// <summary>
        /// 已巡数量
        /// </summary>
        public string OmitCount1 { get; set; }
        /// <summary>
        /// 漏巡数量
        /// </summary>
        public string OmitCount2 { get; set; }
        /// <summary>
        /// 完成率
        /// </summary>
        public string OmitCount3 { get; set; }
        /// <summary>
        /// 日期列表
        /// </summary>
        public string DayCountList { get; set; }
    }
    /// <summary>
    /// 考勤统计
    /// </summary>
    public class HUCheck_CheckInCount_Model
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 机构名或护林员姓名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 考勤人数
        /// </summary>
        public string HUCount { get; set; }
        /// <summary>
        /// 各日期列表,逗号分隔
        /// </summary>
        public string DayCountList { get; set; }
        /// <summary>
        /// 总出勤天数
        /// </summary>
        public string daysC { get; set; }
        /// <summary>
        /// 出勤天数
        /// </summary>
        public string daysOK { get; set; }
        /// <summary>
        /// 出勤率
        /// </summary>
        public string daysPer { get; set; }
    }
}
