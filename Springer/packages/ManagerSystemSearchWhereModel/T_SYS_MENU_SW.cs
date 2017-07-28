using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
   public class T_SYS_MENU_SW
   {
       public string MENUID { get; set; }
       public string MENUCODE { get; set; }
       public string MENUNAME { get; set; }
       public string MENUURL { get; set; }
       public string MENUICO { get; set; }
       public string ORDERBY { get; set; }
       public string MENURIGHTFLAG { get; set; }
       public string SYSFLAG { get; set; }
       /// <summary>
       /// 所属系统用户ID
       /// </summary>
       public string UID { get; set; }
    }
}
