using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 数据中心_队伍_人员表
    /// </summary>
    public class DC_ARMY_MEMBER_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_ARMY_MEMBER_ID { get; set; }
        /// <summary>
        /// 所属队伍序号
        /// </summary>
        public string DC_ARMY_ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string MEMBERNAME { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string SEX { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
