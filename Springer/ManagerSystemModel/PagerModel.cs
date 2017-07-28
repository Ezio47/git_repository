using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 每页记录大小
    /// </summary>
    public class PagerSizeModel
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 显示值
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 是否选中状态
        /// </summary>
        public string selected { get; set; }
    }
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagerModel
    {
        /// <summary>
        /// Li样式
        /// </summary>
        public string liClass { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string aHref { get; set; }
        /// <summary>
        /// i样式
        /// </summary>
        public string iClass { get; set; }
        /// <summary>
        /// 显示值
        /// </summary>
        public string vlaue { get; set; }
    }
}
