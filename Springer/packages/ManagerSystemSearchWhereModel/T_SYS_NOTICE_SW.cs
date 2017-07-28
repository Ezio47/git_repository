using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    public class T_SYS_NOTICE_SW
    {
        public string INFOID { get; set; }
        public string INFOTITLE { get; set; }
        public string INFOCONTENT { get; set; }
        public string INFOURL { get; set; }
        public string FBTIME { get; set; }
        public string LABLE { get; set; }
        public string NUM { get; set; }
        public string INFOTYPE { get; set; }
        public string INFOUSERID { get; set; }
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
