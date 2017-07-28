using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_成灾表
    /// </summary>
    public class PEST_REPORT_HARM_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_HARMID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 发生年份
        /// </summary>
        public string HAPPENYEAR { get; set; }
        /// <summary>
        /// 发生月份
        /// </summary>
        public string HAPPENMONTH { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTBYCODE { get; set; }           
        /// <summary>
        /// 成灾面积
        /// </summary>
        public string DISASTERAREA { get; set; }
        /// <summary>
        /// 预计成灾面积
        /// </summary>
        public string FORECASTDISASTERAREA { get; set; }
        /// <summary>
        /// 死亡株数
        /// </summary>
        public string DIEPLATECOUNT { get; set; }
        /// <summary>
        /// 上级机构编码
        /// </summary>
        public string TopORGNO { get; set; }
    }
}
