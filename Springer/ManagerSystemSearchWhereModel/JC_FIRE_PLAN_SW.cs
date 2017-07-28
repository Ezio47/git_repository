using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 火灾级别预案表
    /// </summary>
    public class JC_FIRE_PLAN_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string JC_FIRE_PLANID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 火灾级别
        /// </summary>
        public string FIRELEVEL { get; set; }
        /// <summary>
        /// 预案标题
        /// </summary>
        public string PLANTITLE { get; set; }
        /// <summary>
        /// 查询单位编码，如查询某乡镇的，则显示该镇、所属县、所属市的预案 主要用于调度指挥
        /// </summary>

        public string searchOrgNo { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
