using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    public class T_SYS_LOG_SW
    {
        public string LOGID { get; set; }
        public string LOGTYPE { get; set; }
        public string OPERATION { get; set; }
        public string OPERATIONCONTENT { get; set; }
        public string LOGINUSERID { get; set; }
        public string USERIP { get; set; }
        public string OPERATETIME { get; set; }
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
