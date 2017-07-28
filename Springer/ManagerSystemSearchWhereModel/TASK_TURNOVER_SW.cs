using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统管理_任务流转
    /// </summary>
    public class TASK_TURNOVER_SW
    {
        /// <summary>
        /// 任务流转序号
        /// </summary>
        public string TASK_TURNOVERID { get; set; }

        /// <summary>
        /// 任务序号
        /// </summary>
        public string TASK_INFOID { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OPUID { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string OPTIME { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public string OPSTATUS { get; set; }

        /// <summary>
        /// 操作标题
        /// </summary>
        public string OPTITLE { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string OPIP { get; set; }
    }
}
