using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class WeatherInfoModel
    {
        /// <summary>
        /// 地域名称
        /// </summary>
        public string AreaName{get;set;}
        /// <summary>
        /// 雨量
        /// </summary>
        public string Hum { get; set; }

        /// <summary>
        /// 当前温度
        /// </summary>
        public string TCur { get; set; }
       
        /// <summary>
        /// 最高温度
        /// </summary>
        public string Thigh { get; set; }

        /// <summary>
        /// 最低温度
        /// </summary>
        public string Tlower { get; set; }

        /// <summary>
        /// 最高温度、最低温度
        /// </summary>
        public string HighAndLow { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string WeatherDate { get; set; }
    }
}