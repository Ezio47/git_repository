using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物与树种对应表
    /// </summary>
    public class PEST_TREESPECIES_PEST_SW
    {
        /// <summary>
        /// 树种编码
        /// </summary>
        public string TREESPECIESCODE { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTBYCODE { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }       
    }
}
