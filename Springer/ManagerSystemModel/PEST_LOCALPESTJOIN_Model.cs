using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_本地化生物关联表
    /// </summary>
    public class PEST_LOCALPESTJOIN_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_LOCALPESTJOINID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string ORGNONAME { get; set; }
        /// <summary>
        /// 科编码
        /// </summary>
        public string PESTKECODE { get; set; }
        /// <summary>
        /// 科名称
        /// </summary>
        public string PESTKENAME { get; set; }
        /// <summary>
        /// 科编码
        /// </summary>
        public string PESTSHUCODE { get; set; }
        /// <summary>
        /// 科名称
        /// </summary>
        public string PESTSHUNAME { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTBYCODE { get; set; }
        /// <summary>
        /// 有害生物名称
        /// </summary>
        public string PESTBYCODENAME { get; set; }
        /// <summary>
        /// 调查类型编号
        /// </summary>
        public string SEARCHTYPE { get; set; }
        /// <summary>
        /// 调查类型编号编码
        /// </summary>
        public string SEARCHTYPENAME { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回地址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
