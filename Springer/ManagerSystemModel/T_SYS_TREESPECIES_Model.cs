using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 树种表
    /// </summary>
    public class T_SYS_TREESPECIES_Model
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string TSPCODE { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TSPNAME { get; set; }
        /// <summary>
        /// 拉丁名称
        /// </summary>
        public string LATINNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回地址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
