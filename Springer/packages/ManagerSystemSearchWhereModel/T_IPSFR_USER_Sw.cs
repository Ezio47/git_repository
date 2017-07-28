using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
   public  class T_IPSFR_USER_SW
    {
       public string HID { get; set; }
       public string HNAME { get; set; }
       public string SN { get; set; }
       public string PHONE { get; set; }
       public string SEX { get; set; }
       public string BIRTH { get; set; }
       public string ONSTATE { get; set; }
       public string BYORGNO { get; set; }
       public string ISENABLE { get; set; }


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
