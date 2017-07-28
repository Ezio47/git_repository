using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 邮件主题
    /// </summary>
    public class E_SUBJECT_SW
    {
        /// <summary>
        /// 邮件序号
        /// </summary>
        public string EMAILID { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string EMAILTITLE { get; set; }
        /// <summary>
        /// 邮件状态
        /// </summary>
        public string EMAILSTATUS { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string EMAILSENDUSERID { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string EMAILTIME { get; set; }
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string EMAILCONTENT { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 接收人序号 用于收邮件 
        /// </summary>
        public string RECEIVEUSERID { get; set; }
        /// <summary>
        /// 获取收件人类型 0 收件人 1抄送人 2密送人
        /// </summary>

        public string getReceiveType { get; set; }
    }
}
