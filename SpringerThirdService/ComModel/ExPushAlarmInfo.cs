using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TLW.AH.ComModel
{

    /// <summary>
    /// 火情告警信息
    /// </summary>
    [DataContract]
    public class ExPushAlarmInfo
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [DataMember]
        public string deviceid { get; set; }
       /// <summary>
        /// 告警ID
        /// </summary>
        [DataMember]
        public string alarmid { get; set; }
        /// <summary>
        /// 水平角
        /// </summary>
        [DataMember]
        public string hor { get; set; }
        /// <summary>
        /// 俯仰角
        /// </summary>
        [DataMember]
        public string pit { get; set; }
        /// <summary>
        ///  视场角
        /// </summary>
        [DataMember]
        public string view { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public string longtitue { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public string latitue { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        [DataMember]
        public string elevation { get; set; }
    }
}
