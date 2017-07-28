using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 一键报警
    /// </summary>
   public class T_IPS_ALARM_SW
   {
       /// <summary>
       /// 报警序号
       /// </summary>
       public string ALARMID { get; set; }
       /// <summary>
       /// 电话号码
       /// </summary>
       public string PHONE { get; set; }
       /// <summary>
       /// 处理状态
       /// </summary>
       public string MANSTATE { get; set; }
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
