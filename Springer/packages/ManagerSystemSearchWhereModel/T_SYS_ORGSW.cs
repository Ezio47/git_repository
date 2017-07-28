using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    public class T_SYS_ORGSW
    {
        public string ORGNO { get; set; }
        public string ORGNAME { get; set; }
        public string ORGDUTY { get; set; }
        public string LEADER { get; set; }
        public string AREACODE { get; set; }
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 顶部编码，不为空代表获取此编码及属于此编码的记录
        /// </summary>
        public string TopORGNO { get; set; }
        /// <summary>
        /// 当前编码，用于下拉框匹配默认选中项
        /// </summary>
        public string CurORGNO { get; set; }
    }
}
