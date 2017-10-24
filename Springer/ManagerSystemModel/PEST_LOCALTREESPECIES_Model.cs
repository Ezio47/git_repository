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
        /// 所属机构名称
        /// </summary>
        public string ORGNONAME { get; set; }
        /// <summary>
        /// 树种编码
        /// </summary>
        public string TSPCODE { get; set; }
        /// <summary>
        /// 树种名称
        /// </summary>
        public string TSPNAME { get; set; }     
        /// <summary>
        /// 本地树种面积
        /// </summary>
        public string TSPAREA { get; set; }

        /// <summary>
        /// 树种科级编码
        /// </summary>
        public string TSPKECODE { get; set; }
        /// <summary>
        /// 树种科级名称
        /// </summary>
        public string TSPKENAME { get; set; }

        /// <summary>
        /// 树种属级编码
        /// </summary>
        public string TSPSHUCODE { get; set; }
        /// <summary>
        /// 树种属级名称
        /// </summary>
        public string TSPSHUNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
