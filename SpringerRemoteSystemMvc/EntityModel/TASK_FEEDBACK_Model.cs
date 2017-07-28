using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// 系统管理_任务反馈
    /// </summary>
    public class TASK_FEEDBACK_Model
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
        ///原有护林员ID
        /// </summary>
        public string OHID { get; set; }

        /// <summary>
        ///新护林员ID
        /// </summary>
        public string NHID { get; set; }

        /// <summary>
        ///护林员姓名
        /// </summary>
        public string HNAME { get; set; }

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
