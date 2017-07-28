using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_目标考核表
    /// </summary>
    public class PEST_REPORT_ASSESSINGTARGET_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_ASSESSINGTARGETID { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string RCWYEAR { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 目标考核类别编码
        /// </summary>
        public string ASSESSINGTARGETTYPECODE { get; set; }
        /// <summary>
        /// 目标考核值
        /// </summary>
        public string ASSESSINGTARGETVALUE { get; set; }
        /// <summary>
        /// 上级机构编码
        /// </summary>
        public string TopORGNO { get; set; }
    }
}
