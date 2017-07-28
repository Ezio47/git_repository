using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_固定资产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_FIXEDASSETS_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_FIXEDASSETSID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 固定资产名称
        /// </summary>
        public string FIXEDASSETSNAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 重置价值
        /// </summary>
        public string RESETVALUE { get; set; }
        /// <summary>
        /// 年平均折旧率
        /// </summary>
        public string YEARAVGDEPRECIATIONRATE { get; set; }
        /// <summary>
        /// 已使用年限
        /// </summary>
        public string HAVEUSEYEAR { get; set; }
        /// <summary>
        /// 烧毁率
        /// </summary>
        public string BURNRATE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
