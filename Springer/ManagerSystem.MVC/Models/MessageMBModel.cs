using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    /// <summary>
    /// 短信发送模板
    /// </summary>
    public class MessageMBModel
    {
        /// <summary>
        /// 各级政府和森林防火指挥部
        /// </summary>
        public string Message1 { get; set; }

        /// <summary>
        /// 各级林业主管部门
        /// </summary>
        public string Message2 { get; set; }
        /// <summary>
        /// 专业森林消防队
        /// </summary>
        public string Message3 { get; set; }
        /// <summary>
        /// 值班员
        /// </summary>
        public string Message4 { get; set; }
        /// <summary>
        /// 护林员
        /// </summary>
        public string Message5 { get; set; }

    }
}