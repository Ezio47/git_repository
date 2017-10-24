using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 本地化动物模型
    /// </summary>
   public class WILD_LOCALANIMAL_Model
    {
        ///<summary>
        /// 序号
        /// </summary>
       public string WILD_LOCALANIMALID { get; set; }
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
        /// 属名称
        /// </summary>
        public string PESTSHUNAME { get; set; }
        /// <summary>
        /// 本地动物编码
        /// </summary>
        public string BIOLOGICALTYPECODE { get; set; }
        /// <summary>
        ///  本地动物名称
        /// </summary>
        public string BIOLOGICALTYPECODENAME { get; set; }
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
