using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 巡查路线
    /// </summary>
    public class PatrolRouteStat_SW
    {
        /// <summary>
        /// 护林员和电话号码
        /// </summary>
        public string PhoneHname { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string orgNo { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
        /// <summary>
        /// 顶级单位
        /// </summary>
        public string TopORGNO { get; set; }
    }
}
