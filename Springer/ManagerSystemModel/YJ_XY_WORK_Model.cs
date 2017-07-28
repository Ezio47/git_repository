using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 预警响应相关工作表
    /// </summary>
    public class YJ_XY_WORK_Model
    {
        /// <summary>
        /// ID
        /// </summary>
        public string YJXYID { get; set; }
        /// <summary>
        /// 火险等级
        /// </summary>
        public string DANGERCLASS { get; set; }
        /// <summary>
        /// 响应部门
        /// </summary>
        public string YJXYDEPT { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string YJXYCONTENT { get; set; }
        /// <summary>
        /// 火险等级名称
        /// </summary>
        public string DANGERCLASSName { get; set; }
        /// <summary>
        /// 响应部门名称
        /// </summary>
        public string YJXYDEPTName { get; set; }
        /// <summary>
        /// 火灾预警等级模型
        /// </summary>
        public T_SYS_DICTModel dicModel { get; set; }
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