using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统管理-任务信息
    /// </summary>
    public class TASK_INFO_SW
    {
        /// <summary>
        /// 任务序号
        /// </summary>
        public string TASK_INFOID { get; set; }

        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TASKTITLE { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TASKTYPE { get; set; }

        /// <summary>
        /// 任务级别
        /// </summary>
        public string TASKLEVEL { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public string TASKSTAUTS { get; set; }

        /// <summary>
        /// 任务发起时间
        /// </summary>
        public string TASKSTARTTIME { get; set; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        public string TASKBEGINTIME { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public string TASKENDTIME { get; set; }

        /// <summary>
        /// 任务内容
        /// </summary>
        public string TASKCONTENT { get; set; }

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
