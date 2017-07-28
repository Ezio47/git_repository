using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物查询模型
    /// </summary>
    public class T_SYS_PEST_SW
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string PESTCODE { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string PESTNAME { get; set; }
        /// <summary>
        /// 拉丁名称
        /// </summary>
        public string LATINNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 获取最上级编码
        /// </summary>
        public bool IsGetTopCode { get; set; }
        /// <summary>
        /// 获取所有下级编码
        /// </summary>
        public bool GetAllChileCode { get; set; }
        /// <summary>
        /// 下级编码长度
        /// </summary>
        public int ChildCODELength { get; set; }
        /// <summary>
        /// 是否现在所有
        /// </summary>
        public bool IsShowALL { get; set; }
    }
}
