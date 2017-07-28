using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统字典类别查询
    /// </summary>
    public class T_SYS_DICTTYPE_SW
    {
        /// <summary>
        /// 所属字典序号
        /// </summary>
        public string DICTTYPEID { get; set; }
        /// <summary>
        /// 字典父序号
        /// </summary>
        public string DICTTYPERID { get; set; }
        /// <summary>
        /// 所属字典名称
        /// </summary>
        public string DICTTYPENAME { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 是否允许修改
        /// </summary>
        public string ISMAN { get; set; }
        /// <summary>
        /// 是否在顶端加所有选项 1是 其他否
        /// </summary>
        public string isShowAll { get; set; }
    }
    /// <summary>
    /// 系统字典查询
    /// </summary>
    public class T_SYS_DICTSW
    {
        /// <summary>
        /// 字典类别名称
        /// </summary>
        public string DICTTYPENAME { get; set; }
        /// <summary>
        /// 字典序号
        /// </summary>
        public string DICTID { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DICTFLAG { get; set; }
        /// <summary>
        /// 所属字典序号
        /// </summary>
        public string DICTTYPEID { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DICTVALUE { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 是否在顶端加所有选项 1是 其他否
        /// </summary>
        public string isShowAll { get; set; }

        /// <summary>
        /// 备用字段1 1 表是州部门 2 表示市县部门 3 表示乡镇部门
        /// </summary>
        public string STANDBY1 { get; set; }
        /// <summary>
        /// 是否添加备注1到显示text 1显示
        /// </summary>
        public string STANDBY1InName { get; set; }
    }
}
