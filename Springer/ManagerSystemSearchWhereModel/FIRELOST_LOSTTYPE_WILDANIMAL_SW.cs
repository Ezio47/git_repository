using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_野生动物损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_WILDANIMAL_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string WILDANIMALID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 野生动物名称
        /// </summary>
        public string WILDANIMALNAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 野生动物损失数量
        /// </summary>
        public string WILDANIMALCOUNT { get; set; }
        /// <summary>
        /// 野生动物价格
        /// </summary>
        public string WILDANIMALPRICE { get; set; }
        /// <summary>
        /// 残值
        /// </summary>
        public string RESIDUALVALUE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
