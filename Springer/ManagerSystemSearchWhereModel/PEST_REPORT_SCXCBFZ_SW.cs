using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_报表_松材线虫病防治表
    /// </summary>
    public class PEST_REPORT_SCXCBFZ_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_SCXCBFZID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 防治年份
        /// </summary>
        public string SCXCBFZYEAR { get; set; }
        /// <summary>
        /// 发生面积
        /// </summary>
        public string SCXCBFZAREA { get; set; }
        /// <summary>
        /// 计划防治面积
        /// </summary>
        public string SCXCBFZPLANAREA { get; set; }
        /// <summary>
        /// 已防治面积
        /// </summary>
        public string SCXCBFZFINISHAREA { get; set; }
    }
}
