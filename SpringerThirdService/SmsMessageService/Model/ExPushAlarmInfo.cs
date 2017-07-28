using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW.Project.SmsMessageWcfService.Model
{
    /// <summary>
    /// 火情告警信息类
    /// </summary>
    public class ExPushAlarmInfo
    {
        //设备ID
        public string deviceid { get; set; }
        //告警ID
        public string alarmid { get; set; }
        //水平角
        public string hor { get; set; }
        //俯仰角
        public string pit { get; set; }
        //视场角
        public string view { get; set; }
        //经度
        public string longtitue { get; set; }
        //纬度
        public string latitue { get; set; }
        //高程
        public string elevation { get; set; }
    }
}
