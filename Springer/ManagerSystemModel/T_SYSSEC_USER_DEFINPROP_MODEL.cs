using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 系统用户_自定义属性表
    /// </summary>
  public  class T_SYSSEC_USER_DEFINPROP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string T_SYSSEC_USER_DEFINPROPID { get; set; }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DICTVALUE { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PROPVALUE { get; set; }
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
