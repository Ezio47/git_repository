using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 本地化动物
    /// </summary>
    public class WILD_LOCALANIMAL_SW
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 序号
        /// </summary>

        public string WILD_LOCALANIMALID { get; set; }
        /// <summary>
        /// 有害生物编码
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
        /// 是否显示所有
        /// </summary>
        public string isShowAll { get; set; }
    }
}
