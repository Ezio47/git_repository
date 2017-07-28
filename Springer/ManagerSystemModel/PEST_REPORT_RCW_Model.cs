using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_人财物表
    /// </summary>
    public class PEST_REPORT_RCW_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_RCWID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string RCWCODE { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string RCWYEAR { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 人财物值
        /// </summary>
        public string RCWVALUE { get; set; }
    }
}
