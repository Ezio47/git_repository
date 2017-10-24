using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 野生植物本地化
    /// </summary>
   public class WILD_LOCALBOTANY_SW
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 序号
        /// </summary>

        public string WILD_LOCALBOTANYID { get; set; }
        /// <summary>
        /// 野生植物编码
        /// </summary>     
        public string BIOLOGICALTYPECODE { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
        /// <summary>
        /// 本机构码
        /// </summary>
        public string ORGNO { get; set; }
       /// <summary>
       ///是否显示所有
       /// </summary>
        public string isShowAll { get; set; }   
    }
}
