using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 文档类别
    /// </summary>
   public class ART_TYPE_Model
   {
       /// <summary>
       /// 类别序号
       /// </summary>
       public string ARTTYPEID { get; set; }
       /// <summary>
       /// 类别名称
       /// </summary>
       public string ARTTYPENAME { get; set; }
       /// <summary>
       /// 父类别序号
       /// </summary>
       public string ARTTYPERID { get; set; }
       /// <summary>
       /// 是否需要审核
       /// </summary>
       public string ISCHECKED { get; set; }
       /// <summary>
       /// 排序号
       /// </summary>
       public string ORDERBY { get; set; }
    }
}