using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_非木质林产品损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_NTFP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_NTFPID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 非木质林产品名称
        /// </summary>
        public string NTFPNAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 损失数量
        /// </summary>
        public string LOSECOUNT { get; set; }
        /// <summary>
        /// 市场平均价格
        /// </summary>
        public string MARKETAVGPRICE { get; set; }
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
