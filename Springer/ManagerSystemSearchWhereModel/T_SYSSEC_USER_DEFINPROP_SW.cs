using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统用户_自定义属性表
    /// </summary>
   public class T_SYSSEC_USER_DEFINPROP_SW
    {
       /// <summary>
       /// 序号
       /// </summary>
        public string T_SYSSEC_USER_DEFINPROPID { get; set; }
       /// <summary>
        /// 所属用户ID
       /// </summary>
        public string UID { get; set; }
       /// <summary>
        /// 字典值
       /// </summary>
        public string DICTVALUE { get; set; }
       /// <summary>
        /// 属性值
       /// </summary>
        public string PROPVALUE { get; set; }
    }
}
