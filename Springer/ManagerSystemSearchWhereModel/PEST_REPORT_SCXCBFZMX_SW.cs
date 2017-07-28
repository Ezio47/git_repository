using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_报表_松材线虫病防治明细表
    /// </summary>
    public class PEST_REPORT_SCXCBFZMX_SW
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
    }
}