using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 野生动物-动态属性表
    /// </summary>
   public class WILD_ANIMALDYNAMICPROP_Model
    {
       /// <summary>
       /// 序号
       /// </summary>
       public string WILD_ANIMALDYNAMICPROPID { get; set; }
        /// <summary>
        /// 生物物种编码
        /// </summary>
       public string BIOLOGICALTYPECODE { get; set; }
        /// <summary>
        /// 属性字典编码
        /// </summary>
       public string DYNAMICPROPCODE { get; set; }
        /// <summary>
        /// 属性内容
        /// </summary>
        public string DYNAMICPROPCONTENT { get; set; }
    }
}
