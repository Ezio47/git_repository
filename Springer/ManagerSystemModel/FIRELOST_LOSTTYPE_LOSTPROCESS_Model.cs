using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_灾后处理费用表
    /// </summary>
    public class FIRELOST_LOSTTYPE_LOSTPROCESS_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string LOSTPROCESSID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 灾后处理名称
        /// </summary>
        public string LOSTPROCESSNAME { get; set; }
        /// <summary>
        /// 灾后处理类别序号
        /// </summary>
        public string LOSTPROCESSCODE { get; set; }
        /// <summary>
        /// 灾后处理类别名称
        /// </summary>
        public string LOSTPROCESSCODENAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
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
