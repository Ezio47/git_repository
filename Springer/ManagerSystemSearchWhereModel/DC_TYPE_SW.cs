using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
        /// 数据中心-类别表
        /// </summary>
    public class DC_TYPE_SW
    {
            /// <summary>
            /// 序号
            /// </summary>
           public  string DCTYPEID { get; set; }
            /// <summary>
            /// 父序号
            /// </summary>
           public  string DCTYPETOPID { get; set; }
            /// <summary>
            /// 类别名称
            /// </summary>
           public string DCTYPENAME { get; set; }
            /// <summary>
            /// 排序号
            /// </summary>
           public string ORDERBY { get; set; }
            /// <summary>
            /// 类别标志
            /// </summary>
           public string DCTYPEFLAG { get; set; }
        /// <summary>
        /// 获取所有下拉框
        /// </summary>
           public string isShowAll { get; set; }
        }
    }

