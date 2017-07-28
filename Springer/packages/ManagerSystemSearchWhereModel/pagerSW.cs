using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
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
    }
}
