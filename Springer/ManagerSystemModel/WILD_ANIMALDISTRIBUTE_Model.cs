using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 野生动物分布模型
    /// </summary>
   public class WILD_ANIMALDISTRIBUTE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
       public string WILD_ANIMALDISTRIBUTEID { get; set; }
       /// <summary>
       /// 生物物种编码
       /// </summary>
       public string BIOLOGICALTYPECODE { get; set; }
       /// <summary>
       /// 物种名称
       /// </summary>
       public string BIOLOGICALTYPEName { get; set; }
       /// <summary>
       /// 种群类型编码
       /// </summary>
       public string POPULATIONTYPE { get; set; }
       /// <summary>
       /// 种群名称
       /// </summary>
       public string POPULATIONTYPEName { get; set; }
       /// <summary>
       /// 经度
       /// </summary>
       public string JD { get; set; }
       /// <summary>
       /// 纬度
       /// </summary>
       public string WD { get; set; }
       /// <summary>
       /// 经纬度集合
       /// </summary>
       public string JWDLIST { get; set; }
       /// <summary>
       /// 动物数量
       /// </summary>
       public string ANIMALCOUNT { get; set; }
       /// <summary>
       /// 备注
       /// </summary>
       public string MARK { get; set; }
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
