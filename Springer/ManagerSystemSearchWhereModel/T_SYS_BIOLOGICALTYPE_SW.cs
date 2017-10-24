using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 公用_生物分类表
    /// </summary>
    public class T_SYS_BIOLOGICALTYPE_SW
    {
        /// <summary>
        /// 分类编码
        /// </summary>
        public string BIOLOCODE { get; set; }
        /// <summary>
        /// 当前分类编码
        /// </summary>
        public string CurCODE { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string BIOLONAME { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string BIOLOENNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 是否只获取下级编码
        /// </summary>
        public bool IsOnlyGetChild { get; set; }
        /// <summary>
        /// 是否只获取科级(包含自己)以下的编码
        /// </summary>
        public bool IsOnlyGetKe { get; set; }
        /// <summary>
        /// 是否只获取种级(包含自己)以下的编码
        /// </summary>
        public bool IsOnlyGetZhong { get; set; }
    }
}
