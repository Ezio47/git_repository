using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_组织管理费(P5)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P5_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string P5ID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 组织管理费名称
        /// </summary>
        public string P5NAME { get; set; }
        /// <summary>
        /// 组织管理费类别编号
        /// </summary>
        public string P5CODE { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 组织管理费费用
        /// </summary>
        public string P5MONEY { get; set; }
        /// <summary>
        /// 扑救天数
        /// </summary>
        public string ATTACKDAYS { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public string ELSEMONEY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
