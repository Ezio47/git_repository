using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统日志查询
    /// </summary>
    public class T_SYS_LOG_SW
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public string LOGID { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string LOGTYPE { get; set; }
        /// <summary>
        /// 操作标题
        /// </summary>
        public string OPERATION { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OPERATIONCONTENT { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string LOGINUSERID { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string USERIP { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OPERATETIME { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string TIMEBegin { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string TIMEEnd { get; set; }
    }
}
