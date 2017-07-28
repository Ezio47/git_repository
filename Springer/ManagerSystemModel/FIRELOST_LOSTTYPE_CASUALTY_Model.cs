using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_人员伤亡损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_CASUALTY_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_CASUALTYID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 伤亡类别编号
        /// </summary>
        public string CASUALTYCODE { get; set; }
        /// <summary>
        /// 伤亡类别编号名称
        /// </summary>
        public string CASUALTYCODENAME { get; set; }
        /// <summary>
        /// 伤亡名称
        /// </summary>
        public string CASUALTYNAME { get; set; }
        /// <summary>
        /// 伤亡人数
        /// </summary>
        public string CASUALTYNUMBERS { get; set; }
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
