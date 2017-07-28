using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 数据中心_设施_营房
    /// </summary>
    public class DC_UTILITY_CAMP_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_UTILITY_CAMP_ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 结构类型
        /// </summary>
        public string STRUCTURETYPE { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string ORGNOS { get; set; }
        /// <summary>
        /// 最高组织机构码
        /// </summary>
        public string TopORGNO { get; set; }
        /// <summary>
        /// 价值
        /// </summary>
        public string WORTH { get; set; }
        /// <summary>
        /// 单独取市县镇
        /// </summary>
        public string ORGNOSXZ { get; set; }
    }
}
