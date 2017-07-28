using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_车马船交通费(P1)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P1_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string P1ID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 车马船名称
        /// </summary>
        public string P1NAME { get; set; }
        /// <summary>
        /// 车马船类别编号
        /// </summary>
        public string P1CODE { get; set; }
        /// <summary>
        /// 车马船类别编号名称
        /// </summary>
        public string P1CODENAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 车马船数量
        /// </summary>
        public string P1COUNT { get; set; }
        /// <summary>
        /// 车马船单位
        /// </summary>
        public string P1UNIT { get; set; }
        /// <summary>
        /// 车马船费用
        /// </summary>
        public string P1PRICE { get; set; }
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
