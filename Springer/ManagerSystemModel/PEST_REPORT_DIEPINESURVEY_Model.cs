using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_枯死松树调查表
    /// </summary>
    public class PEST_REPORT_DIEPINESURVEY_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_DIEPINESURVEYID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string BYORGNONAME { get; set; }
        /// <summary>
        /// 发现人
        /// </summary>
        public string FINDER { get; set; }
        /// <summary>
        /// 发现日期
        /// </summary>
        public string FINDDATE { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LINKTELL { get; set; }
        /// <summary>
        /// 死亡株数
        /// </summary>
        public string DIEPINECOUNT { get; set; }
        /// <summary>
        /// 报告日期
        /// </summary>
        public string REPORTDATE { get; set; }
        /// <summary>
        /// 取样株数
        /// </summary>
        public string SAMPLINGCOUNT { get; set; }
        /// <summary>
        /// 鉴定结果
        /// </summary>
        public string AUTHENTICATERESULT { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
