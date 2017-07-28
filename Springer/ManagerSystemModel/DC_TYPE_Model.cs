using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心-类别表
    /// </summary>
    public class DC_TYPE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DCTYPEID { get; set; }
        /// <summary>
        /// 父序号
        /// </summary>
        public string DCTYPETOPID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string DCTYPENAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 类别标志
        /// </summary>
        public string DCTYPEFLAG { get; set; }
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
