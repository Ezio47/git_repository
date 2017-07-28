using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 火灾级别预案表
    /// </summary>
    public class JC_FIRE_PLAN_Model
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
        /// 预案内容
        /// </summary>
        public string PLANCONTENT { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string PLANFILENAME { get; set; }

        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string BYORGNOName { get; set; }
        /// <summary>
        /// 火灾级别名称
        /// </summary>
        public string FIRELEVELName { get; set; }

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