using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 收件
    /// </summary>
    public class E_RECEIVE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ERID { get; set; }
        /// <summary>
        /// 所属邮件序号
        /// </summary>
        public string BYEMAILID { get; set; }
        /// <summary>
        /// 接收人类别
        /// </summary>
        public string RECEIVETYPE { get; set; }
        /// <summary>
        /// 接收人序号
        /// </summary>
        public string RECEIVEUSERID { get; set; }
        /// <summary>
        /// 接收状态
        /// </summary>
        public string EMAILRECEIVESTATUS { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string EMAILSENDTIME { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public string EMAILRECEIVETIME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 邮件主题信息
        /// </summary>
        public E_SUBJECT_Model SubjectModel { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        public IEnumerable< E_FILE_Model> FileModel { get; set; }

    }
}
