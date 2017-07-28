using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_检疫表
    /// </summary>
    public class PEST_REPORT_QUARANTINE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_QUARANTINEID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 发生年份
        /// </summary>
        public string HAPPENYEAR { get; set; }
        /// <summary>
        /// 检疫类别编码
        /// </summary>
        public string QUARANTINETYPECODE { get; set; }
        /// <summary>
        /// 检疫值
        /// </summary>
        public string QUARANTINEVALUE { get; set; }
        /// <summary>
        /// 上级机构编码
        /// </summary>
        public string TopORGNO { get; set; }
    }
}
