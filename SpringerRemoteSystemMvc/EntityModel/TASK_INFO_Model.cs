using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// 任务管理_任务信息
    /// </summary>
    public class TASK_INFO_Model
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
        /// 组织机构名称
        /// </summary>
        public string ORGNAME { get; set; }

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
        /// 护林员姓名
        /// </summary>
        public string HLYNAMELIST { get; set; }

        /// <summary>
        /// 护林员id集合
        /// </summary>
        public string HIDLIST { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }

        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
