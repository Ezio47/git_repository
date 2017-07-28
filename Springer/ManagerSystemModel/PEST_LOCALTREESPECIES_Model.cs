using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_本地化树种信息表
    /// </summary>
    public class PEST_LOCALTREESPECIES_Model
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
    }
}
