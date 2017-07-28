using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_消防器材消耗表(P4)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P4_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string P4ID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 消防器材消耗名称
        /// </summary>
        public string P4NAME { get; set; }
        /// <summary>
        /// 消防器材消耗类别编号
        /// </summary>
        public string P4CODE { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 现行价格
        /// </summary>
        public string NOWPRICE { get; set; }
        /// <summary>
        /// 消防器材消耗数量
        /// </summary>
        public string P4COUNT { get; set; }
        /// <summary>
        /// 消防器材消耗单位
        /// </summary>
        public string P4UNIT { get; set; }
        /// <summary>
        /// 年平均折旧率
        /// </summary>
        public string YEARAVGDEPRECIATIONRATE { get; set; }
        /// <summary>
        /// 已使用年限
        /// </summary>
        public string HAVEUSEYEAR { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
