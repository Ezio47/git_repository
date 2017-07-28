using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_发生表
    /// </summary>
    public class PEST_REPORT_HAPPEN_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_HAPPENID { get; set; }
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
        /// 危害发生面积类别编号
        /// </summary>
        public string HARMTYPEID { get; set; }
        /// <summary>
        /// 危害级别编码
        /// </summary>
        public string HARMLEVELCODE { get; set; }
        /// <summary>
        /// 发生面积
        /// </summary>
        public string HAPPENAREA { get; set; }
        /// <summary>
        /// 上级机构编码
        /// </summary>
        public string TopORGNO { get;set; }
    }
}
