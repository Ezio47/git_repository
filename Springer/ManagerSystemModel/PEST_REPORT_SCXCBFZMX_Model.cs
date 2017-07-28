using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_报表_松材线虫病防治明细表
    /// </summary>
    public class PEST_REPORT_SCXCBFZMX_Model
    {
        /// <summary>
        /// 序号ID
        /// </summary>
        public string PEST_REPORT_SCXCBFZMXID { get; set; }
        /// <summary>
        /// 防治表ID
        /// </summary>
        public string PEST_REPORT_SCXCBFZID { get; set; }
        /// <summary>
        ///数据字典类型序号
        /// </summary>
        public string SCXCBFZMXTYPEID { get; set; }
        /// <summary>
        /// 数据字典类型值
        /// </summary>
        public string SCXCBFZMXTYPEVALUE { get; set; }
        /// <summary>
        /// 防治字符字段
        /// </summary>
        public string SCXCBFZMXVARCHAR { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 防治年份
        /// </summary>
        public string SCXCBFZYEAR { get; set; }
    }
}
