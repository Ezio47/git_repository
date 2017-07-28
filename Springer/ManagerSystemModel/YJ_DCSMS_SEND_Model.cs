using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 
    /// </summary>
    public class YJ_DCSMS_SEND_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string YJ_DCSMS_SENDID { get; set; }
        /// <summary>
        /// 模板序号
        /// </summary>
        public string YJ_DCSMS_TMPID { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TMPCONTENT { get; set; }

        /// <summary>
        /// 接收人员序号列表
        /// </summary>
        public string SMSSENDUSERLIST { get; set; }

        /// <summary>
        /// 等级时间
        /// </summary>
        public string DCDATE { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 短信发送状态
        /// </summary>
        public string SMSSENDSTATUS { get; set; }
    }
}
