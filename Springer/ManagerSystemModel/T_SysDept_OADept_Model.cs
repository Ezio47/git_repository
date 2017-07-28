using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 防火系统部门与OA部门关系对应表
    /// </summary>
    public class T_SysDept_OADept_Model
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 防火系统组织机构ID
        /// </summary>
        public string SysORGNO { get; set; }
        /// <summary>
        /// 防火系统组织机构ID
        /// </summary>
        public string SysDeptID { get; set; }
        /// <summary>
        /// OA系统部门ID
        /// </summary>
        public string OADeptID { get; set; }
    }
}
