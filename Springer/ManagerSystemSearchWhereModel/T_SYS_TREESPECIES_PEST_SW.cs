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
    public class T_SYS_TREESPECIES_PEST_SW
    {
        /// <summary>
        /// 树种编码
        /// </summary>
        public string TSPCODE { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTCODE { get; set; }
    }
}
