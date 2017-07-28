using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    /// <summary>
    /// 火险等级个数
    /// </summary>
    public class FireLevelCountModel
    {
        /// <summary>
        /// 等级时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 1以及以上
        /// </summary>
        public string CountA { get; set; }
        /// <summary>
        /// 2以及以上
        /// </summary>
        public string CountB { get; set; }
        /// <summary>
        /// 3以及以上
        /// </summary>
        public string CountC { get; set; }
        /// <summary>
        /// 4以及以上
        /// </summary>
        public string CountD { get; set; }

        /// <summary>
        /// 5以及以上
        /// </summary>
        public string CountE { get; set; }
    }
}