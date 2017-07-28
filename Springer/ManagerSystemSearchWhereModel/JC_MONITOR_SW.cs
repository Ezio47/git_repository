using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 监测_电子监控基本信息表
    /// </summary>
    public class JC_MONITOR_BASICINFO_SW
    {
        /// <summary>
        /// 监控序号
        /// </summary>
        public string EMID { get; set; }
        /// <summary>
        /// 塔台编码
        /// </summary>
        public string TTBH { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
    }
    /// <summary>
    /// 监测_电子监控表
    /// </summary>
    public class JC_MONITOR_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string IMBID { get; set; }
        /// <summary>
        /// 塔台编码
        /// </summary>
        public string TTBH { get; set; }
        /// <summary>
        /// 所属单位编号
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 是否处理
        /// </summary>
        public string MANSTATE { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
    }
}
