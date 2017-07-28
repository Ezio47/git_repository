using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TLW.Project.SmsMessageWcfService.Model
{
    /// <summary>
    /// 短信返回消息
    /// </summary>
    [DataContract]
    public class SmsMessage
    {
        /// <summary>
        /// 数据操作返回消息
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param> 
        public SmsMessage(bool success, string msg)
        {
            this.Msg = msg;
            this.Success = success;
        }
        /// <summary>
        /// 返回的信息
        /// </summary>
        [DataMember]
        public string Msg { get; set; }

        /// <summary>
        /// 数据操作成功
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

    }
}
