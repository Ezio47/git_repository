using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 热点
    /// </summary>
    public class T_IPS_HOTS_SW
    {
        /// <summary>
        /// 热点ID
        /// </summary>
        public string HOTSID { get; set; }
        /// <summary>
        /// 反馈结果 是否处理
        /// </summary>
        public string MANSTATE { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public string FXSJ { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
    }
}
