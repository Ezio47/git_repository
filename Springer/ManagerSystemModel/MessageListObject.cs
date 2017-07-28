using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
   /// <summary>
   /// 处理信息提醒
   /// </summary>
    public class MessageListObject
    {
        /// <summary>
        /// 数据操作信息对象
        /// </summary>
        /// <param name="success">是否处理成功</param>
        /// <param name="objList">List列表</param>
        public MessageListObject(bool success, IEnumerable<object> objList)
        {
            this.Success = success;
            this.DataList = objList;
        }
        /// <summary>
        /// 是否处理成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 信息列表
        /// </summary>
       
        public IEnumerable<object> DataList { get; set; }
    }
}
