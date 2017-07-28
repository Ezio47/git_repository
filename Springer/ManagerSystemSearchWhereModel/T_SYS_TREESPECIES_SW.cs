using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 古树名木查询模型
    /// </summary>
    public class T_SYS_TREESPECIES_SW
    {
        /// <summary>
        /// 获取最上级编码
        /// </summary>
        public bool IsGetTopCode { get; set; }
        /// <summary>
        /// 下级编码长度
        /// </summary>
        public int ChildCODELength { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string TSPCODE { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string TSPNAME { get; set; }
        /// <summary>
        /// 拉丁名称
        /// </summary>
        public string LATINNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
    }
}
