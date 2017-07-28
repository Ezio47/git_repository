using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class MapShowModel
    {
        public string JCFID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 发生区域
        /// </summary>
        public string AREA { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }

        /// <summary>
        /// 反馈状态
        /// </summary>
        public string FKSTATE { get; set; }
    }
}