using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 字典类别Model
    /// </summary>
    public class T_SYS_DICTTYPE_Model
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
        /// 字典类别名称
        /// </summary>
        public string DICTTYPENAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 是否允许修改
        /// </summary>
        public string ISMAN { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 子字典类别ListModel
        /// </summary>
        public List<T_SYS_DICTTYPE_Model> DICTTYPEListModel { get; set; }
        /// <summary>
        /// 字典ListModel
        /// </summary>
        public List<T_SYS_DICTModel> DICTListModel { get; set; }
    }
    /// <summary>
    /// 字典Model
    /// </summary>
    public class T_SYS_DICTModel
    {
        /// <summary>
        /// 字典序号
        /// </summary>
        public string DICTID { get; set; }
        /// <summary>
        /// 所属字典序号
        /// </summary>
        public string DICTTYPEID { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DICTFLAG { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DICTNAME { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DICTVALUE { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 备用字段一
        /// </summary>
        public string STANDBY1 { get; set; }
        /// <summary>
        /// 备用字段二
        /// </summary>
        public string STANDBY2 { get; set; }
        /// <summary>
        /// 备用字段三
        /// </summary>
        public string STANDBY3 { get; set; }
        /// <summary>
        /// 备用字段四
        /// </summary>
        public string STANDBY4 { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
    }
}