using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 排序类别表查询条件
    /// </summary>
    public class OD_ODTYPE_SW
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 是否根据默认排序获取最新一条
        /// </summary>
        public string isTopOne { get; set; }
        ///// <summary>
        ///// ID
        ///// </summary>
        //public string OD_TYPEID { get; set; }
        ///// <summary>
        ///// 名称标题
        ///// </summary>
        //public string OD_TYPENAME { get; set; }
        ///// <summary>
        ///// 开始日期
        ///// </summary>
        //public string OD_DATEBEGIN { get; set; }
        ///// <summary>
        ///// 结束日期
        ///// </summary>
        //public string OD_DATEEND { get; set; }
    }
}
