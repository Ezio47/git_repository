using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_流动资产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_CURRENTASSETS_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_CURRENTASSETSID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 流动资产名称
        /// </summary>
        public string CURRENTASSETSNAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 流动资产数量
        /// </summary>
        public string CURRENTASSETSCOUNT { get; set; }
        /// <summary>
        /// 流动资产单位
        /// </summary>
        public string CURRENTASSETSUNIT { get; set; }
        /// <summary>
        /// 流动资产购入价格
        /// </summary>
        public string CURRENTASSETSPRICE { get; set; }
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
