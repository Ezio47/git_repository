using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    /// <summary>
    /// fla文件模型
    /// </summary>
    public class FlaModel
    {

        public string JC_FIRE_PLOTTINGID { get; set; }
        /// <summary>
        /// 火情id
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 保存标绘时间
        /// </summary>
        public string PLOTTINGTIME { get; set; }
        /// <summary>
        /// fla文件标绘顺序
        /// </summary>
        public string PLOTTINGTITLE { get; set; }
        /// <summary>
        /// fla文件名
        /// </summary>
        public string PLOTTINGFILENAME { get; set; }
    }
}