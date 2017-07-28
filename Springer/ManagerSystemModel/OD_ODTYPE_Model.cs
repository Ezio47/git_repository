using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 值班_排班类别表
    /// </summary>
    public class OD_ODTYPE_Model
    {
        /// <summary>
        /// ID
        /// </summary>
        public string OD_TYPEID { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 名称标题
        /// </summary>
        public string OD_TYPENAME { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string OD_DATEBEGIN { get; set;}
        /// <summary>
        /// 结束日期
        /// </summary>
        public string OD_DATEEND { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string op_Method { get; set; }
    }
}
