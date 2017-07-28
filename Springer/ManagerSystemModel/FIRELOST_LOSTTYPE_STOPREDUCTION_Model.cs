using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_损失分类_停减产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_STOPREDUCTION_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string STOPREDUCTIONID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 停减产名称
        /// </summary>
        public string STOPREDUCTIONNAME { get; set; }
        /// <summary>
        /// 停减产类别编号
        /// </summary>
        public string STOPREDUCTIONCODE { get; set; }
        /// <summary>
        /// 停减产类别名称
        /// </summary>
        public string STOPREDUCTIONCODENAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 停减产数量
        /// </summary>
        public string STOPREDUCTIONCOUNT { get; set; }
        /// <summary>
        /// 停减产天数
        /// </summary>
        public string STOPREDUCTIONTIME { get; set; }
        /// <summary>
        /// 停减产价格
        /// </summary>
        public string STOPREDUCTIONPRICE { get; set; }
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
