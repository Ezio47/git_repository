using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class HotInfoModel
    {

        /// <summary>
        /// 热点类型
        /// </summary>
        public string HotType { get; set; }

        /// <summary>
        /// 热点类型名称
        /// </summary>
        public string HotName { get; set; }

        /// <summary>
        /// 热点个数
        /// </summary>
        public string HotSum { get; set; }

        /// <summary>
        /// 签收个数
        /// </summary>
        public string QSSum { get; set; }

        /// <summary>
        /// 未签收
        /// </summary>
        public string WQSSum { get; set; }

        /// <summary>
        /// 已反馈（已审核）
        /// </summary>
        public string FKSum { get; set; }
        /// <summary>
        /// 未反馈（未审核）
        /// </summary>
        public string WFKSum { get; set; }




    }
}