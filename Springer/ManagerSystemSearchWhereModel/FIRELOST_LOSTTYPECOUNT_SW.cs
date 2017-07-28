using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类统计表
    /// </summary>
    public class FIRELOST_LOSTTYPECOUNT_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPECOUNTID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 灾损分类编码
        /// </summary>
        public string FIRELOSETYPECODE { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
