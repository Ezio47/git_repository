using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 巡检条件
    /// </summary>
   public class T_IPSFR_ROUTERAIL_PATROL_SW
   {
       /// <summary>
       /// 护林员和电话号码
       /// </summary>
       public string PhoneHname { get; set; }
       /// <summary>
       /// 护林员编号,可以逗号分隔成多个
       /// </summary>
       public string HID { get; set; }
       /// <summary>
       /// 机构编码
       /// </summary>
       public string orgNo { get; set; }
       /// <summary>
       /// 开始日期
       /// </summary>
       public string DateBegin { get; set; }
       /// <summary>
       /// 结束日期
       /// </summary>
       public string DateEnd { get; set; }
    }
}
