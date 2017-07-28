using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统权限
    /// </summary>
  public  class T_SYSSEC_RIGHT_SW
    {
      /// <summary>
      /// 权限id
      /// </summary>
      public string RIGHTID{get;set;}
      /// <summary>
      /// 权限名称
      /// </summary>
      public string RIGHTNAME{get;set;}
      /// <summary>
      /// 所属系统标识
      /// </summary>
      public string SYSFLAG { get; set; }
      /// <summary>
      /// 权限编号前几位，如果传递该参数，则需要获取以该编号为前缀的所有权限，为0代表查询段位为3的一级权限
      /// </summary>
      public string SubRightID { get; set; }
    }
}
