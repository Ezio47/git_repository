using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 收件
    /// </summary>
    public class E_RECEIVE_SW
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
        ///  主题
        /// </summary>
        public string EMAILTITLE { get; set; }
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
