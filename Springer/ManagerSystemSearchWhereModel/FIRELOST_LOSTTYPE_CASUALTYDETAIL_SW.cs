using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_人员伤亡损失明细表
    /// </summary>
    public class FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string CASUALTYDETAILID { get; set; }
        /// <summary>
        /// 人员伤亡损失序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_CASUALTYID { get; set; }
        /// <summary>
        /// 人员伤亡损失明细编号
        /// </summary>
        public string CASUALTYDETAILCODE { get; set; }
        /// <summary>
        /// 人员伤亡损失明细费用
        /// </summary>
        public string CASUALTYDETAIMONEY { get; set; }
    }
}
