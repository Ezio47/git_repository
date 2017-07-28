using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 角色权限关联
    /// </summary>
    public class T_SYSSEC_ROLE_RIGHT_Model
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string ROLEID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public string RIGHTID { get; set; }
    }
}
