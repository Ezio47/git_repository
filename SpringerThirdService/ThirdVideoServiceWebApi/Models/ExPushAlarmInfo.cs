using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ThirdVideoServiceWebApi.Models
{
    /// <summary>
    /// 火情告警信息
    /// </summary>
    [DataContract]
    public class ExPushAlarmInfo
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        public string id { get; set; }

        /// <summary>
        /// jsonrpc
        /// </summary>
        [DataMember]
        public string jsonrpc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public AlarmInfoData Params { get; set; }
    }

    /// <summary>
    /// 火情信息
    /// </summary>
    [DataContract]
    public class AlarmInfoData
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [DataMember]
        public string DEVICEID { get; set; }
        /// <summary>
        /// 告警ID
        /// </summary>
        [DataMember]
        public string ALARMID { get; set; }
        /// <summary>
        /// 水平角
        /// </summary>
        [DataMember]
        public string HOR { get; set; }
        /// <summary>
        /// 俯仰角
        /// </summary>
        [DataMember]
        public string PIT { get; set; }
        /// <summary>
        ///  视场角
        /// </summary>
        [DataMember]
        public string VIEW { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public string LONGTITUE { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public string LATITUE { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        [DataMember]
        public string ELEVATION { get; set; }
    }
}