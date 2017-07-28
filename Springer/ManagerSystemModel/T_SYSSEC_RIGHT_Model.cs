using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 获取所有权限、标识某一角色拥有的权限
    /// </summary>
    public class T_SYSSEC_RIGHT_ByRole_Model
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string RIGHTID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string RIGHTNAME { get; set; }
        /// <summary>
        /// 是否拥有该权限
        /// </summary>
        public string isCheck { get; set; }
        /// <summary>
        /// 子权限
        /// </summary>
        public IEnumerable<T_SYSSEC_RIGHT_ByRole_Model> subModel { get; set; }
    }
    /// <summary>
    /// 系统权限Model
    /// </summary>
    public class T_SYSSEC_RIGHT_Model
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string RIGHTID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string RIGHTNAME { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
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
