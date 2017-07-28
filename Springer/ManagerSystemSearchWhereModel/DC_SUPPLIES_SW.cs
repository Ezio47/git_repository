using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据中心_物资表
    /// </summary>
    public class DC_SUPPLIES_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DCSUPPLIESID { get; set; }
        /// <summary>
        /// 所属物资序号
        /// </summary>
        public string SUPID { get; set; }
        /// <summary>
        /// 物资库存数量
        /// </summary>
        public string DCSUPCOUNT { get; set; }
        /// <summary>
        /// 所属仓库编号
        /// </summary>
        public string REPID { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public string curPage { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public string pageSize { get; set; }
        /// <summary>
        /// 展示改仓库下所有物资
        /// </summary>
        public string isShowAll { get; set; }
    }
}
