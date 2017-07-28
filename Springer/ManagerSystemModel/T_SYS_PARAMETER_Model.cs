using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 系统参数Model
    /// </summary>
    public class T_SYS_PARAMETER_Model
    {
        /// <summary>
        /// 参数序号
        /// </summary>
        public string PARAMID { get; set; }
        /// <summary>
        /// 参数标识符
        /// </summary>
        public string PARAMFLAG { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string PARAMNAME{get;set;}
        /// <summary>
        /// 参数值
        /// </summary>
        public string PARAMVALUE { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMETHOD { get; set; }
        /// <summary>
        /// 参数备注
        /// </summary>
        public string PARAMMARK { get; set; }
        /// <summary>
        /// 返回Url
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
    }
}
