using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.Common
{
    public class Message
    {
        /// <summary>
        /// 数据操作返回消息
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public Message(bool success, string msg, string url)
        {
            this.Msg = msg;
            this.Success = success;
            this.Url = url;
        }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 数据操作成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回的Url
        /// </summary>
        public string Url { get; set; }
    }
}
