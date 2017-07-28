using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 火情档案
    /// </summary>
    public class FIRERECORD_REPORT8_SW
    {
        /// <summary>
        /// 森林防火组织机构统计年报表序号
        /// </summary>
        public string FIRERECORD_REPORT8ID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 报表年份
        /// </summary>
        public string REPORTYEAR { get; set; }
        /// <summary>
        /// 报表类别编号
        /// </summary>
        public string REPORTCODE { get; set; }
        /// <summary>
        /// 省地市级别编码
        /// </summary>
        public string SSXTYPELEVELCODE { get; set; }
        /// <summary>
        /// 报表类别值
        /// </summary>
        public string REPORTVALUE { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
    }
}
