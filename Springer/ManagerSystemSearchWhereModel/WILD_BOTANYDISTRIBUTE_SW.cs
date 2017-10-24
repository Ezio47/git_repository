using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 野生植物分布
    /// </summary>
  public  class WILD_BOTANYDISTRIBUTE_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
      public string WILD_BOTANYDISTRIBUTEID { get; set; }
      /// <summary>
      /// 组织机构
      /// </summary>
      public string BYORGNO { get; set; }
      /// <summary>
      /// 机构名称
      /// </summary>
      public string BYORGNOName { get; set; }
      /// <summary>
      /// 本机构
      /// </summary>
      public string ORGNO { get; set; }
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
        /// 植物株数
        /// </summary>
        public string BOTANYCOUNT { get; set; }
        /// <summary>
        /// 植物面积
        /// </summary>
        public string BOTANYAREA { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
