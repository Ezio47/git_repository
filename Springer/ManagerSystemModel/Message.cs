using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// Ajax获取数据及分页返回信息
    /// </summary>
    public class MessagePagerAjax
    {
        /// <summary>
        /// 获取数据及分页返回信息
        /// </summary>
        /// <param name="Success">操作是否成功</param>
        /// <param name="tableInfo">数据列表表格</param>
        /// <param name="pagerInfo">分页信息</param>
        public MessagePagerAjax(bool Success, string tableInfo, string pagerInfo)
        {
            this.Success = Success;
            this.tableInfo = tableInfo;
            this.pagerInfo = pagerInfo;
        }
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 数据列表表格
        /// </summary>
        public string tableInfo { get; set; }
        /// <summary>
        /// 分页信息
        /// </summary>
        public string pagerInfo { get; set; }
    }
    /// <summary>
    /// 数据操作返回消息
    /// </summary>
   public class Message
    {
       /// <summary>
        /// 数据操作返回消息
       /// </summary>
       /// <param name="success"></param>
       /// <param name="msg"></param>
       /// <param name="url"></param>
        public Message(bool success, string msg,string url)
        {
            //Add("Success", success);
            //Add("Msg", msg);
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
