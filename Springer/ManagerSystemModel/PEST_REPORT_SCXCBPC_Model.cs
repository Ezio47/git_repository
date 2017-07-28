using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_松材线虫病普查表
    /// </summary>
    public class PEST_REPORT_SCXCBPC_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_SCXCBPCID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 普查年份
        /// </summary>
        public string SCXCBPCYEAR { get; set; }
        /// <summary>
        /// 普查季节编码
        /// </summary>
        public string SCXCBPCSEASONCODE { get; set; }
        /// <summary>
        /// 普查类型编码
        /// </summary>
        public string SCXCBPCTYPECODE { get; set; }
        /// <summary>
        /// 普查值
        /// </summary>
        public string SCXCBPCVALUE { get; set; }
        /// <summary>
        /// 上级机构编码
        /// </summary>
        public string TopORGNO { get; set; }
    }
}
