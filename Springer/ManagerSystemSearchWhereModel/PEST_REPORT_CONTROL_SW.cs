using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_报表_防治表
    /// </summary>
    public class PEST_REPORT_CONTROL_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_CONTROLID { get; set; }
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
        /// 防治方法编码
        /// </summary>
        public string CONTROLMETHODCODE { get; set; }    
        /// <summary>
        /// 防治面积
        /// </summary>
        public string CONTROLAREA { get; set; }
    }
}
