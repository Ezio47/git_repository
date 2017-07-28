using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 角色
    /// </summary>
   public class T_SYSSEC_ROLE_SW
    {
       /// <summary>
       /// 角色ID
       /// </summary>
       public string ROLEID { get; set; }
       /// <summary>
       /// 角色名称
       /// </summary>
       public string ROLENAME{get;set;}
       /// <summary>
       /// 角色备注
       /// </summary>
       
       public string ROLENOTE{get;set;}
       /// <summary>
       /// 系统标识
       /// </summary>
       public string SYSFLAG { get; set; }
       /// <summary>
       /// 用户ID
       /// </summary>
       public string USERID { get; set; }
    }
}
