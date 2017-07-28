using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 所有角色及某用户是否拥有该角色
    /// </summary>
    public class T_SYSSEC_ROLE_USER_Model
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string ROLENAME { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string ROLEID { get; set; }
        /// <summary>
        /// 是否拥有权限 0未拥有1
        /// </summary>
        public string isCheck { get; set; }
    }
    /// <summary>
    /// 角色Model
    /// </summary>
    public class T_SYSSEC_ROLE_Model
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string ROLEID { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string ROLENAME { get; set; }
        /// <summary>
        /// 角色备注
        /// </summary>
        public string ROLENOTE { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 角色级别
        /// </summary>
        public string ROLELEVEL { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string USERID { get; set; }
        /// <summary>
        /// 该角色对应拥有的权限ID列表，用于角色赋值给权限，即角色管理
        /// </summary>
        public string rightIDList { get; set; }
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
