using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManagerSystemModel
{
    /// <summary>
    /// 火情档案_队伍表
    /// </summary>
    public class FIRERECORD_ARMY_Model
    {
        /// <summary>
        /// 火情档案_队伍表序号
        /// </summary>
        public string FIRERECORD_ARMYID { get; set; }
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
