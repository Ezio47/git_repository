using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
   public class T_SYSSEC_IPSUSER_SW
   {
       /// <summary>
       /// 序号
       /// </summary>
       public string USERID { get; set; }
       /// <summary>
       /// 登录用户名
       /// </summary>
       public string LOGINUSERNAME { get; set; }
       /// <summary>
       /// 用户真实姓名
       /// </summary>
       public string USERNAME { get; set; }
       /// <summary>
       /// 用户密码
       /// </summary>
       public string USERPWD { get; set; }
       /// <summary>
       /// 每页行数
       /// </summary>
       public int pageSize { get; set; }
       /// <summary>
       /// 当前页数
       /// </summary>
       public int curPage { get; set; }
    }
}
