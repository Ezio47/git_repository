using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 分页信息
    /// </summary>
   public class PagerSW
    {
       /// <summary>
       /// 当前页数
       /// </summary>
       public int curPage { get; set; }
       /// <summary>
       /// 总记录数
       /// </summary>
       public int rowCount { get; set; }
       /// <summary>
       /// 每页记录数
       /// </summary>
       public int pageSize { get; set; }
       /// <summary>
       /// 链接地址
       /// </summary>
       public string url { get; set; }
       /// <summary>
       /// 下拉框每页分页数数组
       /// </summary>
       public string[] pageSizeArr { get; set; }
       /// <summary>
       /// 是否隐藏每页条数
       /// </summary>
       public bool hidePageSize { get; set; }
       /// <summary>
       /// 是否隐藏分页下拉框
       /// </summary>
       public bool hidePageSelect { get; set; }
       /// <summary>
       /// 是否隐藏各分页链接
       /// </summary>
       public bool hidePageList { get; set; }
       /// <summary>
       /// 是否隐藏分页信息
       /// </summary>
       public bool hidePageInfo { get; set; }
    }
}
