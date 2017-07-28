using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 主题表模型
    /// </summary>
    public class E_SUBJECT_Model
    {
        /// <summary>
        /// 邮件序号
        /// </summary>
        public string EMAILID { get; set;}
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
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string EMAILRECUSERLIST { get; set; }
        /// <summary>
        /// 收件人1 草稿箱
        /// </summary>
        public string EMAILRECUSERLIST1{ get; set; }
        /// <summary>
        /// 抄送人1 草稿箱
        /// </summary>
        public string EMAILCOPYUSERLIST1 { get; set; }
        /// <summary>
        /// 抄送人
        /// </summary>
        public string EMAILCOPYUSERLIST { get; set; }
        /// <summary>
        /// 密送人1 草稿箱
        /// </summary>
        public string EMAILSECRETUSERLIST1 { get; set; }
        /// <summary>
        /// 密送人
        /// </summary>
        public string EMAILSECRETUSERLIST { get; set; }

        /// <summary>
        /// 收件人中文名
        /// </summary>
        public string EMAILRECUSERNameLIST { get; set; }

        /// <summary>
        /// 抄送人中文名
        /// </summary>
        public string EMAILCOPYUSERNameLIST { get; set; }
        /// <summary>
        /// 密送人中文名
        /// </summary>
        public string EMAILSECRETUSERNameLIST { get; set; }
        /// <summary>
        /// 发送人中文名
        /// </summary>
        public string EMAILSENDUSERName { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        public IEnumerable<E_FILE_Model> FileModel { get; set; }
    }
}
