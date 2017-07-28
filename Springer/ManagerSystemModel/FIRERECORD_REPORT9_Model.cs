using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManagerSystemModel
{    
    /// <summary>
    /// 森林防火办事机构人员统计
    /// </summary>
    public class FIRERECORD_REPORT9_Model
   {
        /// <summary>
       /// 森林防火办事机构人员统计年报表序号
        /// </summary>
        public string FIRERECORD_REPORT9ID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 报表年份
        /// </summary>
        public string REPORTYEAR { get; set; }
        /// <summary>
        /// 报表类别编号
        /// </summary>
        public string REPORTCODE { get; set; }
        /// <summary>
        /// 省地市级别编码
        /// </summary>
        public string SSXTYPELEVELCODE { get; set; }
        /// <summary>
        /// 报表类别值
        /// </summary>
        public string REPORTVALUE { get; set; }
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
