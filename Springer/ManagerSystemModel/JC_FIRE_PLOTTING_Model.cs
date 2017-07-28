using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 监测_火情标绘表
    /// </summary>
    public class JC_FIRE_PLOTTING_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string JC_FIRE_PLOTTINGID { get; set; }
        /// <summary>
        /// 火灾序号
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 标绘时间
        /// </summary>
        public string PLOTTINGTIME { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string PLOTTINGTITLE { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string PLOTTINGFILENAME { get; set; }
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