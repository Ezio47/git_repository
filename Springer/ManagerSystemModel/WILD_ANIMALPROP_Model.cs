using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 野生动物属性模型
    /// </summary>
   public class WILD_ANIMALPROP_Model
    {
       /// <summary>
       /// 序号
       /// </summary>
       public string WILD_ANIMALPROPID { get; set; }
        /// <summary>
        /// 生物物种编码
        /// </summary>
       public string BIOLOGICALTYPECODE { get; set; }
        /// <summary>
        /// 保护级别
        /// </summary>
       public string PROTECTIONLEVELCODE { get; set; }
        /// <summary>
        /// 生存现状
        /// </summary>
       public string LIVINGSTATUSCODE { get; set; }
       /// <summary>
       /// 生物名称
       /// </summary>
       public string BIOLOGICALTYPEName { get; set; }
       /// <summary>
       /// 保护级别名称
       /// </summary>
       public string PROTECTIONLEVELName { get; set; }
       /// <summary>
       /// 生存现状名称
       /// </summary>
       public string LIVINGSTATUSName { get; set; }
    }
}
