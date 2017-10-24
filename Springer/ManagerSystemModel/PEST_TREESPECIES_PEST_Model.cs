using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物与树种对应表
    /// </summary>
    public class PEST_TREESPECIES_PEST_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_TREESPECIES_PESTID { get; set; }        
        /// <summary>
        /// 树种编码
        /// </summary>
        public string TREESPECIESCODE { get; set; }
        /// <summary>
        /// 树种名称
        /// </summary>
        public string TREESPECIESNAME { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTBYCODE { get; set; }
        /// <summary>
        /// 有害生物名称
        /// </summary>
        public string PESTBYNAME { get; set; }
        /// <summary>
        /// 有害生物科级编码
        /// </summary>
        public string PESTKECODE { get; set; }
        /// <summary>
        /// 有害生物科级名称
        /// </summary>
        public string PESTKENAME { get; set; }
        /// <summary>
        /// 有害生物属级编码
        /// </summary>
        public string PESTSHUCODE { get; set; }
        /// <summary>
        /// 有害生物属级名称
        /// </summary>
        public string PESTSHUNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
