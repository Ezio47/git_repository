using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_燃料材料费(P2)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P2_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string P2ID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 燃料材料费名称
        /// </summary>
        public string P2NAME { get; set; }
        /// <summary>
        /// 燃料材料费类别编号
        /// </summary>
        public string P2CODE { get; set; }
        /// <summary>
        /// 燃料材料费类别编号名称
        /// </summary>
        public string P2CODENAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 消耗数量
        /// </summary>
        public string P2COUNT { get; set; }
        /// <summary>
        /// 燃料材料费单位
        /// </summary>
        public string P2UNIT { get; set; }
        /// <summary>
        /// 现行价格
        /// </summary>
        public string NOWPRICE { get; set; }
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
