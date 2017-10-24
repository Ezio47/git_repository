using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_本地化树种信息表
    /// </summary>
    public class PEST_LOCALTREESPECIES_SW
    {
        /// <summary>
        /// 本地树种序号
        /// </summary>
        public string PEST_LOCALTREESPECIESID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 树种编码
        /// </summary>
        public string TSPCODE { get; set; }
        /// <summary>
        /// 本地树种面积
        /// </summary>
        public string TSPAREA { get; set; }
        /// <summary>
        /// 是否显示所有
        /// </summary>
        public string IsShowAll { get; set; }
        /// <summary>
        /// 当前树种编码
        /// </summary>
        public string CurTSPCODE { get; set; }
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
