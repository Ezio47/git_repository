using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_其他扑救费(P6)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P6_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string P6ID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 其他扑救费名称
        /// </summary>
        public string P6NAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
