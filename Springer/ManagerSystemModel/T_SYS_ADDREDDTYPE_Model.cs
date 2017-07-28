using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 通讯录类别管理
    /// </summary>
    public class T_SYS_ADDREDDTYPE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ATID { get; set; }
        /// <summary>
        /// 父序号
        /// </summary>
        public string RATID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string RTNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
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
