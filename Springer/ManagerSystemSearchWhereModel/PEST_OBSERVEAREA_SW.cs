using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 应施面积
    /// </summary>
    public class PEST_OBSERVEAREA_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string T_SYS_PEST_OBSERVEAREAID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 应施面积
        /// </summary>
        public string OBSERVEAREA { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string OBSERVEYEAR { get; set; }
    }
}
