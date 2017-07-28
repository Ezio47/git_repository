using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 数据中心_车辆
    /// </summary>
    public class DC_CAR_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_CAR_ID { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CARTYPE { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 号牌
        /// </summary>
        public string PLATENUM { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 最高组织机构码
        /// </summary>
        public string TopORGNO { get; set; }
        /// <summary>
        /// 单独取市县镇
        /// </summary>
        public string ORGNOSXZ { get; set; }
    }
}
