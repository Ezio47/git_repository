using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_火灾扑救费用_工资伙食费(P3)
    /// </summary>
    public class FIRELOST_LOSTTYPE_ATTACK_P3_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string P3ID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 工资伙食费名称
        /// </summary>
        public string P3NAME { get; set; }
        /// <summary>
        /// 工资伙食费类别编号
        /// </summary>
        public string P3CODE { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 工资伙食费标准
        /// </summary>
        public string P3MONEY { get; set; }
        /// <summary>
        /// 扑救人数
        /// </summary>
        public string ATTACKNUMBERS { get; set; }
        /// <summary>
        /// 扑救天数
        /// </summary>
        public string ATTACKDAYS { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
