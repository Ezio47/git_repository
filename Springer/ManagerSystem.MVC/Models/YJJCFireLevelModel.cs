using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    /// <summary>
    /// 火险等级
    /// </summary>
    public class YJJCFireLevelModel
    {
        /// <summary>
        /// 区域
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 火险等级
        /// </summary>
        public string FireLevel { get; set; }
        /// <summary>
        /// 火险等级时间
        /// </summary>
        public string LevelDate{ get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }

        /// <summary>
        /// 上级单位(市县)
        /// </summary>
        public string OWnAreaName { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string SourceForm { get; set; }

        /// <summary>
        /// 短信状态
        /// </summary>
        public string MessageStatus { get; set; }
    }
}