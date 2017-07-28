using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 日志Model
    /// </summary>
    public class T_SYS_LOG_Model
    {
        /// <summary>
        /// 日志序号
        /// </summary>
        public string LOGID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string LOGTYPE { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string OPERATION { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string OPERATIONCONTENT { get; set; }
        /// <summary>
        /// 操作标题
        /// </summary>
        public string LOGINUSERID { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string USERIP { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OPERATETIME { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 日志用户名
        /// </summary>
        public string LOGINUSERName { get; set; }
        /// <summary>
        ///  类别名称	
        /// </summary>
        public string LOGTYPENAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
