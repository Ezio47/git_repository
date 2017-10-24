using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class EM_Message_SW
    {
        /// <summary>
        /// 短信模板ID
        /// </summary>
        public string EM_MESSAGEID { get; set; }
        /// <summary>
        /// 发送人员
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 短信的内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 短信的人员
        /// </summary>
        public string MessageName { get; set; }
        /// <summary>
        /// 短信的主题
        /// </summary>
        public string MessageTitle { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 页数总数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
