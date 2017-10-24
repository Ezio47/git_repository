using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_属性表
    /// </summary>
    public class PEST_PESTPROP_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_PESTPROPID { get; set; }
        /// <summary>
        /// 生物物种编码
        /// </summary>
        public string BIOLOGICALTYPECODE { get; set; }
        /// <summary>
        /// 检疫性
        /// </summary>
        public string QUARANTINE { get; set; }
        /// <summary>
        /// 危险性
        /// </summary>
        public string RISK { get; set; }
    }
}
