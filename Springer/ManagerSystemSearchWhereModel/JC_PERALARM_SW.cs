using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 监测_群众报警表
    /// </summary>
    public class JC_PERALARM_SW
    {
        /// <summary>
        /// 报警序号
        /// </summary>
        public string PERALARMID { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string PERALARMTIME { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PERALARMPHONE { get; set; }
        /// <summary>
        /// 报警人
        /// </summary>
        public string PERALARMNAME { get; set; }
        /// <summary>
        /// 是否处理 0未处理 1已处理
        /// </summary>
        public string MANSTATE { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
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
    }
}
