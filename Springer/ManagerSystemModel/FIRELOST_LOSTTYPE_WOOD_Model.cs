using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_木材损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_WOOD_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_WOODID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 木材名称
        /// </summary>
        public string WOODNAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 过火木材材积
        /// </summary>
        public string LOSEVOLUME { get; set; }
        /// <summary>
        /// 市场价格
        /// </summary>
        public string MARKETPRICE { get; set; }
        /// <summary>
        /// 残值
        /// </summary>
        public string RESIDUALVALUE { get; set; }
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
