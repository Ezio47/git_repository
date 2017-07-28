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
    public class OD_CLASS_Model
    {
        /// <summary>
        /// 值班班次
        /// </summary>
        public string ONDUTYCLASSID { get; set; }
        /// <summary>
        /// 班次名称
        /// </summary>
        public string ONDUTYCLASSNAME { get; set; }
        /// <summary>
        /// 值班开始时间
        /// </summary>
        public string ONDUTYBEGINTIME { get; set; }
        /// <summary>
        /// 值班结束时间
        /// </summary>
        public string ONDUTYENDTIME { get; set; }
    }
}
