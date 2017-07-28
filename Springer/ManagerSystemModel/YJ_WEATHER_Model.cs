using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 预警_气象信息表
    /// </summary>
   public class YJ_WEATHER_Model
    {
       /// <summary>
       /// 气象序号
       /// </summary>
        public string WEATHERID { get; set; }
       /// <summary>
       /// 气象时间
       /// </summary>
        public string WEATHERDATE { get; set; }
       /// <summary>
       /// 所属机构编码
       /// </summary>
        public string BYORGNO { get; set; }
       /// <summary>
       /// 乡镇名称
       /// </summary>
        public string TOWNNAME { get; set; }
       /// <summary>
       /// 经度
       /// </summary>
        public string JD { get; set; }
       /// <summary>
       /// 纬度
       /// </summary>
        public string WD { get; set; }
       /// <summary>
       /// 雨量
       /// </summary>
        public string P { get; set; }
       /// <summary>
        /// 天气
       /// </summary>
        public string T { get; set; }
       /// <summary>
        /// 温度
       /// </summary>
        public string W { get; set; }
       /// <summary>
       /// 风向
       /// </summary>
        public string F { get; set; }
       /// <summary>
       /// 单位名称
       /// </summary>
        public string orgName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }

       /// <summary>
       /// 当前温度
       /// </summary>
        public string TCUR { get; set; }

       /// <summary>
       /// 最高温度
       /// </summary>
        public string THIGH { get; set; }

       /// <summary>
       /// 最低温度
       /// </summary>
        public string TLOWER { get; set; }

        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}