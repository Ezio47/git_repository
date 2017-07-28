using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 任务反馈表
    /// </summary>
    public class TASK_FEEDBACK_SW
    {
        /// <summary>
        /// 任务反馈序号
        /// </summary>
        public string TASK_FEEDBACKID { get; set; }

        /// <summary>
        ///护林员ID
        /// </summary>
        public string HID { get; set; }

        /// <summary>
        /// 任务序号
        /// </summary>
        public string TASK_INFOID { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public string RECEIVETIME { get; set; }

        /// <summary>
        /// 接受时间
        /// </summary>
        public string ACCEPTTIME { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FEEDBACKTIME { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FEEDBACKCONTENT { get; set; }

        /// <summary>
        /// 反馈状态
        /// </summary>
        public string FEEDBACKSTATUS { get; set; }

    }
}
