using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 值班班次
    /// </summary>
    public class DUTY_CLASS_Model
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 值班班次
        /// </summary>
        public string DUTYCLASSID { get; set; }

        /// <summary>
        /// 值班名称
        /// </summary>
        public string DUTYCLASSNAME { get; set; }

        /// <summary>
        /// 值班开始时间
        /// </summary>
        public string DUTYBEGINTIME { get; set; }

        /// <summary>
        /// 值班结束时间
        /// </summary>
        public string DUTYENDTIME { get; set; }

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
