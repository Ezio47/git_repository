using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 数据中心_队伍
    /// </summary>
    public class DC_ARMY_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_ARMY_ID { get; set; }
        /// <summary>
        /// 队伍类型
        /// </summary>
        public string ARMYTYPE { get; set; }
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
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 顶级单位编码，用于获取该单位下所有的信息
        /// </summary>
        public string TopORGNO { get; set; }
        /// <summary>
        /// 单独取市县镇
        /// </summary>
        public string ORGNOSXZ { get; set; }
    }
}
