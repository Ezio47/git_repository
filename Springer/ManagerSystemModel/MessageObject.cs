using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 信息对象Model
    /// </summary>
    public class MessageObject
    {
        /// <summary>
        /// 信息对象
        /// </summary>
        /// <param name="success"></param>
        /// <param name="obj"></param>
        public MessageObject(bool success, object obj)
        {
            this.Success = success;
            this.Data = obj;
        }
        /// <summary>
        /// 成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
