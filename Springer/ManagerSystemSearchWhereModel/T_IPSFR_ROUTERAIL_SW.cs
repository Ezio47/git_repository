using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 护林员路线/围栏检索条件
    /// </summary>
   public class T_IPSFR_ROUTERAIL_SW
   {
       /// <summary>
       /// 路线/围栏序号
       /// </summary>
       public string ROADID { get; set; }
       /// <summary>
       /// 护林员序号
       /// </summary>
       public string HID { get; set; }
       /// <summary>
       /// 类型 0路线1围栏
       /// </summary>
       public string ROADTYPE { get; set; }
    }
}
